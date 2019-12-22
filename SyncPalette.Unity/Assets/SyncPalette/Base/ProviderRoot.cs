using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{

    /// <summary>
    /// This class exists so its easier to find provides (generic abstract classes are not good FindObjectsOFType material)
    /// </summary>
    public abstract class ProviderRoot : MonoBehaviour
    {
         
        static Dictionary<System.Type, IPaletteProvider> foundProviders;

        public static IPaletteProvider FindProvider(System.Type T)
        {
            if (foundProviders == null) foundProviders = new Dictionary<System.Type, IPaletteProvider>();
            IPaletteProvider dictFound;
            if (foundProviders.TryGetValue(T, out dictFound))
            {
                if (dictFound == null)
                    foundProviders.Remove(T);
                else
                    return dictFound;
            }
            var providers = GameObject.FindObjectsOfType<ProviderRoot>();//as IPaletteProvider[];
            foreach (var p in providers)
            {
                if (p as IPaletteProvider != null)
                {
                    if ((p as IPaletteProvider).IsOfType(T))
                    {
                        foundProviders.Add(T, p as IPaletteProvider);
                        return p as IPaletteProvider;
                    }
                }
                else Debug.Log(" provider root not being a provider");
            }
            return null;
        }

    }
}