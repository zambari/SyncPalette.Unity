using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
[ExecuteInEditMode]
[DisallowMultipleComponent]
public class ColorSync : SyncBase<ColorPalette, NamedColor, Color>
{

    Text text;
    Image image;
    protected override void OnEnable()
    {
        text = GetComponent<Text>();
        image = GetComponent<Image>();
        base.OnEnable();
    }

    public override void UpdateValue()
    {
        if (nameSelection == null || string.IsNullOrEmpty(nameSelection.value))
        {
            return;
        }
        if (Palette != null)
        {
            Color c = Palette.GetValue(nameSelection.value);
            if (c != default(Color))
            {
                if (image != null)
                {
                    image.color = c;
                }
                if (text != null)
                {
                    text.color = c;
                }
            }
            else Debug.Log("invalid name here?", gameObject);
        }
        else Debug.Log("no color Palette in parent ", gameObject);

    }
    public override Color GetCurrentValue()
    {
        if (image == null) image = GetComponent<Image>();
        if (image != null) return image.color;
        if (text == null) text = GetComponent<Text>();
        if (text != null) return text.color;
        return default(Color);
    }


}
