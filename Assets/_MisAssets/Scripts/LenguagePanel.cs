using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TranslationSystem;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class LenguagePanel : MonoBehaviour
{
    public float transitionDuration = 0.3f;

    public LenguageButton[] lenguages;

    public bool isOpen = false;

    private LenguageButton[] lenguagesInOrder;


    private RectTransform rectTransform;

    private Vector2 sizeDelta;

    private float firstPosition;

    private void Awake()
    {
        SetOrderedList();
        rectTransform = GetComponent<RectTransform>();
        sizeDelta = rectTransform.sizeDelta;
        firstPosition = rectTransform.anchoredPosition.x;
        foreach(LenguageButton lenguage in lenguages)
        {
            lenguage.panel = this;
        }

        
    }

    private void SetOrderedList()
    {
        lenguagesInOrder = new LenguageButton[lenguages.Length];
        for (int i = 0; i < lenguagesInOrder.Length; i++)
        {
            lenguagesInOrder[i] = lenguages[i];
        }
    }

    private void DefaultLenguageOrder()
    {
        for (int i = 0; i < lenguagesInOrder.Length; i++)
        {
            lenguages[i] = lenguagesInOrder[i];
        }

        foreach (LenguageButton lenguage in lenguages)
        {
            lenguage.rectTransform.SetAsFirstSibling();
        }
    }

    private void OnEnable()
    {
        TranslationManager.Instance.OnDataLoaded += OrderLenguages;
        TranslationManager.Instance.OnLenguageChanged += ChangeLenguage;

        if(TranslationManager.Instance.loaded)
        {
            OrderLenguages();
        }
        
    }

    private void OnDisable()
    {
        TranslationManager.Instance.OnDataLoaded -= OrderLenguages;
        TranslationManager.Instance.OnLenguageChanged -= ChangeLenguage;
    }

    private void OnDestroy()
    {
        TranslationManager.Instance.OnDataLoaded -= OrderLenguages;
        TranslationManager.Instance.OnLenguageChanged -= ChangeLenguage;
    }

    public void OrderLenguages()
    {
        int current = -1;
        DefaultLenguageOrder();
        for (int i = 0; i < lenguagesInOrder.Length; i++)
        {
            if (string.Compare(lenguages[i].lenguageID, TranslationManager.Instance.currentLenguage) == 0)
            {
                //print(lenguages[i].lenguageID + " " + i);
                current = i;
            }
        }
        if(current!=-1)
        {
            SetFirst(current);
        }
        
    }

    public void Open()
    {
        
        float width = sizeDelta.x;

        if(lenguages.Length>0)
        {
            width = lenguages[0].rectTransform.sizeDelta.x * lenguages.Length;
        }

        Tween widthTween = rectTransform.DOSizeDelta(new Vector2(width, sizeDelta.y), transitionDuration);
        Tween xTween = rectTransform.DOAnchorPosX(firstPosition + (width*0.5f), transitionDuration);
        for (int i = 0; i < lenguages.Length; i++)
        {
            lenguages[i].SetPosition(i,transitionDuration);
        }
        isOpen = true;
    }

    private void SetFirst(int pos)
    {
        if (pos <= 0) return;

        if(pos<lenguages.Length)
        {
            for (int i = pos; i > 0; i--)
            {
                print(lenguages[i - 1] + " <=> " + lenguages[i]);

                LenguageButton aux = lenguages[i - 1];
                lenguages[i - 1] = lenguages[i];
                lenguages[i] = aux;
            }
        }
        

        foreach (LenguageButton lenguage in lenguages)
        {
            lenguage.rectTransform.SetAsFirstSibling();
        }

    }

    public void Close()
    {
        Tween panelTween = rectTransform.DOSizeDelta(sizeDelta, transitionDuration).OnComplete(OrderLenguages);
        Tween xTween = rectTransform.DOAnchorPosX(firstPosition, transitionDuration);
        for (int i = 0; i < lenguages.Length; i++)
        {
            lenguages[i].SetPosition(0, transitionDuration);
        }
        isOpen = false;
    }
    
    public void ChangeLenguage(string lenguageID)
    {
        OrderLenguages();
    }

    
    
}
