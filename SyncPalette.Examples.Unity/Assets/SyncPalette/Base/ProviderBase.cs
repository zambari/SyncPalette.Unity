using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{
    [ExecuteInEditMode]
    public abstract class ProviderBase<TypePal, TNamed, TypeVal> : ProviderRoot, INamedSelectionProvider, IPaletteProvider
    where TNamed : NamedValue<TypeVal>, new()
    where TypePal : PaletteBase<TNamed, TypeVal>
    {
        [SerializeField] [HideInInspector] TypePal lastPalette;
        [SerializeField] TypePal _palette;
        protected List<IPaletteUpdater> listeners;

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
                SendPaletteUpdates();
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
        void SendPaletteUpdates()
        {
            lastPalette = _palette;
            if (listeners == null)
            {
                Debug.Log("lisneres list does not exist"); return;
            }
            for (int i = 0; i < listeners.Count; i++)
                listeners[i].UpdatePalette();
            if (listeners.Count == 0)
            {
                Debug.Log(" we have no listeners!!");
            }
            else
            {
                // Debug.Log("sent update to " + listeners.Count + " listenrers");
            }
        }
        protected virtual void OnValidate()
        {
            if (palette == null)
                _palette = FindDefaultTemplate();
            if (palette != lastPalette)
            {
                Debug.Log("palette change detected");
                SendPaletteUpdates();
            }
            //  palette = _palette;
        }
        public void RegisterListener(IPaletteUpdater source)
        {
            if (listeners == null) listeners = new List<IPaletteUpdater>();
            if (listeners.Contains(source))
            {
                // Debug.Log(Time.frameCount + " we already have ths soruce", source.gameObject);
            }
            else
            {

                // Debug.Log(Time.frameCount + " adding " + source.name,source.gameObject);
                listeners.Add(source);
            }
        }

        public void UnRegisterListener(IPaletteUpdater source)
        {
            //Debug.Log(Time.frameCount+" unregregistering listener "+source.name,source.gameObject);
            //onPaletteChange-=source.UpdatePalette;
            if (listeners == null) listeners = new List<IPaletteUpdater>();
            if (!listeners.Contains(source))
            {
                Debug.Log("list does not contain this object " + source.name, source.gameObject);
            }
            listeners.Remove(source);
        }
    }

}