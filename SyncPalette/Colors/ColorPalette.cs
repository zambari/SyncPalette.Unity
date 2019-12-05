using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Z
{

    [CreateAssetMenu(menuName = "Palettes/ColorPalette", fileName = "ColorPalette")]
    public class ColorPalette : PaletteBase<NamedColor, Color>
    {
        public ColorPalette otherPal;
        public override void OnValidate()
        {
            base.OnValidate();
            if (otherPal != null && copy)
                values = otherPal.values;
        }
        public bool copy;

        public NamedColor[] values=new NamedColor[0];
        protected override NamedColor[] GetValues()
        { return values; }
        protected override void SetValues(NamedColor[] newValues)
        { values = newValues as NamedColor[]; }

        protected override void AddDefaultValues()
        {
#if UNITY_EDITOR

            values = new NamedColor[] {
                new NamedColor(){name= "BG", value=Color.black},
                new NamedColor(){name= "Black", value=Color.black},
                new NamedColor(){name= "White", value=Color.white},
                new NamedColor(){name= "Text", value=Color.white},
                new NamedColor(){name= "TextTop", value=Color.white},
                new NamedColor(){name= "PanelFill", value=new Color(0.6f,0.6f,0.6f,0.2f)},
                new NamedColor(){name= "Accent", value=new Color(0.3f,0.8f,0.8f,0.8f)},
                new NamedColor(){name= "AccentDim", value=new Color(0.2f,0.8f,0.7f,0.3f)}
            };
#endif
        }
        public override void AddSyncer(GameObject target)
        {

            var sync = target.AddComponent<ColorSync>();
#if UNITY_EDITOR1
            Undo.RegisterCreatedObjectUndo(sync,"adding sync");
#endif
        }
    }

}

#if UNITY_EDITOR
namespace Z
{
    // [CustomPropertyDrawer(typeof(ColorPalette), true)] public class ColorPaletteDrawer : ScriptableObjectDrawer { }
    [CustomEditor(typeof(ColorPalette))] public class ColorPaletteEditor : PaletteEditor<NamedColor> { }
}
#endif