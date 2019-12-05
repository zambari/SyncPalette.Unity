using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{

    public abstract class ProviderBase<TypePal, TNamed, TypeVal> : ProviderRoot, INamedSelectionProvider, IPaletteProvider
    where TNamed : NamedValue<TypeVal>, new()
    where TypePal : PaletteBase<TNamed, TypeVal>
    {
        public System.Action onPaletteChange { get; set; }
        [SerializeField] [HideInInspector] TypePal lastPalette;
        [SerializeField] TypePal _palette;

        public bool IsOfType(System.Type T)
        {
            return T.Equals(typeof(TNamed));
        }
        public virtual void AddSyncer(GameObject where)
        {
        }
        public IPalette palette
        {
            get { return _palette; }
            set
            {
                _palette = value as TypePal;
                SentPaletteUpdates();
            }
        }
        void OnDisable()
        {

            if (palette != null)
            {
                palette.ClearEvents();
            }
        }

        protected virtual TypePal FindDefaultTemplate()
        {
            TypePal[] templates = Resources.FindObjectsOfTypeAll<TypePal>() as TypePal[];
            if (templates.Length == 1) return (templates[0]); else if (templates.Length > 1) Debug.Log("too many templates to choose from");
            Debug.Log("no Palettee found " + typeof(TypePal).ToString());
            return null;
        }
        public string[] GetNames(System.Type requestedType)
        {
            System.Type thistype = typeof(TypeVal);
            if (requestedType == thistype)
            {
                if (palette == null) palette = FindDefaultTemplate();
                if (palette != null)
                {
                    if (palette.validNames == null) Debug.Log("no valid names " + typeof(TNamed).ToString());
                    if (palette.validNames.Length == 0) Debug.Log("no valid names length=0");
                    return _palette.validNames;
                }
                else Debug.Log("no valid SO " + typeof(TNamed).ToString(), gameObject);
            }
            return null;
        }

        protected virtual void Reset()
        {
            _palette = FindDefaultTemplate();

        }
        void SentPaletteUpdates()
        {
                if (onPaletteChange != null)
                {
                    onPaletteChange.Invoke();
                }
                else
                {
//                      Debug.Log("no listnere");
                }
            lastPalette = _palette;
        }
        protected virtual void OnValidate()
        {
            if (palette == null)
                _palette = FindDefaultTemplate();
            if (palette != lastPalette)
            {
                Debug.Log("palette change detected");
                SentPaletteUpdates();
            }
            palette = _palette;
        }
    }

}