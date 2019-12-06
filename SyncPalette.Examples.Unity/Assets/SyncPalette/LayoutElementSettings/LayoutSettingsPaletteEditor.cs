using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace Z
{
    [CustomPropertyDrawer(typeof(LayoutElementSettings))]
    public class LayoutSetingsDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {

            var isIgnore = prop.FindPropertyRelative("ignoreLayout");
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(isIgnore);
            //  if (!isIgnore.boolValue)
            //   {
            GUILayout.BeginVertical();
            GUILayout.Label("min");
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("minWidth"));
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("minHeight"));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("preferred");
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("preferredWidth"));
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("preferredHeight"));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("flexible");
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("flexibleWidth"));
            EditorGUILayout.PropertyField(prop.FindPropertyRelative("flexibleHeight"));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
    // [CustomPropertyDrawer(typeof(ColorPalette), true)] public class ColorPaletteDrawer : ScriptableObjectDrawer { }
    [CustomEditor(typeof(LayoutSettingsPalette))]
    public class LayoutSettingsPaletteEditor : PaletteEditor<NamedLayoutSettings>
    {

   
        protected override void DrawFoldoutEditor()
        {
            var temp = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 1;
            for (int i = 0; i < values.arraySize; i++)
            {
                GUILayout.Space(30);
                if (UilityBarEditAction(i))
                {
                    GUILayout.Label("if you see this please refresh somehow; )");
                    return;
                }
                EditorGUILayout.PropertyField(values.GetArrayElementAtIndex(i).FindPropertyRelative("value"));
            }

            EditorGUIUtility.labelWidth = temp;
            if (GUILayout.Button("Add new empty Item"))
            {
                editedName = palette.AddNew();
                newName = editedName;
                Refresh();
            }
        }

    }
}
#endif