using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VisitorController : MonoBehaviour
{
    [SerializeField] private Visitor visitorData;
    [SerializeField] private float patientPercentage;
    [SerializeField] private float currentPatient;
    [SerializeField] private VisitorStatus currentVisitorStatus = VisitorStatus.Normal;
    [SerializeField] private bool VisitorIsWaiting = false;
    [Header("Visitor UI Settings")]
    [SerializeField] private Image VisitorAvatar;
    [SerializeField] private Button VisitorBtn;
    private Sequence visitorAnimation;

    [Header("SFX Settings")]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip sfxHappy;
    [SerializeField] private AudioClip sfxAngry;

    // Start is called before the first frame update
    void Start()
    {
        VisitorBtn.onClick.AddListener(VisitorSpeak);
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
            if (currentPatient <= 3) 
            { 
                VisitorAvatar.sprite = visitorData.Angry;
                currentVisitorStatus = VisitorStatus.Angry;
            }
            if (currentPatient <= 0) 
                VisitorOut(true);           
        }
    }

    public void LoadVisitorInfo(Visitor newVisitor, float patient) 
    {
        visitorData = newVisitor;
        currentPatient = patient;
        patientPercentage = patient / 2;

        VisitorAvatar.sprite = visitorData.Normal;
        currentVisitorStatus = VisitorStatus.Normal;

        VisitorIn();
    }

    private void CheckVisitorStatus() 
    { 
        
    }

    public void VisitorSpeak()
    {
        string message = visitorData.Dialog;
        DialogSystem.Instance.WriteText(message);
    }

    private void VisitorIn()
    {
        VisitorAvatar.rectTransform.position = new Vector3(65, 0, 0);
        VisitorAvatar.color = Color.black;

        visitorAnimation = DOTween.Sequence().SetEase(Ease.Linear);
        visitorAnimation
            .Append(VisitorAvatar.rectTransform.DOLocalMove(Vector3.zero, 1f))
            .Join(VisitorAvatar.DOColor(Color.white, .3f));

        VisitorIsWaiting = true;
    }

    private void VisitorOut(bool TimeOut)
    {
        VisitorIsWaiting = false;

        visitorAnimation = DOTween.Sequence().SetEase(Ease.Linear);
        visitorAnimation
            .Append(VisitorAvatar.rectTransform.DOLocalMove(new Vector3(-65, 0, 0), 1f))
            .Join(VisitorAvatar.DOColor(Color.black, .3f))
            .OnComplete(() => 
            {
                if (TimeOut)
                    GameManager.Instance.AttackHouse();
            });
    }
}

public enum VisitorStatus { Normal, Impatient, Happy, Angry }

