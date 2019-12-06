using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Z
{

    [CreateAssetMenu(menuName = "Palettes/PrefabPalette", fileName = "PrefabPalette")]
    public class PrefabPalette : PaletteBase<NamedGameObject, GameObject>
    {
        public NamedGameObject[] values;
        protected override NamedGameObject[] GetValues() { return values; }
        protected override void SetValues(NamedGameObject[] newValues) { values = newValues; }

        protected override void AddDefaultValues()
        {
#if UNITY_EDITOR

            values = new NamedGameObject[] {
                new NamedGameObject(){name= "Panel", value=null},
                new NamedGameObject(){name= "SubPanel", value=null},
                new NamedGameObject(){name= "SubPanelSmall", value=null},
                new NamedGameObject(){name= "Label", value=null},
                new NamedGameObject(){name= "Header", value=null},
                new NamedGameObject(){name= "Toggle", value=null},
                new NamedGameObject(){name= "Button", value=null},
                new NamedGameObject(){name= "Toggle", value=null},
                new NamedGameObject(){name= "ToggleHoriz", value=null},
                new NamedGameObject(){name= "ToggleRocker", value=null},
                new NamedGameObject(){name= "ComponentPanel", value=null},
                new NamedGameObject(){name= "Slider", value=null},
                new NamedGameObject(){name= "SliderSmall", value=null},
                new NamedGameObject(){name= "HorizontalLayout", value=null}

            };
#endif
        }
    }
}

#if UNITY_EDITOR
namespace Z
{
    // [CustomPropertyDrawer(typeof(PrefabPalette), true)] public class PrefabPaletteDrawer : ScriptableObjectDrawer { }
    [CustomEditor(typeof(PrefabPalette))] public class CPrefabPaletteEditor : PaletteEditor<NamedGameObject> { }
}
#endif