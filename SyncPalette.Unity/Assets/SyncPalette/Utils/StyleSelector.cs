using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Z
{
    public class StyleSelector : MonoBehaviour
    {
        public List<StyleDefinition> styles;
        public Toggle toggleTemplate;
        public FontProvider fontProvider;
        public FontSizeProvider fontSizeProvider;
        public LayoutElementSettingsProvider layoutElementSettingsProvider;
        public LayoutGroupSettingsProvider layoutGroupSettingsProvider;
        int currentStyle = 0;
        public bool loadGroupSettings = false;
        void OnValidate()
        {
            if (toggleTemplate == null) toggleTemplate = GetComponentInChildren<Toggle>();
            if (fontProvider == null) fontProvider = GetComponentInParent<FontProvider>();
            if (fontSizeProvider == null) fontSizeProvider = GetComponentInParent<FontSizeProvider>();

            if (layoutGroupSettingsProvider == null) layoutGroupSettingsProvider = GetComponentInParent<LayoutGroupSettingsProvider>();

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
            if (fontProvider != null) fontProvider.palette = style.fontPalette;
            if (fontSizeProvider != null) fontSizeProvider.palette = style.fontSizePalette;
            if (layoutElementSettingsProvider != null) layoutElementSettingsProvider.palette = style.layoutSettingsPalette;
            if (loadGroupSettings)
                if (layoutGroupSettingsProvider != null) layoutGroupSettingsProvider.palette = style.layoutGroupSettingsPalette;



            Debug.Log("loaded style set " + style.name);
#if UNITY_EDITOR

            //  UnityEditor.EditorApplication.delayCall += UnityEditor.EditorApplication.RepaintHierarchyWindow;
            //EditorWindow view = EditorWindow.GetWindow<SceneView>();
            UnityEditor.EditorApplication.delayCall += Resubscribe;
            //view.Repaint();

#endif
        }
#if UNITY_EDITOR
        void Resubscribe()
        {
            gameObject.SetActive(false);
            UnityEditor.EditorApplication.delayCall += TryToRefresh; //
        }

        void TryToRefresh()
        {
            gameObject.SetActive(true);
            //var sel = Selection.activeGameObject;
            //Selection.activeGameObject = (GameObject.FindObjectOfType(typeof(Image)) as Image).gameObject;
            //UnityEditor.EditorApplication.delayCall += () => Selection.activeGameObject = sel;

        }
        //  UnityEditor.EditorApplication.delayCall += UnityEditor.EditorApplication.RepaintHierarchyWindow;
        //EditorWindow view = EditorWindow.GetWindow<SceneView>();
        //view.Repaint();

#endif

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