using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using zUI;
namespace Z
{
    /*
	https://sequencediagram.org/index.html#initialData=C4S2BsFMAIAUCcD2A3EATS8DKBPAdgMbRogCGA5vKQLYBQeiwMKm0AcjZGgCoAWIecgC5oAdyoAHAM7QciAK7xoiAEYArSAWD1GzZKw7UuANVLh5kABTcAlCNJo0MvJ2gAzEJHBpowRLIVgXxwJSB0mZX0lPgFyWDMoYBEpUn0ZUjxoUngqHGU3aBcjH1UNLSlaQy4YwQBaAD4qtFNzK1sRAV5MMAqGCJZo-kEEFHRMEQkkVAx06AkEyCD4SDdMSEIwppaLaxsGmriFpOgpP2WZAROCeBAJYFIVKGV1TW0+vVYD3EIRAGFEPBSeRGGRyRTQA60A7xcCJfZDOJTMbwZLyCQScCeGTzWGLWhQhHfAjw2IjabjdwCJxZE5ojGeeD4r74Yn1aFHETLACOFlOsyKXGgYEg1GgKjypxuggJpKODSaBxEADEqaCFEplqtlhtnmVtAqESTBESRABxRazaikYAEBG617KJR4eSwoUFMDuUggcAVdm44BG8gm6BYdbUjLQSD6PBBURdTJoa2kaC2jLkSBSIA


title ProviderSync diagram
note over NamedThing: wraps your object
note over NamedValue(T): adds name field to yout type
note over ThingPalette: saves an array of named objects
NamedThing->NamedValue(T): inherits
note over ThingProvider: provides a Palette reference
NamedValue(T)->ThingPalette: stores in scriptable object
note over ThingSync: Consumes your Thing
ThingPalette->ThingProvider: supplies Palette

ThingSync->ThingProvider: finds a supplier

ThingSync->ThingPalette: requests a named item by string
ThingPalette->NamedThing: Finds your reference object
NamedThing->ThingSync: Gets a matching object or null if it fails
ThingPalette->ThingSync: Sends an event when data changes

	 */

    public class ProviderCreator : MonoBehaviour
    {
        [ExposeMethodInEditor]
        public void AddSyncersAndProviders()
        {
           // gameObject.AddOrGetComponent<FontProvider>();
            gameObject.AddOrGetComponent<ColorProvider>();
          //  gameObject.AddOrGetComponent<FontSizeProvider>();
            var texts = gameObject.GetComponentsInChildren<Text>();
            foreach (var t in texts)
            {
                if (!string.IsNullOrEmpty(t.text))
                {
                //    t.gameObject.AddOrGetComponent<FontSync>().presetName = "PanelTop";
                    t.gameObject.AddOrGetComponent<ColorSync>().presetName = "PanelTopText";
                //    t.gameObject.AddOrGetComponent<FontSizeSync>().presetName = "PanelTop";
                }
            }
            // var borders = gameObject.GetComponentsInChildren<LayoutBorderDragger>();
            // foreach (var b in borders)
            // {
            //     var bordersync = b.AddOrGetComponent<ColorSync>();
            //     bordersync.presetName = "Border";
            // }
            // var tops = gameObject.GetComponentsInChildren<LayoutTopControl>();
            // foreach (var t in tops)
            // {
            //     var bordersync = t.AddOrGetComponent<ColorSync>();
            //     bordersync.presetName = "PanelTop";
            // }
            // var pans = gameObject.GetComponentsInChildren<LayoutPanel>();
            // foreach (var t in pans)
            // {
            //     t.AddOrGetComponent<ColorSync>().presetName = "PanelBody";
            // }
        }
    }
}
