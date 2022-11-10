using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndState : State
{
    [Header("Screen Setting")]
    [SerializeField] private Image EndScreenBG;
    [SerializeField] private AudioSource m_audio;

    [Header("Win Setting")]
    [SerializeField] private Sprite WinBG;
    [SerializeField] private AudioClip WinSFX;

    [Header("Lose Setting")]
    [SerializeField] private Sprite LoseBG;
    [SerializeField] private AudioClip LoseSFX;

    public override void OnEnterState() 
    {
        if (GameManager.Instance.GameStatus == gameStatusType.Win)
        {
            EndScreenBG.sprite = WinBG;
            m_audio.PlayOneShot(WinSFX);
        }
        else 
        {
            EndScreenBG.sprite = LoseBG;
            m_audio.PlayOneShot(LoseSFX);
        }

        StartCoroutine(WaitInScreen());
    }
    public override void OnUpdateState() { }
    public override void OnExitState() { }

    private IEnumerator WaitInScreen() 
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.NextState();
    }
}

public enum gameStatusType { Win, Lose }
