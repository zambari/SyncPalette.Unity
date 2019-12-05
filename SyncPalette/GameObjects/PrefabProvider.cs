using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
using zUI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PrefabProvider : ProviderBase<PrefabPalette, NamedGameObject, GameObject>, IPaletteProvider
{
    public static PrefabProvider instnace;
    PrefabPalette _ppalette;
    PrefabPalette prefabPalette { get { if (_ppalette == null) _ppalette = palette as PrefabPalette; return _ppalette; } }
    void Awake()
    {
        instnace = this;
    }
    public Slider GetSlider(Transform parent, string label)
    {
        var o = GetGameObject(parent, "Slider", label);
        return o.GetComponentInChildren<Slider>();
    }
    public Slider GetSliderSmall(Transform parent, string label)
    {
        var o = GetGameObject(parent, "SliderSmall", label);
        return o.GetComponentInChildren<Slider>();
    }
    public Button GetButton(Transform parent, string label)
    {
        var o = GetGameObject(parent, "Button", label);
        return o.GetComponentInChildren<Button>();
    }
    public Text GetHeader(Transform parent, string label)
    {
        var o = GetGameObject(parent, "Header", label);
        if (o == null) return null;
        return o.GetComponentInChildren<Text>();
    }
    public static PrefabProvider Get(Component component)
    {
        GameObject source = component.gameObject;
        var prefparent = source.GetComponentInParent<PrefabProvider>();
        if (prefparent != null) return prefparent;
        if (instnace != null) return instnace;
        return ProviderRoot.FindProvider(typeof(NamedGameObject)) as PrefabProvider;
    }
    public LayoutPanel GetPanelWithScrollRect(Transform parent, string label)
    {
        var o = GetGameObject(parent, "PanelWithScrollRect", label);
        return o.GetComponentInChildren<LayoutPanel>();
    }
    public Toggle GetToggle(Transform parent, string label)
    {
        var o = GetGameObject(parent, "Toggle", label);
        return o.GetComponentInChildren<Toggle>();
    }
    public Text GetLabel(Transform parent, string label)
    {
        var o = GetGameObject(parent, "Label", label);
        return o.GetComponentInChildren<Text>();
    }
    public GameObject GetGameObject(Transform parent, string type, string label)
    {
        var o = prefabPalette.GetValue(type);
        if (o == null)
        {
            Debug.Log("could not find " + type);
            return null;
        }
        o = Instantiate(o, parent);
        o.SetActive(true);

        if (!string.IsNullOrEmpty(label))
        {
            o.name = label;
            Text t = o.GetComponentInChildren<Text>();

            t.SetText(label);
        }
        return o;
    }
    public LayoutPanel GetPanel(Transform parent, string label)
    {
        var o = GetGameObject(parent, "Panel", label);
        return o.GetComponentInChildren<LayoutPanel>();
    }
    public LayoutFoldController GetSubPanel(Transform parent, string label)
    {
        var o = GetGameObject(parent, "SubPanel", label);
        return o.GetComponentInChildren<LayoutFoldController>();
    }
    public GameObject GetInstance(string s, Transform parent)
    {
        if (palette == null) return null;
        var template = (palette as PrefabPalette).GetValue(s);
        if (template == null)
        {
            Debug.Log("could not find template " + s);
            return null;
        }
        var instanceTransform = Instantiate(template, parent).transform;
#if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(instanceTransform.gameObject, "prefab added");
#endif
        return instanceTransform.gameObject;

    }


}

// public  static class PrefabProviderExtensions
// {
//     public static Button 

// }