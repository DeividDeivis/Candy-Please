using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Candy : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler
{
    [Header("Type of Candy")]
    public CandyType Type;
    [Header("Description of the candy")]
    [TextArea(2, 10)] public string Description;
    [Header("Image of the Candy")]
    public Image Icon;
    [Header("SFX")]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip pickUpSfx;
    [Header("Cooldown")]
    [SerializeField] private float pickUpCD;
    private float currentCD;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        CandiesManager.Instance.BeginDragCandy(this, transform.position);
        _audio.PlayOneShot(pickUpSfx);
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
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        rectTransform.position = Input.mousePosition;
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
