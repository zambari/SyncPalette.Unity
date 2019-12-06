
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Z
{
    public class ColorPaletteSelector : MonoBehaviour
    {

        public Toggle toggleTemplate;
        public ColorProvider colorProvider;
        int currentStyle = 0;
        public List<ColorPalette> colorPalettes;
        void OnValidate()
        {
            if (toggleTemplate == null) toggleTemplate = GetComponentInChildren<Toggle>();
            if (colorProvider == null) colorProvider = GetComponentInParent<ColorProvider>();
        }
        void Start()
        {
            toggleTemplate.gameObject.SetActive(false);
            for (int i = 0; i < colorPalettes.Count; i++)
            {
                var thisToggle = Instantiate(toggleTemplate, toggleTemplate.transform.parent);
                thisToggle.GetComponentInChildren<Text>().SetText(colorPalettes[i].name);
                thisToggle.gameObject.SetActive(true);
                var thisStyle = colorPalettes[i];
                thisToggle.onValueChanged.AddListener((x) => { if (x) LoadStyle(thisStyle); });
            }
        }


        public void LoadStyle(ColorPalette style)
        {
            if (colorProvider != null) colorProvider.palette = style;
            Debug.Log("loaded style set " + style.name);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += Resubscribe;
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

        }

#endif

        [ExposeMethodInEditor]

        void LoadNext()
        {
            if (colorPalettes == null || colorPalettes.Count == 0)
            {
                Debug.Log("Add some styles first");
                return;
            }
            currentStyle++;
            if (currentStyle >= colorPalettes.Count) currentStyle = 0;

            LoadStyle(colorPalettes[currentStyle]);
        }
        [ExposeMethodInEditor]
        void LoadPrev()
        {
            if (colorPalettes == null || colorPalettes.Count == 0)
            {
                Debug.Log("Add some styles first");
                return;
            }
            currentStyle--;
            if (currentStyle < 0) currentStyle = colorPalettes.Count - 1;
            LoadStyle(colorPalettes[currentStyle]);
        }


    }
}