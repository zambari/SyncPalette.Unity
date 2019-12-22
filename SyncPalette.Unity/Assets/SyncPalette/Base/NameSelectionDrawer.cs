using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Z
{
    [System.Serializable]
    public class NameSelectionDrawer
    {
        public string value = "";
        public bool requestAdd;

    }
}
#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(NameSelectionDrawer))]
[CanEditMultipleObjects]
public class DrawerNameSelectionDrawer : PropertyDrawer

{
    string[] names = new string[0];
    SerializedProperty nameProp;
    string typeString;

    IValueSync[] query;
    NameSelectionDrawer drawer;
    MonoBehaviour[] sources;
    System.Type T;
    int lastWidth = 100;

    string[] GetNames(SerializedProperty property)
    {
        if (drawer == null)
        {
            var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
            drawer = obj as NameSelectionDrawer;
        }
        var listm = new List<MonoBehaviour>();
        for (int i = 0; i < property.serializedObject.targetObjects.Length; i++)
        {
            var thisMono = property.serializedObject.targetObjects[i] as MonoBehaviour;
            listm.Add(thisMono);
        }
        sources = listm.ToArray();

        if (sources == null) Debug.Log("ugly null");
        List<IValueSync> list = new List<IValueSync>();
        for (int i = 0; i < sources.Length; i++)
        {
            if (sources[i] as IValueSync != null) list.Add(sources[i] as IValueSync);
        }
        query = list.ToArray();
        if (query.Length > 0)
        {
            T = query[0].GetValueType();
            typeString = T.ToString();
            var nameProviders = sources[0].GetComponentsInParent<INamedSelectionProvider>();
            for (int i = 0; i < nameProviders.Length; i++)
            {
                var ret = nameProviders[i].GetNames(T);
                if (ret != null && ret.Length > 0)
                    names = ret;
            }
        }
        else
            Debug.Log("Name Slector host is not implementing IValueSync");
        return names;
    }

    void DrawButton(string s, SerializedProperty property, int width)
    {
        string label = s;
        if (s == nameProp.stringValue)
            label = "[  " + s + "  ]";

        if (GUILayout.Button(label, EditorStyles.miniButton, GUILayout.Width(width)))
        {
            nameProp.stringValue = s;
            for (int i = 0; i < query.Length; i++)
            {
                var thisDrawer = query[i].GetDrawer();
                Undo.RecordObject(sources[i], "Selection change");
                thisDrawer.value = s;
                query[i].UpdateValue();
            }
        }
    }
    void DrawSelectionBar()
    {

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Select provider"))
        {
            var paletteProviders = sources[0].GetComponentsInParent<IPaletteProvider>();
            var Tpal = query[0].GetPaletteType();
            var Tnamed = query[0].GetNamedType();

            var Tval = query[0].GetValueType();
            for (int i = 0; i < paletteProviders.Length; i++)
            {
                var thisPalette = paletteProviders[i].palette ;
                if (thisPalette != null)
                {
                    Selection.activeGameObject = paletteProviders[i].gameObject;
                    Debug.Log("found");
                }

            }
        }
        if (GUILayout.Button("Select pallet"))
        {
            var paletteProviders = sources[0].GetComponentsInParent<IPaletteProvider>();
            var Tpal = query[0].GetPaletteType();
            var Tnamed = query[0].GetNamedType();

            var Tval = query[0].GetValueType();
            for (int i = 0; i < paletteProviders.Length; i++)
            {
                if (paletteProviders[i].IsOfType(Tnamed))
                {
                    var thisPalette = (paletteProviders[i] as IPaletteProvider).palette;
                    if (thisPalette != null)
                    {
                        Selection.activeObject = thisPalette.scriptableObject;
                        Debug.Log("found");
                    }

                }

            }
        }
        GUILayout.EndHorizontal();
    }
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        if (names == null || names.Length == 0)
            names = GetNames(property);
        if (nameProp == null) nameProp = property.FindPropertyRelative("value");
        if (names.Length == 0)
            GUILayout.Label("No names found in parents " + typeString);
        int width = (int)(rect.width / 2);
        if (width > 100)
        {
            width -= 10;
            lastWidth = width;
        }
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        for (int i = 0; i < names.Length / 2; i++)
            DrawButton(names[i], property, lastWidth);
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        for (int i = names.Length / 2; i < names.Length; i++)
            DrawButton(names[i], property, lastWidth);
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (names.Length == 0)
        {
            GUILayout.Label("No Value provider responds ");
        }
        bool requestAdd = property.FindPropertyRelative("requestAdd").boolValue;
        if (requestAdd)
        {
            if (GUILayout.Button("Add current value"))
            {
                var adder = sources[0] as IAddCurrentValue;
                if (adder != null) adder.AddCurrentValue();

            }

        }
        DrawSelectionBar();
        GUILayout.Label("Seleceted: " + nameProp.stringValue, EditorStyles.boldLabel);
    }
}

#endif
