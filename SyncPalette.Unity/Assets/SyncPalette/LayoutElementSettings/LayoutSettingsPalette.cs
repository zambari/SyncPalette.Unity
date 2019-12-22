using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Z
{

    [CreateAssetMenu(menuName = "Palettes/LayoutSettingsPalette", fileName = "LayoutSettingsPalette")]
    public class LayoutSettingsPalette : PaletteBase<NamedLayoutSettings, LayoutElementSettings>
    {
        public NamedLayoutSettings[] values;
        protected override NamedLayoutSettings[] GetValues()
        { return values; }
        protected override void SetValues(NamedLayoutSettings[] newValues)
        { values = newValues as NamedLayoutSettings[]; }

        protected override void AddDefaultValues()
        {
#if UNITY_EDITOR

            // values = new NamedColor[] {
            //     new NamedColor(){name= "BG", value=Color.black},
            //     new NamedColor(){name= "Black", value=Color.black},
            //     new NamedColor(){name= "White", value=Color.white},
            //     new NamedColor(){name= "Text", value=Color.white},
            //     new NamedColor(){name= "TextTop", value=Color.white},
            //     new NamedColor(){name= "PanelFill", value=new Color(0.6f,0.6f,0.6f,0.2f)},
            //     new NamedColor(){name= "Accent", value=new Color(0.3f,0.8f,0.8f,0.8f)},
            //     new NamedColor(){name= "AccentDim", value=new Color(0.2f,0.8f,0.7f,0.3f)}
            //   };
#endif
        }
        public override void AddSyncer(GameObject target)
        {

            var sync = target.AddComponent<LayoutSettingSync>();
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(sync, "adding sync");
#endif
        }
    }

}
