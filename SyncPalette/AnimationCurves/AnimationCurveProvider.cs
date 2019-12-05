using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{
    public class AnimationCurveProvider : ProviderBase<AnimationCurvePalette, NamedAnimationCurve, AnimationCurve>,  IPaletteProvider
    {
        // public AnimationCurvePalette Palette;

        // public override IPalette GetPalette(System.Type T)
        // {
        //     if (T == typeof(NamedAnimationCurve))
        //         return Palette;
        //     return null;
        // }
        // public override void SetPalette(AnimationCurvePalette c)
        // {
        //     Palette = c;
        // }

    }
}