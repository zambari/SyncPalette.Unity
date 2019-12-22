using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class FontSizeSync : SyncBase<FontSizePalette, NamedFontSize, int>
    {
        Text text;
        public int currentFontSize;
        public override void UpdateValue()
        {
            if (text == null) text = GetComponent<Text>();
            if (Palette != null && nameSelection != null && !string.IsNullOrEmpty(nameSelection.value) && nameSelection != null)
            {
                var val = Palette.GetValue(nameSelection.value);
                if (val == 0)
                {
                    Debug.Log("no val for " + nameSelection.value, gameObject);
                }
                else
                    text.fontSize = val;
            }
            currentFontSize = text.fontSize;
        }
        public override int GetCurrentValue()
        {
            if (text == null) text = GetComponent<Text>();
            return text.fontSize;
        }
    }

}