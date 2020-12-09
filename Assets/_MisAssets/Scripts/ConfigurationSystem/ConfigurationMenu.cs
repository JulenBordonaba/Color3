using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtraUI;

public class ConfigurationManager : MonoBehaviour
{
    public static ConfigurationSettings settings;

    [Header("Graphics")]
    public Slider brightnessSlider;
    public Slider vSyncSlider;
    public Slider antialiasingSlider;
    public Slider textureQualitySlider;
    public Slider shadowResolutionSlider;

    [Header("Controller")]
    public Slider sensitivitySlider;
    public Slider invertControlsSlider;
    public Slider controllerSlider;

    [Header("Music")]
    public Slider generalVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;

    public Dictionary<int, bool> intToBool = new Dictionary<int, bool>()
    {
        {0,false },
        {1,true }
    };

    public Dictionary<bool, int> boolToInt = new Dictionary<bool, int>()
    {
        {false,0 },
        {true,1 }
    };

    private static bool slidersSet = false;


    IEnumerator WaitUntilDataIsLoaded()
    {
        yield return new WaitUntil(() => SaveLoadManager.Instance.dataLoaded);
        SetQualitySettings();
        SetSliderValues();

    }
    
    public void OnEnable()
    {
        StartCoroutine(WaitUntilDataIsLoaded());
    }

    private void OnDisable()
    {
        slidersSet = false;
    }

    public void SetQualitySettings()
    {
        RenderSettings.ambientLight = new Color(settings.brightness, settings.brightness, settings.brightness, 1f);
        QualitySettings.vSyncCount = (int)settings.vSync;
        QualitySettings.antiAliasing = (int)settings.antialiasing * 2;
        QualitySettings.masterTextureLimit = Mathf.Abs((int)settings.textureQuality - 3);
        QualitySettings.shadowResolution = (UnityEngine.ShadowResolution)settings.shadowResolution;
    }


    private void Update()
    {
        if (!slidersSet) return;
        ChangeSensitivity();
        ChangeGeneralVolume();
        ChangeEffectVolume();
        ChangeMusicVolume();
    }

    public void SetSliderValues()
    {
        

        //sonido
        if (musicVolumeSlider != null)
            musicVolumeSlider.Value = settings.musicVolume;
        if (effectVolumeSlider != null)
            effectVolumeSlider.Value = settings.effectVolume;
        if (generalVolumeSlider != null)
            generalVolumeSlider.Value = settings.generalVolume;

        //graphics
        if (vSyncSlider != null)
            vSyncSlider.Value = settings.vSync;
        if (brightnessSlider != null)
            brightnessSlider.Value = settings.brightness;
        if (antialiasingSlider != null)
            antialiasingSlider.Value = settings.antialiasing;
        if (textureQualitySlider != null)
            textureQualitySlider.Value = settings.textureQuality;
        if (shadowResolutionSlider != null)
            shadowResolutionSlider.Value = settings.shadowResolution;

        //controller
        if (controllerSlider != null)
            controllerSlider.Value = (int)settings.controllerType;
        if (invertControlsSlider != null)
            invertControlsSlider.Value = boolToInt[settings.inverted];
        if (sensitivitySlider != null)
            sensitivitySlider.Value = settings.sensitivity.y;

        slidersSet = true;
    }


    #region Music

    public void ChangeMusicVolume()
    {
        settings.musicVolume = musicVolumeSlider.Value;
    }

    public void ChangeEffectVolume()
    {
        settings.effectVolume = effectVolumeSlider.Value;
    }

    public void ChangeGeneralVolume()
    {
        settings.generalVolume = generalVolumeSlider.Value;
    }

    #endregion

    #region Controller

    public void ChangeSensitivity()
    {
        settings.sensitivity.x = sensitivitySlider.Value;
        settings.sensitivity.y = sensitivitySlider.Value;
    }

    public void ChooseController()
    {
        settings.controllerType = (ControllerType)controllerSlider.Value;
    }

    public void Inverted()
    {
        settings.inverted = intToBool[(int)invertControlsSlider.Value];
    }

    #endregion

    #region Graphics

    public void TextureQuality()
    {
        settings.textureQuality = textureQualitySlider.Value;
        SetQualitySettings();
    }

    public void ShadowResolution()
    {
        settings.shadowResolution = shadowResolutionSlider.Value;
        SetQualitySettings();
    }

    public void Brightness()
    {
        settings.brightness = brightnessSlider.Value;
        SetQualitySettings();
    }

    public void VSync()
    {
        settings.vSync = (int)vSyncSlider.Value;
        SetQualitySettings();
    }

    public void AntiAliasing(bool active)
    {
        settings.antialiasing = (int)antialiasingSlider.Value;
        SetQualitySettings();
    }

    #endregion

}

[System.Serializable]
public class ConfigurationSettings
{
    public float brightness = RenderSettings.ambientLight.r;
    public float vSync = QualitySettings.vSyncCount;
    public float antialiasing = QualitySettings.antiAliasing / 2f;
    public float textureQuality = Mathf.Abs(QualitySettings.masterTextureLimit - 3);
    public float shadowResolution = (int)QualitySettings.shadowResolution;

    public bool inverted = false;
    public float generalVolume = 1;
    public float musicVolume = 1;
    public float effectVolume = 1;
    public Vector2 sensitivity = Vector2.one;
    public ControllerType controllerType = ControllerType.PS4;
}
