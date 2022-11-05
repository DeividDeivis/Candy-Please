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
    private float currentPatience;

    [Header("House Settings")]
    [SerializeField] private Image m_houseStatus;
    [SerializeField] private int HouseLifes;
    private int currentHouseLife;

    public override void OnEnterState() 
    {
        m_Door.onClick.AddListener(ClickDoor);
        ResetData();
        m_audio.Play();
    }
    public override void OnUpdateState() { }
    public override void OnExitState() 
    {
        m_audio.Stop();
    }

    private void ResetData() 
    {
        m_houseStatus.sprite = statusNormal;
        currentHouseLife = HouseLifes;
        currentPatience = visitorsManager.VisitorPatience;
        m_Dialog.text = "";
        doorOpen = false;
        m_Door.transform.localScale = Vector3.one;
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
            });
        }
        else 
        {
            m_DoorSound.PlayOneShot(doorCloseSFX);
            m_Door.transform.DOScaleX(1f, .3f).OnComplete(() =>
            {
                doorOpen = false;
                m_Door.interactable = true;
            });
        }
    }

    public int GetHouseLife() { return currentHouseLife; }
}
