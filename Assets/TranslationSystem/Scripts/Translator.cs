using System.Collections;
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
            print("Change");
            if (!myText)
            {
                myText = GetComponent<TextMeshProUGUI>();
            }

            myText.text = TranslationManager.Instance.traductions[lenguageID][textID];

            print(TranslationManager.Instance.traductions[lenguageID][textID]);
        }
    }

}