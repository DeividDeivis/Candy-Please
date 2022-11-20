using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndState : State
{
    [Header("Screen Setting")]
    [SerializeField] private Image EndScreenBG;
    [SerializeField] private Button MenuBtn;
    [SerializeField] private AudioSource m_audio;

    [Header("Win Setting")]
    [SerializeField] private Sprite WinBG;

    [Header("Lose Setting")]
    [SerializeField] private Sprite LoseBG;

    public override void OnEnterState() 
    {
        if (GameManager.Instance.GameStatus == gameStatusType.Win)
        {
            EndScreenBG.sprite = WinBG;
            m_audio.PlayOneShot(AudioManager.Instance.GetSound("Victory"));
        }
        else if (GameManager.Instance.GameStatus == gameStatusType.Lose)
        {
            EndScreenBG.sprite = LoseBG;
            m_audio.PlayOneShot(AudioManager.Instance.GetSound("Defeat"));
        }

        MenuBtn.onClick.AddListener(() => {
            m_audio.PlayOneShot(AudioManager.Instance.GetSound("Click"));
            GameManager.Instance.SetState(0); 
        });
        Animation();
    }
    public override void OnUpdateState() { }
    public override void OnExitState() 
    {
        GameManager.Instance.EndGame();
    }

    private void Animation() 
    {
        EndScreenBG.color = new Color(0, 0, 0, 1);
        MenuBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        MenuBtn.transform.localScale = new Vector3(.3f, .3f, 1f);

        Sequence MenuSequence = DOTween.Sequence().SetEase(Ease.Linear);
        MenuSequence
            .Append(EndScreenBG.DOColor(Color.white, 1f))
            .Append(MenuBtn.GetComponent<Image>().DOFade(1, .15f))
            .Join(MenuBtn.transform.DOScaleY(1.3f, .15f))
            .Append(MenuBtn.transform.DOScaleX(1.3f, .15f))
            .Join(MenuBtn.transform.DOScaleY(1f, .15f))
            .Append(MenuBtn.transform.DOScaleX(1f, .15f));
    }
}

public enum gameStatusType { Win, Lose }
