using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class VisitorController : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] private Visitor visitorData;
    private float patientPercentage;
    private float currentPatient;
    [SerializeField] private VisitorStatus currentVisitorStatus = VisitorStatus.Normal;
    [SerializeField] private bool VisitorIsWaiting = false;
    [SerializeField] private bool VisitorServed = false; // Fue atendido este visitante?
    [Header("Visitor UI Settings")]
    [SerializeField] private Image VisitorAvatar;
    private Sequence visitorAnimation;

    [Header("SFX Settings")]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip sfxHappy;
    [SerializeField] private AudioClip sfxAngry;

    // Start is called before the first frame update
    void Start()
    {
        VisitorAvatar.rectTransform.localPosition = new Vector3(65, 0, 0);
        VisitorAvatar.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (VisitorIsWaiting)
        {
            currentPatient -= Time.deltaTime;

            if (currentPatient < patientPercentage)
            {
                VisitorAvatar.sprite = visitorData.Impatient;
                currentVisitorStatus = VisitorStatus.Impatient;
            }
            if (currentPatient <= 1) 
            { 
                VisitorAvatar.sprite = visitorData.Angry;               
            }
            if (currentPatient <= 0) 
            { 
                currentVisitorStatus = VisitorStatus.Angry;
                _audio.PlayOneShot(sfxAngry);
                VisitorOut(); 
            }                         
        }
    }

    public void LoadVisitorInfo(Visitor newVisitor, float patient) 
    {
        visitorData = newVisitor;
        currentPatient = patient;
        patientPercentage = patient / 2;
        VisitorServed = false;

        VisitorAvatar.sprite = visitorData.Normal;
        currentVisitorStatus = VisitorStatus.Normal;

        VisitorIsWaiting = true;
    }

    public void VisitorInDoor(bool door) 
    {
        if (VisitorServed == false && door == true)
        {
            VisitorServed = true;
            VisitorIn();
        }
        else if (VisitorServed == false && door == false)
            DialogSystem.Instance.WriteText("TOC TOC TOC!!!");
    }

    private void VisitorSpeak()
    {
        string message = visitorData.Dialog;
        DialogSystem.Instance.WriteText(message);
    }

    private void VisitorIn()
    {
        VisitorAvatar.rectTransform.localPosition = new Vector3(65, 0, 0);
        VisitorAvatar.color = Color.black;
        VisitorAvatar.raycastTarget = false;

        visitorAnimation = DOTween.Sequence().SetEase(Ease.Linear);
        visitorAnimation
            .Append(VisitorAvatar.rectTransform.DOLocalMove(Vector3.zero, .3f))
            .Append(VisitorAvatar.DOColor(Color.white, .3f))
            .OnComplete(()=> { 
                VisitorSpeak();
                VisitorAvatar.raycastTarget = true;
            });
    }

    private void VisitorOut()
    {
        VisitorIsWaiting = false;
        VisitorAvatar.raycastTarget = false;

        visitorAnimation = DOTween.Sequence().SetEase(Ease.Linear).SetDelay(1);
        visitorAnimation
            .Append(VisitorAvatar.DOColor(Color.black, .3f))
            .Append(VisitorAvatar.rectTransform.DOLocalMove(new Vector3(-65, 0, 0), .3f))            
            .OnComplete(() => {
                GameManager.Instance.CheckVisitorStatus(currentVisitorStatus);
            });
    }  

    public void OnPointerClick(PointerEventData eventData)
    {
        VisitorSpeak();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROP IN: " + eventData.pointerDrag.name);
        Candy candyDroped = eventData.pointerDrag.GetComponent<Candy>();
        CheckCandyLiked(candyDroped);
    }

    private void CheckCandyLiked(Candy candy) 
    {
        bool liked = false;
        foreach(CandyType type in visitorData.CandiesLike)
            if(candy.Type == type)
                liked = true;

        if (liked)
        {
            VisitorAvatar.sprite = visitorData.Happy;
            currentVisitorStatus = VisitorStatus.Happy;
            _audio.PlayOneShot(sfxHappy);
        }
        else 
        {
            VisitorAvatar.sprite = visitorData.Angry;
            currentVisitorStatus = VisitorStatus.Angry;
            _audio.PlayOneShot(sfxAngry);
        }

        VisitorOut();
    }
}

public enum VisitorStatus { Normal = 0, Impatient = 1, Happy = 2, Angry = 3 }

