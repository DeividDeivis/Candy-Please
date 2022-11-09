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

    private Sequence MenuSequence;

    public override void OnEnterState() 
    {
        Startgame.onClick.AddListener(()=> 
        {
            m_audio.Stop();
            GameManager.Instance.StartGame();                              
        });
        m_audio.Play();

        Animation();     
    }

    public override void OnUpdateState() 
    { 
        
    }

    public override void OnExitState() 
    { 
        Startgame.onClick.RemoveAllListeners();
    }

    private void Animation() 
    {
        Startgame.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        Startgame.transform.localScale = new Vector3(.3f, .3f, 1);

        MenuSequence = DOTween.Sequence().SetEase(Ease.Linear).SetDelay(1f);
        MenuSequence
            .Append(Startgame.GetComponent<Image>().DOFade(1, .15f))
            .Join(Startgame.transform.DOScaleY(1.3f, .15f))
            .Append(Startgame.transform.DOScaleX(1.3f, .15f))
            .Join(Startgame.transform.DOScaleY(1f, .15f))
            .Append(Startgame.transform.DOScaleX(1f, .15f));
    }
}
