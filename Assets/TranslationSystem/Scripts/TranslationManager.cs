using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace TranslationSystem
{
    public class TranslationManager : Singleton<TranslationManager>
    {
        [HideInInspector]
        //The event called whenever the lenguage is changed
        public event Action<string> OnLenguageChanged;

        public event Action OnDataLoaded;

        //the dictionary of lenguages
        public Dictionary<string, Dictionary<string, string>> translations;

        public TranslationData data;

        public string currentLenguage;
        [HideInInspector]
        public bool loaded = false;

        protected override void Awake()
        {
            base.Awake();
            
            loaded = false;
            LoadLenguageData();
            StartCoroutine(SetSavedLenguage());

        }

        public void LoadLenguageData()
        {

            translations = data.DataToDictionary();
            
            loaded = true;
            OnDataLoaded?.Invoke();
        }

        
        
        public void LoadLenguageDataEditor()
        {
            data.LoadLenguageData();
        }
        
        public IEnumerator SetSavedLenguage()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => loaded && SaveLoadManager.Instance.dataLoaded);

            string lenguage = ConfigurationManager.settings.lenguage;

            print("El idioma almacenado es: " + ConfigurationManager.settings.lenguage);

            if (lenguage == "")
            {
                if (translations.Count > 0)
                {
                    lenguage = translations.Keys.ElementAt(0);
                }
            }

            currentLenguage = lenguage;

            ChangeLenguage(currentLenguage);
        }

        public void ChangeLenguage(string lenguageID)
        {
            if (lenguageID == "") return;

            print("ChangeLenguage " + lenguageID);
            currentLenguage = lenguageID;

            {
                ConfigurationManager.settings.lenguage = currentLenguage;
            }
            OnLenguageChanged?.Invoke(lenguageID);
        }

        
    }


}


