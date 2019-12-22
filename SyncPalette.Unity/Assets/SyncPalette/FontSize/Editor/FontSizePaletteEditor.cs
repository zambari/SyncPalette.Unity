using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
namespace Z
{
    [CustomEditor(typeof(FontSizePalette))]

    public class FontSizePaletteEditor : PaletteEditor<FontSizePalette>
    {


        protected override void DrawSubProperty(SerializedProperty prop)
        {

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+", EditorStyles.miniButton, GUILayout.Width(butWidth)))
            {
                prop.intValue += 1;
            }
            if (GUILayout.Button("-", EditorStyles.miniButton, GUILayout.Width(butWidth)))
            {
                prop.intValue -= 1;
            }

            EditorGUILayout.PropertyField(prop);
            GUILayout.EndHorizontal();
        }

        // protected virtual bool UilityBarEditAction(int i)
        // {
        //     string thisName = values.GetArrayElementAtIndex(i).FindPropertyRelative("name").stringValue;
        //     GUILayout.BeginHorizontal();
        //     if (i == values.arraySize - 1)
        //     {
        //         GUILayout.Label("", GUILayout.Width(butWidth));
        //     }
        //     else
        //     {
        //         if (GUILayout.Button("▼", EditorStyles.miniButton, GUILayout.Width(butWidth)))
        //         {

        //             if (i == values.arraySize - 1) return false;
        //             palette.Move(i, i + 1);
        //             Undo.RecordObject(target, "Moving");
        //             Refresh();
        //             return true;
        //         }
        //     }

        //     if (i == 0)
        //         GUILayout.Label("", GUILayout.Width(butWidth));
        //     else
        //     if (GUILayout.Button("▲", EditorStyles.miniButton, GUILayout.Width(butWidth)))
        //     {

        //         if (i == 0) return false;
        //         palette.Move(i, i - 1);
        //         Undo.RecordObject(target, "Moving");
        //         Refresh();
        //         return true;
        //     }
        //     if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.Width(butWidth)))
        //     {
        //         Undo.RecordObject(target, "Removing");
        //         palette.Remove(i);
        //         Refresh();

        //         return true;
        //     }
        //     // if (GUILayout.Button("D", EditorStyles.miniButton, GUILayout.Width(butWidth)))
        //     // {
        //     //     Undo.RecordObject(target, "Duplicating");
        //     //     palette.Duplicate(i);
        //     //     Refresh();
        //     //     return true;
        //     // }
        //     if (editedName != thisName && GUILayout.Button("E", EditorStyles.miniButton, GUILayout.Width(butWidth)))
        //     {
        //         editedName = thisName;
        //         newName = editedName;
        //         return false; // only opened the editor
        //     }
        //     if (i > usages.Length - 1) Refresh();
        //     try
        //     {
        //         GUILayout.Label(thisName + (usages[i] > 0 ? " (" + usages[i] + ")" : null) + (selectedPresetName == thisName ? " [SEL]" : null));
        //     }
        //     catch
        //     {

        //     }
        //     //  EditorGUILayout.PropertyField();
        //     GUILayout.EndHorizontal();
        //     if (editedName == thisName)
        //         DrawNameEditor();

        //     return false;
        // }

        //         protected override void DrawFoldoutEditor()
        //         {
        //             GUILayout.BeginHorizontal();
        //             GUILayout.BeginVertical();
        //             var temp = EditorGUIUtility.labelWidth;
        //             EditorGUIUtility.labelWidth = 1;
        //             for (int i = 0; i < values.arraySize; i++)
        //                 if (UilityBarEditAction(i))
        //                 {
        //                     GUILayout.Label("if you see this please refresh somehow; )");
        //                     return;
        //                 }
        //             GUILayout.EndVertical();
        //             GUILayout.BeginVertical();
        //             for (int i = 0; i < values.arraySize; i++)
        //                 DrawSubProperty(values.GetArrayElementAtIndex(i).FindPropertyRelative("value"));
        //             GUILayout.EndVertical();
        //             GUILayout.EndHorizontal();
        //             EditorGUIUtility.labelWidth = temp;
        //             if (GUILayout.Button("Add new empty Item"))
        //             {
        //                 editedName = palette.AddNew();
        //                 newName = editedName;
        //                 Refresh();
        //             }
        //         }

        //         public override void OnInspectorGUI()
        //         {
        //             GUILayout.Label("warning, all changes are destructive and there is no undo");
        //             if (values == null) Refresh();

        //             EditorGUI.BeginChangeCheck();
        //             isFoldOut = EditorGUILayout.Foldout(isFoldOut, "Expand values");
        //             if (isFoldOut)
        //                 DrawFoldoutEditor();

        //             if (selectedPresetName == null && Selection.activeObject != null)
        //             {
        //                 if (GUILayout.Button("Add Syncer"))
        //                 {
        //                     foreach (var g in Selection.objects)
        //                     {
        //                         GameObject thisGameObject = g as GameObject;
        //                         if (thisGameObject != null)
        //                             palette.AddSyncer(thisGameObject);
        //                     }
        //                 }
        //             }
        //             if (EditorGUI.EndChangeCheck())
        //             {
        // //                Debug.Log("appliting props");
        //                 serializedObject.ApplyModifiedProperties();
        //                 palette.OnValidate();
        //             }
        //             if (selectedPresetName == null)
        //                 GUILayout.Label("Selected object has no syncer assigned");
        //             else
        //                 GUILayout.Label("Selected object picks :" + selectedPresetName);
        //         }
    }
}
#endif
