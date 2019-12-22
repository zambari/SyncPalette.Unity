using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Z
{

    [CreateAssetMenu(menuName = "Palettes/LayoutGroupSettingsPalette", fileName = "LayoutGroupSettingsPalette")]
    public class LayoutGroupSettingsPalette : PaletteBase<NamedLayoutGroupSettings, LayoutGroupSettings>
    {
        public NamedLayoutGroupSettings[] values;
        protected override NamedLayoutGroupSettings[] GetValues()
        { return values; }
        protected override void SetValues(NamedLayoutGroupSettings[] newValues)
        { values = newValues as NamedLayoutGroupSettings[]; }

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

            var sync = target.AddComponent<LayoutGroupSettingSync>();
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(sync, "adding sync");
#endif
        }
    }

}
