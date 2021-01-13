using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordText : MonoBehaviour
{
    public TextMeshProUGUI auxText;
    public TextMeshProUGUI recordText;

    
    IEnumerator WaitUntilDataIsLoaded()
    {
        yield return new WaitUntil(() => SaveLoadManager.Instance.dataLoaded);
        if (recordText)
        {
            recordText.text = RecordMessage;
        }

    }

    public void OnEnable()
    {
        //StartCoroutine(WaitUntilDataIsLoaded());
    }

    private void Update()
    {
        if (recordText)
        {
            recordText.text = RecordMessage;
        }
    }

    public string RecordMessage
    {
        get
        {
            if(auxText)
            {
                return string.Format("{0} <size=+20>{1}</size>", auxText.text, CurrentRecord);
            }
            return ""; 
        }
    }

    public int CurrentRecord
    {
        get
        {
            if (ScoreManager.record != null)
            {
                return ScoreManager.record.score;
            }
            return 0;
        }
    }
}
