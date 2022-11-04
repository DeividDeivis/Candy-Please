using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens;
    private int screenIndex = 0;

    [Header("Menu Elements")]
    [SerializeField] private Transform m_Title;
    [SerializeField] private Transform m_StartBtn;
    private Sequence MenuAnim;

    [Header("Game Elements")]
    [SerializeField] private Transform g_Watch;

    [Header("EndGame Elements")]
    [SerializeField] private Transform e_Title;
    private Sequence EndAnim;

    public void SetSreen(int index)
    {
        foreach (var screen in screens)
            screen.SetActive(false);
        screens[index].SetActive(true);
        screenIndex = index;
    }

    public void AnimMenu() 
    {
        MenuAnim = DOTween.Sequence();
        //MenuAnim.Append();
    }
}
