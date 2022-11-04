using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameState : State
{
    [SerializeField] private Image m_Door;
    [SerializeField] private Transform m_InventoryArea;
    [SerializeField] private TextMeshProUGUI m_Dialog;

    [SerializeField] private VisitorsManager visitorsManager;

    [SerializeField] private Image m_houseStatus;
    [Header("Status")]
    [SerializeField] private Sprite statusNormal;

    public override void OnEnterState() 
    { 
        
    }
    public override void OnUpdateState() { }
    public override void OnExitState() { }
}
