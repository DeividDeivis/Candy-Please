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
    public gameStatusType GameStatus;
    [SerializeField] private bool startGame;
    [Tooltip("Cuanto dura la partida?")]
    [SerializeField] private float GameTime;
    private float currentTime;

    public static GameManager Instance;

    #region Singleton
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetState(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState)
            currentState.OnUpdateState();

        if (startGame) 
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0) 
            { 
                startGame = false;
                NextState();
            }
        }
    }

    #region StateMachine
    public void SetState(int index) 
    {
        currentState.OnExitState();
        stateIndex = index;
        currentState = states[stateIndex];
        m_UI.SetSreen(stateIndex);
        currentState.OnEnterState();
    }
    public void NextState() 
    {
        currentState.OnExitState();
        states[stateIndex + 1].OnEnterState();
        stateIndex ++;
        currentState = states[stateIndex];
        m_UI.SetSreen(stateIndex);
    }
    #endregion

    #region GameSettings
    public void Initialize() 
    {
        currentTime = GameTime;
        startGame = false;
        GameStatus = gameStatusType.Win;
    }

    public void StartGame() 
    { 
        startGame = true;
        m_UI.SetClockTime(GameTime);
        NextState();
    }

    private void EndGame() 
    {
        
    }
    #endregion
}
