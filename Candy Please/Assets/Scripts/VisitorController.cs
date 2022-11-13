using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VisitorController : MonoBehaviour
{
    [SerializeField] private Visitor visitorData;
    [SerializeField] private float currentPatient;
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
        
    }

    public void LoadVisitorInfo(Visitor newVisitor, float patient) 
    {
        visitorData = newVisitor;
        currentPatient = patient;

        VisitorAvatar.sprite = visitorData.Normal;

        VisitorIn();
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
    }

    private void VisitorOut()
    {
        visitorAnimation = DOTween.Sequence().SetEase(Ease.Linear);
        visitorAnimation
            .Append(VisitorAvatar.rectTransform.DOLocalMove(new Vector3(-65, 0, 0), 1f))
            .Join(VisitorAvatar.DOColor(Color.black, .3f));
    }
}
