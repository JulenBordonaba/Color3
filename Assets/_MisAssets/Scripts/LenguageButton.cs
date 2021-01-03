using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TranslationSystem;

[RequireComponent(typeof(RectTransform))]
public class LenguageButton : MonoBehaviour
{
    public string lenguageID;
    [HideInInspector]
    public LenguagePanel panel;


    [HideInInspector]
    public RectTransform rectTransform;

    private float startX;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startX = rectTransform.anchoredPosition.x;
    }

    public void LenguageButtonOnClick()
    {
        if(string.Compare(lenguageID, TranslationManager.Instance.currentLenguage)!=0)
        {
            TranslationManager.Instance.ChangeLenguage(lenguageID);
        }

        panel.OrderLenguages();

        OpenClose();
    }

    private void OpenClose()
    {
        if(panel.isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void SetPosition(int position, float duration)
    {
        Tween tween = rectTransform.DOAnchorPosX(startX + (rectTransform.sizeDelta.x * position), duration);
    }

    private void Open()
    {
        panel.Open();
    }

    private void Close()
    {
        panel.Close();
    }

    public override string ToString()
    {
        return lenguageID;
    }

}
