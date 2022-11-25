using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Data;

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
    [SerializeField] private Vector3[] pathIn;
    [SerializeField] private Vector3[] pathOut;

    [Header("SFX Settings")]
    [SerializeField] private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        if (VisitorIsWaiting)
        {
            currentPatient -= Time.deltaTime;

            if (currentPatient < patientPercentage && currentVisitorStatus != VisitorStatus.Impatient)
            {
                UpdateStatus(VisitorStatus.Impatient);
            }
            if (currentPatient <= 0 && currentVisitorStatus != VisitorStatus.Angry) 
            {
                UpdateStatus(VisitorStatus.Angry);
                _audio.PlayOneShot(AudioManager.Instance.GetSound("Sad"));
                VisitorOut(); 
            }                         
        }
    }

    public void Initialized()
    {
        VisitorAvatar.rectTransform.localPosition = new Vector3(65, 0, 0);
        VisitorAvatar.color = Color.black;
    }

    public void LoadVisitorInfo(Visitor newVisitor, float patient) 
    {
        visitorData = newVisitor;
        currentPatient = patient;
        patientPercentage = patient / 2;
        VisitorServed = false;

        UpdateStatus(VisitorStatus.Normal);

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
            UIManager.Instance.HitDoor();
    }

    private void VisitorSpeak()
    {
        List<string> message = visitorData.Dialog;
        DialogSystem.Instance.WriteText(message);
    }

    private void UpdateStatus(VisitorStatus status) 
    {
        currentVisitorStatus = status;
        switch (currentVisitorStatus) 
        {
            case VisitorStatus.Normal: VisitorAvatar.sprite = visitorData.Normal; break;
            case VisitorStatus.Impatient: VisitorAvatar.sprite = visitorData.Impatient; break;
            case VisitorStatus.Happy: VisitorAvatar.sprite = visitorData.Happy; break;
            case VisitorStatus.Angry: VisitorAvatar.sprite = visitorData.Angry; break;
        }
        if (currentVisitorStatus != VisitorStatus.Normal)
            DOTween.Sequence().SetEase(Ease.Linear).SetLoops(3)
                .Append(transform.DOLocalMoveY(4, .15f))
                .Append(transform.DOLocalMoveY(0, .15f));
    }

    private void VisitorIn()
    {
        VisitorAvatar.rectTransform.localPosition = new Vector3(65, 0, 0);
        VisitorAvatar.color = Color.black;
        VisitorAvatar.raycastTarget = false;

        visitorAnimation = DOTween.Sequence().SetEase(Ease.Linear);
        visitorAnimation
            .Append(VisitorAvatar.rectTransform.DOLocalPath(pathIn, 1f, PathType.Linear))
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
            .Append(VisitorAvatar.rectTransform.DOLocalPath(pathOut, 1f, PathType.Linear))
            .OnComplete(() => {
                GameManager.Instance.CheckVisitorStatus(currentVisitorStatus);
            });
    }  

    public void OnPointerClick(PointerEventData eventData)
    {
        VisitorSpeak();
        _audio.PlayOneShot(AudioManager.Instance.GetSound("Click"));
    }

    public void OnDrop(PointerEventData eventData)
    {
        _audio.PlayOneShot(AudioManager.Instance.GetSound("Drop"));
        Debug.Log("DROP IN: " + eventData.pointerDrag.name);
        Candy candyDroped = eventData.pointerDrag.GetComponent<Candy>();
        candyDroped.DeactiveCandy();
        CheckCandyLiked(candyDroped);
        GameManager.Instance._candiesGived++;
    }

    private void CheckCandyLiked(Candy candy) 
    {
        bool liked = false;
        foreach(CandyType type in visitorData.CandiesLike)
            if(candy.Type == type)
                liked = true;

        if (liked)
        {
            UpdateStatus(VisitorStatus.Happy);
            _audio.PlayOneShot(AudioManager.Instance.GetSound("Happy"));
            GameManager.Instance._candiesCorrects++;
        }
        else 
        {
            UpdateStatus(VisitorStatus.Angry);
            _audio.PlayOneShot(AudioManager.Instance.GetSound("Sad"));
            GameManager.Instance._candiesIncorrects++;
        }

        VisitorOut();
    }
}

public enum VisitorStatus { Normal = 0, Impatient = 1, Happy = 2, Angry = 3 }

