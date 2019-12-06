using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z
{
    public class StyleSelector : MonoBehaviour
    {

        public List<StyleDefinition> styles;
        public Toggle toggleTemplate;
        public ColorProvider colorProvider;
        public FontProvider fontProvider;
        public FontSizeProvider fontSizeProvider;
        public LayoutElementSettingsProvider layoutElementSettingsProvider;
        int currentStyle = 0;
        void OnValidate()
        {
            if (toggleTemplate == null) toggleTemplate = GetComponentInChildren<Toggle>();
            if (colorProvider == null) colorProvider = GetComponentInParent<ColorProvider>();
            if (fontProvider == null) fontProvider = GetComponentInParent<FontProvider>();
            if (fontSizeProvider == null) fontSizeProvider = GetComponentInParent<FontSizeProvider>();


            if (layoutElementSettingsProvider == null) layoutElementSettingsProvider = GetComponentInParent<LayoutElementSettingsProvider>();
        }
        void Start()
        {
            toggleTemplate.gameObject.SetActive(false);
            for (int i = 0; i < styles.Count; i++)
            {
                var thisToggle = Instantiate(toggleTemplate, toggleTemplate.transform.parent);
                thisToggle.GetComponentInChildren<Text>().SetText(styles[i].name);
                thisToggle.gameObject.SetActive(true);
                var thisStyle = styles[i];
                thisToggle.onValueChanged.AddListener((x) => { if (x) LoadStyle(thisStyle); });
            }
        }


        public void LoadStyle(StyleDefinition style)
        {
            if (colorProvider != null) colorProvider.palette = style.colorPalette;
            if (fontProvider != null) fontProvider.palette = style.fontPalette;
            if (fontSizeProvider != null) fontSizeProvider.palette = style.fontSizePalette;
            if (layoutElementSettingsProvider != null) layoutElementSettingsProvider.palette = style.layoutSettingsPalette;
            Debug.Log("loaded style set " + style.name);
#if UNITY_EDITOR

            UnityEditor.EditorApplication.delayCall += UnityEditor.EditorApplication.RepaintHierarchyWindow;
#endif
        }


        [ExposeMethodInEditor]

        void LoadNext()
        {
            if (styles == null || styles.Count == 0)
            {
                Debug.Log("Add some styles first");
                return;
            }
            currentStyle++;
            if (currentStyle >= styles.Count) currentStyle = 0;

            LoadStyle(styles[currentStyle]);
        }
        [ExposeMethodInEditor]
        void LoadPrev()
        {
            if (styles == null || styles.Count == 0)
            {
                Debug.Log("Add some styles first");
                return;
            }
            currentStyle--;
            if (currentStyle < 0) currentStyle = styles.Count - 1;
            LoadStyle(styles[currentStyle]);
        }


    }
}