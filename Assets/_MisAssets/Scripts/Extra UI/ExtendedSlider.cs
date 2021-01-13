using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExtendedSlider : Slider
{

    public UnityEvent OnPointerEnterEvent = new UnityEvent();

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OnPointerEnterEvent?.Invoke();
    }
}
