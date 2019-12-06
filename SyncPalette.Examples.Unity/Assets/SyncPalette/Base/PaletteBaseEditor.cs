using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace Z
{

    //  [CustomEditor(typeof(ColorPalette))]

    public class PaletteEditor<T> : Editor
    {
        protected SerializedProperty values;
        protected static readonly int butWidth = 18;

        protected IPalette palette;
        protected string editedName;
        protected string newName;
        protected int[] usages = null;
        protected int selectedObject;
        protected string selectedPresetName;
        protected bool isFoldOut = true;

        protected void OnSelectionChanged()
        {
            if (Selection.activeGameObject == null) selectedPresetName = null;
            else
            {
                var valueSyncers = Selection.activeGameObject.GetComponents<IValueSync>();
                if (valueSyncers.Length == 0)
                {
                    selectedPresetName = null;
                    return;
                }
                foreach (var x in valueSyncers)
                {
                    if (x.GetNamedType().Equals(typeof(T)))
                    {
                        selectedPresetName = x.presetName;
                        Repaint();
                        return;
                    }
                }
            }
        }
        protected void Awake()
        {
            Selection.selectionChanged -= OnSelectionChanged;
            Selection.selectionChanged += OnSelectionChanged;
            Refresh();
            (target as IPalette).InitializeDictionary();
        }
        protected void OnDestroy()
        {
            Selection.selectionChanged -= OnSelectionChanged;
        }
        protected void CountUsages()
        {

            usages = new int[palette.validNames.Length];
            for (int i = 0; i < palette.validNames.Length; i++)
            {
                usages[i] = palette.CountUsage(palette.validNames[i]);
            }
        }
        protected void Refresh()
        {
            if (palette == null)
                palette = target as IPalette;
            serializedObject.Update();
            values = serializedObject.FindProperty("values");
            CountUsages();
            OnSelectionChanged();
        }
        protected virtual bool UilityBarEditAction(int i)
        {
            string thisName = values.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;
            GUILayout.BeginHorizontal();
            if (i == values.arraySize - 1)
            {
                GUILayout.Label("", GUILayout.Width(butWidth));
            }
            else
            {
                if (GUILayout.Button("▼", EditorStyles.miniButton, GUILayout.Width(butWidth)))
                {

                    if (i == values.arraySize - 1) return false;
                    palette.Move(i, i + 1);
                    Undo.RecordObject(target, "Moving");
                    Refresh();
                    return true;
                }
            }

            if (i == 0)
                GUILayout.Label("", GUILayout.Width(butWidth));
            else
            if (GUILayout.Button("▲", EditorStyles.miniButton, GUILayout.Width(butWidth)))
            {

                if (i == 0) return false;
                palette.Move(i, i - 1);
                Undo.RecordObject(target, "Moving");
                Refresh();
                return true;
            }
            if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.Width(butWidth)))
            {
                Undo.RecordObject(target, "Removing");
                palette.Remove(i);
                Refresh();

                return true;
            }
            // if (GUILayout.Button("D", EditorStyles.miniButton, GUILayout.Width(butWidth)))
            // {
            //     Undo.RecordObject(target, "Duplicating");
            //     palette.Duplicate(i);
            //     Refresh();
            //     return true;
            // }
            if (editedName != thisName && GUILayout.Button("E", EditorStyles.miniButton, GUILayout.Width(butWidth)))
            {
                editedName = thisName;
                newName = editedName;
                return false; // only opened the editor
            }
            if (i > usages.Length - 1) Refresh();
            try
            {
                GUILayout.Label(thisName + (usages[i] > 0 ? " (" + usages[i] + ")" : null) + (selectedPresetName == thisName ? " [SEL]" : null));
            }
            catch
            {

            }
            //  EditorGUILayout.PropertyField();
            GUILayout.EndHorizontal();
            if (editedName == thisName)
                DrawNameEditor();

            return false;
        }
        protected virtual void DrawSubProperty(SerializedProperty prop)
        {
            EditorGUILayout.PropertyField(prop);
        }
        protected virtual void DrawFoldoutEditor()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            var temp = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 1;
            for (int i = 0; i < values.arraySize; i++)
                if (UilityBarEditAction(i))
                {
                    GUILayout.Label("if you see this please refresh somehow; )");
                    return;
                }
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            for (int i = 0; i < values.arraySize; i++)
                DrawSubProperty(values.GetArrayElementAtIndex(i).FindPropertyRelative("value"));
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = temp;
            if (GUILayout.Button("Add new empty Item"))
            {
                editedName = palette.AddNew();
                newName = editedName;
                Refresh();
            }
        }
        protected void DrawNameEditor()
        {
            GUILayout.BeginHorizontal();
            newName = GUILayout.TextField(newName);
            if (GUILayout.Button("Save", GUILayout.Width(40)))
            {
                if (!string.IsNullOrEmpty(newName))
                {
                    palette.Rename(editedName, newName);
                    editedName = null;
                    Refresh();
                }
            }
            if (GUILayout.Button("Cancel", GUILayout.Width(50)))
                editedName = null;
            GUILayout.EndHorizontal();

        }
        public override void OnInspectorGUI()
        {
            GUILayout.Label("warning, all changes are destructive and there is no undo");
            if (values == null) Refresh();

            EditorGUI.BeginChangeCheck();
            isFoldOut = EditorGUILayout.Foldout(isFoldOut, "Expand values");
            if (isFoldOut)
                DrawFoldoutEditor();

            if (selectedPresetName == null && Selection.activeObject != null)
            {
                if (GUILayout.Button("Add Syncer"))
                {
                    foreach (var g in Selection.objects)
                    {
                        GameObject thisGameObject = g as GameObject;
                        if (thisGameObject != null)
                            palette.AddSyncer(thisGameObject);
                    }
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
//                Debug.Log("appliting props");
                serializedObject.ApplyModifiedProperties();
                palette.OnValidate();
            }
            if (selectedPresetName == null)
                GUILayout.Label("Selected object has no syncer assigned");
            else
                GUILayout.Label("Selected object picks :" + selectedPresetName);
        }
    }
}
#endif
