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
    [SerializeField] private VisitorsManager m_Visitors;
    [SerializeField] private CandiesManager m_Candies;

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
        m_UI.SetSreen(stateIndex);
        currentState = states[stateIndex];
        currentState.OnEnterState();       
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
        m_UI.SetSreen(stateIndex);
        currentState = states[stateIndex];
        currentState.OnEnterState();             
    }
    public void NextState() 
    {
        currentState.OnExitState();
        stateIndex ++;
        if (stateIndex == states.Count)
            stateIndex = 0;
        m_UI.SetSreen(stateIndex);
        currentState = states[stateIndex];
        currentState.OnEnterState();       
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
        m_UI.SetClockTime(GameTime);
        startGame = true;
        m_Visitors.SpawnVisitor();
        m_Candies.SortCandiesList();
        m_UI.LoadCandiesInUI(m_Candies.GetCandies());
        DialogSystem.Instance.WriteText("TOC TOC TOC!!!");
    }

    public void CheckVisitorStatus(VisitorStatus status) 
    {
        GameState gameState = states[1].GetComponent<GameState>();
        if (status == VisitorStatus.Angry)
            gameState.DamageHouse();
        gameState.DoorClose();
        m_Visitors.SpawnVisitor();
    }

    public void EndGame() 
    {
        m_UI.ResetCandiesInUI();
    }
    #endregion
}
