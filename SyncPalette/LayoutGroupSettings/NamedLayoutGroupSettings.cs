using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z
{

    [System.Serializable] public class NamedLayoutGroupSettings : NamedValue<LayoutGroupSettings> { public NamedLayoutGroupSettings() { } }
    [System.Serializable]
    public class LayoutGroupSettings

    {

        public RectOffset padding;
        public float spacing;

        public LayoutGroupSettings()
        {

        }
        public LayoutGroupSettings(HorizontalLayoutGroup e)
        {
            padding = e.padding;
            spacing = e.spacing;
        }

        public LayoutGroupSettings(VerticalLayoutGroup e)
        {
            padding = e.padding;
            spacing = e.spacing;
        }
        public void Apply(VerticalLayoutGroup e)
        {
            e.padding = padding;
            e.spacing = spacing;
        }
        public void Apply(HorizontalLayoutGroup e)
        {
            e.padding = padding;
            e.spacing = spacing;
        }
    }
}

