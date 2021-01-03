﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TranslationSystem
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Translator : MonoBehaviour
    {
        [HideInInspector]
        public TextMeshProUGUI myText;

        public string textID;


        private void OnEnable()
        {
            if (!myText)
            {
                myText = GetComponent<TextMeshProUGUI>();
            }
            TranslationManager.Instance.OnLenguageChanged += ChangeLenguage;
            TranslationManager.Instance.ChangeLenguage(TranslationManager.Instance.currentLenguage);
            //ChangeLenguage(TranslationManager.Instance.currentLenguage);
        }

        private void OnDisable()
        {
            TranslationManager.Instance.OnLenguageChanged -= ChangeLenguage;
        }

        private void OnDestroy()
        {
            TranslationManager.Instance.OnLenguageChanged -= ChangeLenguage;
        }

        public void ChangeLenguage(string lenguageID)
        {
            if (string.Compare(lenguageID, "") == 0) return;

            if (!myText)
            {
                myText = GetComponent<TextMeshProUGUI>();
            }

            myText.text = TranslationManager.Instance.traductions[lenguageID][textID];
        }
    }

}