using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
[ExecuteInEditMode]
[DisallowMultipleComponent]
public class LayoutSettingSync : SyncBase<LayoutSettingsPalette, NamedLayoutSettings, LayoutElementSettings>
{

    LayoutElement layoutElement;
    protected override void OnEnable()
    {
        if (layoutElement == null) layoutElement = GetComponent<LayoutElement>();
           base.OnEnable();
    }

    public override void UpdateValue()
    {
        if (layoutElement == null) layoutElement = GetComponent<LayoutElement>();
        if (nameSelection == null || string.IsNullOrEmpty(nameSelection.value)) return;
        if (Palette != null)
        {
            LayoutElementSettings s = Palette.GetValue( nameSelection.value);
            if (s != null)
                s.Apply(layoutElement);
        }
        else Debug.Log("no color Palette in parent ", gameObject);
#if UNITY_EDITOR

#endif
    }
    public override LayoutElementSettings GetCurrentValue()
    {
        if (layoutElement == null) layoutElement = GetComponent<LayoutElement>();
        var thisSet = new LayoutElementSettings(layoutElement);
        return thisSet;
    }


}
