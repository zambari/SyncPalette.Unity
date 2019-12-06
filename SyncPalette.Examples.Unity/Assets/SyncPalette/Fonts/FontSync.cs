using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]

    public class FontSync : SyncBase<FontPalette, NamedFont, Font>
    {
        Text text;
        public Font currentFont;
        public override void UpdateValue()
        {
            if (text == null) text = GetComponent<Text>();
            if (Palette == null || nameSelection == null || nameSelection.value == null) return;
            var font = Palette.GetValue( nameSelection.value);
            if (font != null)
                text.font = font;
            currentFont = text.font;
        }
        public override Font GetCurrentValue()
        {
            if (text == null) text = GetComponent<Text>();
            return text.font;
        }
    }
}