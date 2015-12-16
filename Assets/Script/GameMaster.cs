using System;

using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    [SerializeField] private CardPlayer m_player1 = null;
    [SerializeField] private CardPlayer m_player2 = null;


    public enum Phase { PREPARE, START, FRIENDSHIP, ATTEND, ACTION, END};

    private int currentPlayerIdx = 0;



    public delegate void PhaseAction(CardPlayer cardPlayer, Phase current );
    public static event PhaseAction OnPhaseStart;
    public static event PhaseAction OnPhaseEnd;

    public static GameMaster instance { get; set; } 

    public delegate void NextAction();

    public static event NextAction OnStartNext;
    public static event NextAction OnFriendShipNext;
    public static event NextAction OnAttendNext;
    public static event NextAction OnActionNext;
    public static event NextAction OnEndNext;


    public int round { get; set; }
    public Phase currentPhase { get; set; }
    // Use this for initialization
    void Start () {

        if (instance == null)
            instance = this;

        currentPhase = Phase.PREPARE;
        round = 0;


        //preparePhaseNext();

        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        currentPlayerIdx = 0;


        nextPhase();

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void nextPhase()
    {
        CardPlayer currentPlayer = (currentPlayerIdx == 0) ? m_player1 : m_player2;

        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        currentPlayerIdx = 0;
       
       

        OnPhaseEnd(currentPlayer, currentPhase);

        currentPhase += 1;

        if(currentPhase >Phase.END)
        {
            currentPhase = Phase.START;
            currentPlayerIdx = (currentPlayerIdx == 0) ? 1 : 0;

            currentPlayer = (currentPlayerIdx == 0) ? m_player1 : m_player2;
            ++round;

        }

        OnPhaseStart(currentPlayer, currentPhase);

    }

    


}
