using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameState : State
{
    [SerializeField] private AudioSource m_audio;
    [Header("Door Settings")]
    [SerializeField] private Button m_Door;
    private bool doorOpen = false;
    [SerializeField] private AudioSource m_DoorSound;

    [Header("Inventory Settings")]
    [SerializeField] private Transform m_InventoryArea;

    [Header("Dialog Settings")]
    [SerializeField] private TextMeshProUGUI m_Dialog;
    [SerializeField] private DialogSystem m_DialogSystem;

    [Header("Visitor Settings")]
    [SerializeField] private VisitorsManager visitorsManager;
    [SerializeField] private VisitorController visitorController; 

    [Header("House Settings")]
    [SerializeField] private Image m_houseStatus;
    [SerializeField] private int HouseLifes = 3;
    private int currentHouseLife;
    [Header("Status")]
    [SerializeField] private Sprite statusNormal;
    [SerializeField] private Sprite statusDamage1;
    [SerializeField] private Sprite statusDamage2;
    [SerializeField] private Sprite statusDamage3;

    public override void OnEnterState() 
    {
        m_Door.onClick.AddListener(ClickDoor);
        ResetData();
        m_audio.Play();
        GameManager.Instance.StartGame();
    }
    public override void OnUpdateState() { }
    public override void OnExitState() 
    {
        m_audio.Stop();
        if (currentHouseLife <= 0) // perdio toda la vida.
            GameManager.Instance.GameStatus = gameStatusType.Lose;
        else
            GameManager.Instance.GameStatus = gameStatusType.Win;
    }

    private void ResetData() 
    {
        m_houseStatus.sprite = statusNormal;
        currentHouseLife = HouseLifes;
        m_Dialog.text = "";
        doorOpen = false;
        m_Door.transform.localScale = Vector3.one;
        GameManager.Instance.Initialize();
    }

    public void ClickDoor() 
    {
        if (doorOpen == false)       
            DoorOpen();       
        else        
            DoorClose();       
    }

    public void DoorOpen() 
    {
        UIManager.Instance.StopHitDoor();
        m_Door.interactable = false;
        m_DoorSound.PlayOneShot(AudioManager.Instance.GetSound("Door Open"));
        m_Door.transform.DOScaleX(.1f, .3f).OnComplete(() =>
        {
            doorOpen = true;
            m_Door.interactable = true;
            visitorController.VisitorInDoor(doorOpen);
        });
    }

    public void DoorClose()
    {
        m_Door.interactable = false;
        m_DoorSound.PlayOneShot(AudioManager.Instance.GetSound("Door Close"));
        m_Door.transform.DOScaleX(1f, .3f).OnComplete(() =>
        {
            doorOpen = false;
            m_Door.interactable = true;
            visitorController.VisitorInDoor(doorOpen);
        });
    }

    public void DamageHouse() 
    {
        currentHouseLife--;
        switch (currentHouseLife) 
        {
            case 2: m_houseStatus.sprite = statusDamage1; GameManager.Instance._houseStatus = "Sucia"; break;
            case 1: m_houseStatus.sprite = statusDamage2; GameManager.Instance._houseStatus = "Quemada"; break;
            case 0: m_houseStatus.sprite = statusDamage3; GameManager.Instance._houseStatus = "Arruinada"; break;
            case -1: GameManager.Instance.NextState(); GameManager.Instance._houseStatus = "Destruida"; break;
            default: m_houseStatus.sprite = statusNormal; GameManager.Instance._houseStatus = "Impecable"; break;
        }
        Sequence StatusAnim = DOTween.Sequence()
            .Append(m_houseStatus.rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .3f))
            .Join(m_houseStatus.rectTransform.DOShakeRotation(1, 30, 10, 90, false , ShakeRandomnessMode.Harmonic))
            .Append(m_houseStatus.rectTransform.DOScale(Vector3.one, .3f));
    }
}
