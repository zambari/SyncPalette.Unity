using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Z
{

    [CreateAssetMenu(menuName = "Palettes/AnimationCurvePalette")] //, fileName = "AnimationCurvePalette"
    public class AnimationCurvePalette : PaletteBase<NamedAnimationCurve, AnimationCurve>
    {
        public NamedAnimationCurve[] values= new NamedAnimationCurve[0];
        protected override NamedAnimationCurve[] GetValues()
        { return values; }
        protected override void SetValues(NamedAnimationCurve[] newValues)
        { values = newValues as NamedAnimationCurve[]; }

        protected override void AddDefaultValues()
        {
#if UNITY_EDITOR


#endif
        }
        public override NamedAnimationCurve CrateItem()
        {
            return new NamedAnimationCurve();
        }
        public override void AddSyncer(GameObject target)
        {

            //             var sync = target.AddComponent<ColorSync>();
            // #if UNITY_EDITOR
            //             Undo.RegisterCreatedObjectUndo(sync,"adding sync");
            // #endif
        }
    }

}

#if UNITY_EDITOR1
namespace Z
{
    // [CustomPropertyDrawer(typeof(ColorPalette), true)] public class ColorPaletteDrawer : ScriptableObjectDrawer { }
//    [CustomEditor(typeof(AnimationCurvePalette))] public class AnimationCurveEditor : PaletteEditor<NamedAnimationCurve> { }
}
#endif