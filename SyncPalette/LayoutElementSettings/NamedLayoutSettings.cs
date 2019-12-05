using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z
{

    [System.Serializable] public class NamedLayoutSettings : NamedValue<LayoutElementSettings> { public NamedLayoutSettings() { } }
    [System.Serializable]
    public class LayoutElementSettings

    {
        public bool ignoreLayout;
        public float minHeight = 20;
        public float minWidth = 20;
        public float preferredWidth = 30;
        public float preferredHeight = 20;
        public float flexibleWidth = 0.1f;
        public float flexibleHeight = 0.1f;

        public LayoutElementSettings()
        {

        }
        public LayoutElementSettings(LayoutElement e)
        {
            if (e == null) return;

            ignoreLayout = e.ignoreLayout;
            minHeight = e.minHeight;
            minWidth = e.minWidth;
            preferredWidth = e.preferredWidth;
            preferredHeight = e.preferredHeight;

            flexibleWidth = e.flexibleWidth;
            flexibleHeight = e.flexibleHeight;

        }
        public void Apply(LayoutElement e)
        {
            if (e == null) return;
            e.ignoreLayout = ignoreLayout;
            e.minHeight = minHeight;
            e.minWidth = minWidth;
            e.preferredWidth = preferredWidth;
            e.preferredHeight = preferredHeight;
            e.flexibleWidth = flexibleWidth;
            e.flexibleHeight = flexibleHeight;
        }
    }
}

