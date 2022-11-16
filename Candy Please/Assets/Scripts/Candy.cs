using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Candy : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler
{
    [Header("Type of Candy")]
    public CandyType Type;
    [Header("Description of the candy")]
    [TextArea(2, 10)]public string Description;
    [Header("Image of the Candy")]
    public Image Icon;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        CandiesManager.Instance.BeginDragCandy(this, transform.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        CandiesManager.Instance.EndDragCandy();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DialogSystem.Instance.WriteText(Description);
    }

    public void OnDrag(PointerEventData eventData) // Necessary for Drag.
    {
        //RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform rectTransform = CandiesManager.Instance.CandyData();
        Canvas canvas = FindObjectOfType<Canvas>();
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
    GrapeCandy
}
