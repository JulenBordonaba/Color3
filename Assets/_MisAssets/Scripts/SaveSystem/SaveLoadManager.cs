using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public string settingsPath = "Settings.json";

    [HideInInspector]
    public bool dataLoaded = false;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 30;
        LoadData();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }


    public void SaveData()
    {
        if (!dataLoaded) return;

        ConfigurationManager.settings.SaveData(settingsPath);
    }

    public void LoadData()
    {
        ConfigurationSettings settings = settingsPath.LoadData<ConfigurationSettings>();
        if (settings == null)
        {
            ConfigurationManager.settings = new ConfigurationSettings();
        }
        else
        {
            ConfigurationManager.settings = settings;
        }

        dataLoaded = true;
    }
}
