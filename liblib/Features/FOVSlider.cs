using AGUI.StackedUI;
using BestHTTP.Extensions;
using liblib.Core;
using liblib.Libraries.RadiumWrapper;
using liblib.Patches;
using liblib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace liblib.Features
{
    [Instance.AddOnAwake]
    internal class FOVSlider : MonoWrap
    {
        internal static FOVSlider instance;
        internal static Button3D DecrementalButton;
        internal static Button3D IncrementalButton;
        internal static TextMeshProUGUI Label;
        internal static int LabelNumber = 60;

        public void Awake()
        {
            instance = this;
            LabelNumber = PlayerPrefs.GetInt("FOV");
        }
        public void LateUpdate()
        {
            // please speed i need this
            var watch = GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)");
            if ((watch != null && !watch.activeInHierarchy) && GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/ToolMenu") == null)
                GameObject.Find("GameRoot/Startup/Core Systems/[RR CameraRig]/ScreenModeCamera").GetComponent<Camera>().fieldOfView = LabelNumber;

            if (DecrementalButton == null || IncrementalButton == null)
            {
                var screen = GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)/Canvas/Screens/[SettingsScreen](Clone)/Body/[ScreenGameplaySubscreen]");
                if (screen == null)
                    return;

                screen.transform.Find("Rockers/GamepadSensitivity/TitleText").GetComponent<TMPro.TextMeshProUGUI>().text = "Field of View";
                screen.transform.Find("Rockers/GamepadSensitivity").GetComponent<Tooltip>().Message = "Change Field of View";

                Label = screen.transform.Find("Rockers/GamepadSensitivity/Controls/Label").GetComponent<TMPro.TextMeshProUGUI>();
                DecrementalButton = screen.transform.Find("Rockers/GamepadSensitivity/Controls/Buttons/DecrementButton").GetComponent<Button3D>();
                IncrementalButton = screen.transform.Find("Rockers/GamepadSensitivity/Controls/Buttons/IncrementButton").GetComponent<Button3D>();
            }

            // i wanna bet this is gonna fuck something up but i dont care
            if (GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)/Canvas/Screens/[SettingsScreen](Clone)/Body/[ScreenGameplaySubscreen]/Rockers/GamepadSensitivity").active == false)
                GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)/Canvas/Screens/[SettingsScreen](Clone)/Body/[ScreenGameplaySubscreen]/Rockers/GamepadSensitivity").SetActive(true);

            Label.text = LabelNumber.ToString();
            if (LabelNumber < 30)
                LabelNumber = 30;
            if (LabelNumber > 120)
                LabelNumber = 120;

            // stupid buttons
            DecrementalButton.OnClick = new Button3D.OnButton3DClick();
            DecrementalButton.OnClick.RemoveAllListeners();
            DecrementalButton.OnClick.AddListener(new Action(() => { LabelNumber -= 5; PlayerPrefs.SetInt("FOV", LabelNumber); PlayerPrefs.Save(); }));

            IncrementalButton.OnClick = new Button3D.OnButton3DClick();
            IncrementalButton.OnClick.RemoveAllListeners();
            IncrementalButton.OnClick.AddListener(new Action(() => { LabelNumber += 5; PlayerPrefs.SetInt("FOV", LabelNumber); PlayerPrefs.Save(); }));
        }
    }
}
