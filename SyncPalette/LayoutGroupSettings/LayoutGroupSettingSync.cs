using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
[ExecuteInEditMode]
[DisallowMultipleComponent]
public class LayoutGroupSettingSync : SyncBase<LayoutGroupSettingsPalette, NamedLayoutGroupSettings, LayoutGroupSettings>
{

    VerticalLayoutGroup vertical;
    HorizontalLayoutGroup horizontal;
    bool hasVertical;
    bool hasHorizontal;
    protected override void OnEnable()
    {
        if (vertical == null) vertical = GetComponent<VerticalLayoutGroup>();
        if (horizontal == null) horizontal = GetComponent<HorizontalLayoutGroup>();
        hasVertical = vertical != null;
        hasHorizontal = horizontal != null;
        base.OnEnable();
    }

    public override void UpdateValue()
    {
        if (nameSelection == null || string.IsNullOrEmpty(nameSelection.value)) return;
        if (Palette != null)
        {
            LayoutGroupSettings s = Palette.GetValue( nameSelection.value);
            if (s != null)
            {
                if (hasHorizontal)
                    s.Apply(horizontal);
                if (hasVertical)
                    s.Apply(vertical);
            }
        }
        else Debug.Log("no layout group settings sync in parent ", gameObject);
#if UNITY_EDITOR

#endif
    }
    public override LayoutGroupSettings GetCurrentValue()
    {
        if (hasVertical) return new LayoutGroupSettings(vertical);
        if (hasHorizontal) return new LayoutGroupSettings(horizontal);

        return null;
    }


}


