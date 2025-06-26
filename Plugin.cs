using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zorro.Core;

namespace CustomCountry
{
    [BepInPlugin("radsi.country", "CustomCountry", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private static ConfigEntry<string> country;

        private class Patcher
        {
            [HarmonyPatch(typeof(PassportManager), "Awake")]
            [HarmonyPostfix]
            public static void PassportManagerAwakePostfix()
            {
                var textObject = GameObject.Find("GAME/PassportManager/PassportUI/Canvas/Panel/Panel/BG/Text/Nationality/Text");
                if (textObject != null)
                {
                    var text = textObject.GetComponent<TextMeshProUGUI>();
                    if (text != null)
                        text.text = country.Value;
                }
            }
        }

        void Awake()
        {
            country = Config.Bind("General", "Country", "Skyland", "Text to display as nationality in the passport.");
            new Harmony("radsi.country").PatchAll(typeof(Patcher));
        }
    }
}