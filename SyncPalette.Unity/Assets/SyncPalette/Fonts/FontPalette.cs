using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Z
{
    [CreateAssetMenu(menuName = "Palettes/FontPalette", fileName = "FontPalette")]
    public class FontPalette : PaletteBase<NamedFont, Font>
    {

        public FontPalette otherPal;
        public bool copy;
        public override void OnValidate()
        {
            base.OnValidate();
            if (otherPal != null && copy)
            {
                values = otherPal.values;
                copy = false;
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].value != null && values[i].name == null)
                    values[i].name = values[i].value.name;
            }
        }
        public NamedFont[] values= new NamedFont[0];
        protected override NamedFont[] GetValues() { return values; }
        protected override void SetValues(NamedFont[] newVals) { values = newVals; }

        public override void AddSyncer(GameObject target)
        {

            var sync = target.AddComponent<FontSync>();
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(sync, "adding sync");
#endif
        }
        protected override void AddDefaultValues()
        {
#if UNITY_EDITOR
            Font defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            values = new NamedFont[] {
                new NamedFont(){name= "Arial", value=defaultFont}
            };
#endif
        }
    }
}

#if UNITY_EDITOR1
namespace Z
{
    //  [CustomPropertyDrawer(typeof(FontPalette), true)] public class FontPaletteDrawer : ScriptableObjectDrawer { }//
    // [CustomEditor(typeof(FontPalette))] public class FontPaletteEditor : PaletteEditor<NamedFont> { }
}
#endif