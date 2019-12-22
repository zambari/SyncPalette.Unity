using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
#endif

namespace Z
{
    // this is not a good place to start reading the code as I didn't know another way thant to box float which makes things overly complicated

    [CreateAssetMenu(menuName = "Palettes/FontSizePalette", fileName = "FontSizePalette")]
    public class FontSizePalette : PaletteBase<NamedFontSize, int>
    {
        public FontSizePalette otherPal;
        public bool copy;
        public override void OnValidate()
        {
            base.OnValidate();
            if (otherPal != null && copy)
            {
                values = otherPal.values;
                copy = false;
            }
        }
        public NamedFontSize[] values = new NamedFontSize[0];
        protected override NamedFontSize[] GetValues() { return values; }
        protected override void SetValues(NamedFontSize[] newValues) { values = newValues; }
        public override void AddSyncer(GameObject target)
        {

            var sync = target.AddComponent<FontSizeSync>();
#if UNITY_EDITOR1
            Undo.RegisterCreatedObjectUndo(sync, "adding sync");
#endif
        }
        protected override void AddDefaultValues()
        {

            values = new NamedFontSize[] {
                new NamedFontSize(){name= "Tiny", value= 8},
                new NamedFontSize(){name= "Small", value= 10},
                new NamedFontSize(){name= "Smallish", value= 12},
                new NamedFontSize(){name= "Normal", value=12 },
                new NamedFontSize(){name= "Large", value= 16}

       };
        }
    }

}

#if UNITY_EDITOR
namespace Z
{
    // [CustomPropertyDrawer(typeof(FontSizePalette), true)] public class FontSizePaletteDrawer : ScriptableObjectDrawer { }
    [CustomEditor(typeof(FontSizePalette))] public class FontSizePaletteEditor : PaletteEditor<NamedFontSize> { }
}
#endif
