using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : State
{
    [SerializeField] private AudioSource m_audio;
    [SerializeField] private Button Startgame;

    public override void OnEnterState() 
    {
        Startgame.onClick.AddListener(()=> GameManager.Instance.NextState());
        m_audio.Play();
    }

    public override void OnUpdateState() 
    { 
        
    }

    public override void OnExitState() 
    { 
        m_audio.Stop();
        Startgame.onClick.RemoveAllListeners();
    }
}
