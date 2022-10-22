using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("State Machine Settings")]
    [SerializeField] List<State> states;
    private State currentState;
    private int stateIndex = 0;

    [Header("Managers & Controllers")]
    [SerializeField] private UIManager m_UI;

    [Header("Game Settings")]
    [Tooltip("Cuanto dura la partida?")]
    [SerializeField] private float GameTime;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdateState();
    }

    #region StateMachine
    public void SetState(int index) 
    {
        stateIndex = index;
        currentState = states[stateIndex];
        currentState.OnEnterState();
    }
    public void NextState() 
    {
        currentState.OnExitState();
        stateIndex ++;
        currentState = states[stateIndex];
        currentState.OnEnterState();
    }
    #endregion

    #region GameSettings
    private void Initialize() 
    {
        currentTime = GameTime;
    }
    #endregion
}
