using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : State
{
    [SerializeField] private AudioSource m_audio;
    [SerializeField] private Button Startgame;
    [SerializeField] private RectTransform BGContainer;

    private Sequence MenuSequence;

    public override void OnEnterState() 
    {
        Startgame.onClick.AddListener(()=> {
            m_audio.PlayOneShot(AudioManager.Instance.GetSound("Click"));
            GameManager.Instance.NextState(); 
        });
        m_audio.Play();
        Animation();     
    }

    public override void OnUpdateState() 
    { 
        
    }

    public override void OnExitState() 
    {
        m_audio.Stop();
        Startgame.onClick.RemoveAllListeners();
    }

    private void Animation() 
    {
        Startgame.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        Startgame.transform.localScale = new Vector3(.3f, .3f, 1f);
        BGContainer.localPosition = new Vector3(0f, -120f, 0f);

        MenuSequence = DOTween.Sequence().SetEase(Ease.Linear);
        MenuSequence
            .Append(BGContainer.DOLocalMove(Vector3.zero, 3f))
            .Append(Startgame.GetComponent<Image>().DOFade(1, .15f))
            .Join(Startgame.transform.DOScaleY(1.3f, .15f))
            .Append(Startgame.transform.DOScaleX(1.3f, .15f))
            .Join(Startgame.transform.DOScaleY(1f, .15f))
            .Append(Startgame.transform.DOScaleX(1f, .15f));
    }
}
