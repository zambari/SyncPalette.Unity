using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventToggleGate1 : MonoBehaviour
{

    // Use this for initialization
    Toggle toggle;
    public VoidEvent whenToggledOn;
    public VoidEvent whenToggledOff;
    public BoolEvent forwardTrigger;
    public BoolEvent invertedTrigger;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(OnToggleValue);
    }

    public void OnToggleValue(bool b)
    {
        if (!enabled) return;
        if (b) whenToggledOn.Invoke();
        else whenToggledOff.Invoke();
        invertedTrigger.Invoke(!b);
        forwardTrigger.Invoke(b);

    }

}
