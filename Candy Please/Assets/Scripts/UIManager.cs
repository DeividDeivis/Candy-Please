using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens;
    private int screenIndex = 0;

    [Header("Game Elements")]
    [SerializeField] private float clockSpins = 1;
    [SerializeField] private Image m_Clock;
    [SerializeField] private Transform m_ClockHandHour;
    [SerializeField] private Transform m_ClockHandMin;
    [SerializeField] private Transform m_CandiesSpawnArea;

    public void SetSreen(int index)
    {
        foreach (var screen in screens)
            screen.SetActive(false);
        screens[index].SetActive(true);
        screenIndex = index;
    }

    public void SetClockTime(float GameTime) 
    {
        float HourAngle = 360 * clockSpins;
        m_ClockHandHour.localRotation = Quaternion.Euler(Vector3.zero);
        m_ClockHandHour.DOLocalRotate(new Vector3(0f, 0f, -HourAngle), GameTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);

        float MinAngle = 360 * clockSpins * 12;
        m_ClockHandMin.localRotation = Quaternion.Euler(Vector3.zero);
        m_ClockHandMin.DOLocalRotate(new Vector3(0f, 0f, -MinAngle), GameTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
    }

    public void LoadCandiesInUI(List<GameObject> candiesToSpawn)
    {
        foreach (GameObject candy in candiesToSpawn) 
        { 
            GameObject instantiate = Instantiate(candy, m_CandiesSpawnArea);
            instantiate.transform.localScale = Vector3.zero;
            instantiate.transform.DOScale(Vector3.one, .3f);
        }
    }

    public void ResetCandiesInUI()
    {
        foreach (Candy candy in m_CandiesSpawnArea.GetComponentsInChildren<Candy>())
            Destroy(candy.gameObject);
    }
}
