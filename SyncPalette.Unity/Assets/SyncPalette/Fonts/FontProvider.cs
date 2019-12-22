using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{
    public class FontProvider : ProviderBase<FontPalette, NamedFont, Font>,  IPaletteProvider
    {
        // public FontPalette Palette;
        // public override IPalette GetPalette(System.Type T)
        // {
        //     if (T==typeof(NamedFont))
        //     return Palette;
        //     return null;
        // }
        
        // public override void SetPalette(FontPalette c)
        // {
        //     Debug.Log("setting palet fontprovider "+(c==null));
        //     Palette = c;
        // }

    }
}