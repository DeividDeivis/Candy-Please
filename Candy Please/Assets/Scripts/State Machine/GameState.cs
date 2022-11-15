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
    [SerializeField] private AudioClip doorOpenSFX;
    [SerializeField] private AudioClip doorCloseSFX;

    [Header("Inventory Settings")]
    [SerializeField] private Transform m_InventoryArea;

    [Header("Dialog Settings")]
    [SerializeField] private TextMeshProUGUI m_Dialog;
    [SerializeField] private DialogSystem m_DialogSystem;

    [Header("Visitor Settings")]
    [SerializeField] private VisitorsManager visitorsManager;
    [SerializeField] private VisitorController visitorController; 
    private float currentPatience;

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
        if (currentHouseLife <= 2) // perdio alguna vida.
            GameManager.Instance.GameStatus = gameStatusType.Lose;
        else
            GameManager.Instance.GameStatus = gameStatusType.Win;
    }

    private void ResetData() 
    {
        m_houseStatus.sprite = statusNormal;
        currentHouseLife = HouseLifes;
        currentPatience = visitorsManager.VisitorPatience;
        m_Dialog.text = "";
        doorOpen = false;
        m_Door.transform.localScale = Vector3.one;
        GameManager.Instance.Initialize();
    }

    public void ClickDoor() 
    {
        m_Door.interactable = false;

        if (doorOpen == false)
        {
            m_DoorSound.PlayOneShot(doorOpenSFX);
            m_Door.transform.DOScaleX(.1f, .3f).OnComplete(() =>
            {
                doorOpen = true;
                m_Door.interactable = true;
                visitorController.VisitorInDoor(doorOpen);
            });
        }
        else 
        {
            m_DoorSound.PlayOneShot(doorCloseSFX);
            m_Door.transform.DOScaleX(1f, .3f).OnComplete(() =>
            {
                doorOpen = false;
                m_Door.interactable = true;
                visitorController.VisitorInDoor(doorOpen);
            });
        }
    }

    public void DamageHouse() 
    {
        currentHouseLife--;
        switch (currentHouseLife) 
        {
            case 2: m_houseStatus.sprite = statusDamage1; break;
            case 1: m_houseStatus.sprite = statusDamage2; break;
            case 0: m_houseStatus.sprite = statusDamage3; break;
            case -1: GameManager.Instance.NextState(); break;
            default: m_houseStatus.sprite = statusNormal; break;
        }
    }
}
