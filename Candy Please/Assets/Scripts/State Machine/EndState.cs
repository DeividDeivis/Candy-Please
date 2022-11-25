using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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

    [Header("Final Score")]
    [SerializeField] private Transform statsWindow;
    [SerializeField] private TextMeshProUGUI visitorsInDoorTxt;
    [SerializeField] private TextMeshProUGUI candiesGivedTxt;
    [SerializeField] private TextMeshProUGUI candiesCorrectTxt;
    [SerializeField] private TextMeshProUGUI candiesIncorrectsTxt;
    [SerializeField] private TextMeshProUGUI houseStatusTxt;

    public override void OnEnterState() 
    {
        ResetValues();
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

    public void ResetValues()
    {
        statsWindow.localScale = Vector3.zero;
        visitorsInDoorTxt.text = "";
        candiesGivedTxt.text = "";
        candiesCorrectTxt.text = "";
        candiesIncorrectsTxt.text = "";
        houseStatusTxt.text = "";
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
            .Append(MenuBtn.transform.DOScaleX(1f, .15f))
            .Join(statsWindow.DOScale(Vector3.one * 1.3f, .3f))
            .Append(statsWindow.DOScale(Vector3.one, .3f))
            .OnComplete(()=> StartCoroutine(FinalScores()));
    }

    private IEnumerator FinalScores() 
    {
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SumScore(GameManager.Instance._visitorsInDoor, visitorsInDoorTxt, "Visitantes"));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SumScore(GameManager.Instance._candiesGived, candiesGivedTxt, "Caramelos Entregados"));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SumScore(GameManager.Instance._candiesCorrects, candiesCorrectTxt, "Caramelos Correctos"));
        yield return new WaitForSeconds(.5f);
        StartCoroutine(SumScore(GameManager.Instance._candiesIncorrects, candiesIncorrectsTxt, "Caramelos Incorrectos"));
        yield return new WaitForSeconds(1f);
        houseStatusTxt.text = string.Format("Estado de la casa: {0}", GameManager.Instance._houseStatus);
        houseStatusTxt.transform.DOScale(Vector3.one * 1.3f, .3f)
            .OnComplete(() => houseStatusTxt.transform.DOScale(Vector3.one, .3f));
    }

    private IEnumerator SumScore(int finalscore, TextMeshProUGUI Text, string title) 
    {
        float lerp = 0f, duration = .5f;
        int startScore = 0;
        int scoreTo = finalscore;

        while (lerp < 1)
        {
            lerp += Time.deltaTime / duration;
            Text.text = string.Format("{0}: {1}", title,(int)Mathf.Lerp(startScore, scoreTo, lerp));           
            yield return null;
        }
    }
}

public enum gameStatusType { Win, Lose }
