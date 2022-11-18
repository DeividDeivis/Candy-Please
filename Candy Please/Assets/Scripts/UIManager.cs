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
    [SerializeField] private Transform m_Door;
    [SerializeField] private AudioSource m_DoorAudio;
    private Coroutine hitDoorCoroutine;

    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }
    #endregion

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
            instantiate.GetComponent<Candy>().ActiveCandy();
        }
    }

    public void ResetCandiesInUI()
    {
        foreach (Candy candy in m_CandiesSpawnArea.GetComponentsInChildren<Candy>())
            Destroy(candy.gameObject);
    }

    public void HitDoor()
    {
        DialogSystem.Instance.WriteText("TOC TOC TOC!!!");
        hitDoorCoroutine = StartCoroutine(Hit());
    }

    private IEnumerator Hit() 
    {
        for (int i = 0; i < 3; i++) 
        {
            m_Door.DOShakeRotation(.3f, 10, 10, 90, false, ShakeRandomnessMode.Harmonic);
            m_DoorAudio.PlayOneShot(AudioManager.Instance.GetSound("Hit Door"));
            yield return new WaitForSeconds(.3f);
        }
    }

    public void StopHitDoor() 
    {
        StopCoroutine(hitDoorCoroutine);
    }
}
