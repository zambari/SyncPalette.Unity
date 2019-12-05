using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{

    // 
    // To inherit of this class, first create a Palette and a provider
    // the Palette should supply type TNamed while itself being type TPalette
    //

    //IGetCurrentValue
    [ExecuteInEditMode]
    public abstract class SyncBase<TPalette, TNamed, TValue> : MonoBehaviour, IValueSync, IAddCurrentValue
    where TNamed : NamedValue<TValue>, new()
     where TPalette : PaletteBase<TNamed, TValue>
    {
        public NameSelectionDrawer nameSelection;
        IPaletteProvider myProvider
        {
            get
            {
                if (_myProvider == null)
                    _myProvider = ProviderRoot.FindProvider(typeof(TNamed));
                if (_myProvider == null) Debug.Log("please add provider for " + typeof(TNamed).ToString());
                return _myProvider;
            }
        }
        IPaletteProvider _myProvider;

        TPalette _palette;

        public TPalette Palette
        {
            get
            {
                if (myProvider != null)
                    return myProvider.palette as TPalette;
                return null;
            }
        }

        public string presetName
        {
            get
            {
                if (nameSelection == null) nameSelection = new NameSelectionDrawer();
                return nameSelection.value;
            }
            set
            {
                if (nameSelection == null) nameSelection = new NameSelectionDrawer();
                nameSelection.value = value;
                UpdateValueIfEnabled();
            }
        }


        public System.Type GetNamedType()
        {
            return typeof(TNamed);

        }
        public System.Type GetValueType()
        {
            return typeof(TValue);

        }
        public System.Type GetPaletteType()
        {
            return typeof(TPalette);
        }
        public virtual void UpdateValue() { }

        public void UpdateValueIfEnabled()
        {
            if (isActiveAndEnabled) UpdateValue();
        }
        public NameSelectionDrawer GetDrawer() { return nameSelection; }
        public virtual void AddCurrentValue()
        {
            var newName = Palette.AddValue(GetCurrentValue());
            if (!string.IsNullOrEmpty(newName))
            {
                nameSelection.value = newName;
                nameSelection.requestAdd = false;
            }
            if (!isActiveAndEnabled) return;
            UpdateValueIfEnabled();
        }
        protected virtual void Start()
        {
            //
            myProvider.onPaletteChange -= UpdatePalette;
            myProvider.onPaletteChange += UpdatePalette;
        }
        public void UpdatePalette()
        {
            //  if (_palette != null) _palette.onValueChanged -= UpdateValue;

            //            else Debug.Log("i dont have provider");
            if (Palette != null)
            {
                Palette.onValueChanged -= UpdateValue;
                Palette.onValueChanged += UpdateValue;
                UpdateValue();
            }
            else
            {
                Debug.Log("no pal " + typeof(TPalette).ToString());
            }

        }

        protected virtual void Reset()
        {
            CheckPreset();
        }
        void CheckPreset()
        {
            if (Palette != null)
            {
                if (nameSelection == null) nameSelection = new NameSelectionDrawer();
                TValue currentValue = GetCurrentValue();
                string foundName = Palette.FindName(currentValue);
                if (string.IsNullOrEmpty(foundName))
                    nameSelection.requestAdd = true;
                else
                    nameSelection.value = foundName;
            }
            else Debug.Log("no Palette present");
        }
        public virtual TValue GetCurrentValue()
        {
            return default(TValue);
        }

        // public virtual TNamed GetCurrentValue1<TNamed>()
        // {
        //     Debug.Log("caled get value !!! yay");
        //     return default(TNamed);
        // }


        protected virtual void OnEnable()
        {
            if (myProvider != null)
            {
                myProvider.onPaletteChange -= UpdatePalette;
                myProvider.onPaletteChange += UpdatePalette;
                UpdatePalette();
            }
            //         myProvider.onPaletteChange += UpdatePalette;
            // if (Palette != null)
            // {
            //     Palette.onValueChanged -= UpdateValue;
            //     Palette.onValueChanged += UpdateValue;

            // }
            // if (myProvider != null)
            // {
            //     myProvider.onPaletteChange -= UpdatePalette;
            //     myProvider.onPaletteChange += UpdatePalette;
            // }
            //    UpdateValue();
        }

        protected virtual void OnValidate()
        {
            if (!isActiveAndEnabled) return;

            if (Palette != null)
            {
                Palette.onValueChanged -= UpdateValue;
                Palette.onValueChanged += UpdateValue;
            }
            UpdateValue();
        }

        protected virtual void OnDisable()
        {
            if (Palette != null)
            {
                Palette.onValueChanged -= UpdateValue;
            }
            if (myProvider != null)
                myProvider.onPaletteChange -= UpdatePalette;
        }
    }

}
