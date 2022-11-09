using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens;
    private int screenIndex = 0;

    [Header("Game Elements")]
    [SerializeField] private float spins = 1;
    [SerializeField] private Image m_Clock;
    [SerializeField] private Transform m_ClockHandHour;
    [SerializeField] private Transform m_ClockHandMin;

    public void SetSreen(int index)
    {
        foreach (var screen in screens)
            screen.SetActive(false);
        screens[index].SetActive(true);
        screenIndex = index;
    }

    public void SetClockTime(float GameTime) 
    {
        float HourAngle = spins * 360;
        m_ClockHandHour.localRotation = Quaternion.Euler(0, 0, 0);

        m_ClockHandHour.DOLocalRotate(new Vector3(0f, 0f, HourAngle), GameTime);
    }
}
