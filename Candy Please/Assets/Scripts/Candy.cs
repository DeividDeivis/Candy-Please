using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Candy : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler
{
    [Header("Type of Candy")]
    public CandyType Type;
    [Header("Description of the candy")]
    [TextArea(2, 10)]public string description;
  

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DialogSystem.Instance.WriteText(description);
    }

    public void OnDrag(PointerEventData eventData) // Necessary for Drag.
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Canvas canvas = FindObjectOfType<Canvas>();
        rectTransform.anchoredPosition = eventData.delta / canvas.scaleFactor;
    }
}

public enum CandyType 
{ 
    Alfajor,
    ZeroCalories,
    PumpkinCandy,
    Chocolate,
    Flynpuff,
    StrawberryCandy,
    Cookie,
    IceCream,
    BoneCandy,
    Apple,
    Mate,
    MediaHora,
    Eye,
    CherryPalette,
    PinkPalette,
    GrapePalette
}
