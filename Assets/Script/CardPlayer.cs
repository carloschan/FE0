using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CardPlayer : MonoBehaviour {

   

    [SerializeField]  private GameObject m_handCardArea;
    [SerializeField]  private GameObject m_deckArea;
    [SerializeField]  private GameObject m_frontCountArea;
    [SerializeField]  private GameObject m_backCountArea;
    [SerializeField]  private GameObject m_energyArea;
    [SerializeField]  private GameObject m_loungeArea;
    [SerializeField]  private GameObject m_lifeArea;

    [SerializeField] private GameObject m_handCardAsset;

    [SerializeField] private String m_playerName;

    private Queue<CardData> m_deck = new Queue<CardData>();
    private Queue<CardData> m_frontCount = new Queue<CardData>();
    private Queue<CardData> m_backCount = new Queue<CardData>();
    private Queue<CardData> m_energy = new Queue<CardData>();
    private Queue<CardData> m_lounge = new Queue<CardData>();
    private Queue<CardData> m_life = new Queue<CardData>();
    // private Queue<CardData> m_handCard;


    private List <HandCard> m_handCard = new List<HandCard>();

    public List<HandCard> handCard { get { return m_handCard; } }
    public Queue<CardData> deck { get { return m_deck; } }
    public Queue<CardData> frontCount { get { return m_frontCount; } }
    public Queue<CardData> backCount { get { return m_backCount; } }
    public Queue<CardData> energy { get { return m_energy; } }
    public Queue<CardData> lounge { get { return m_lounge; } }
    public Queue<CardData> life { get { return m_life; } }



    public delegate void DrawCardAction(int remainText);
    public event DrawCardAction OnCardDraw;

    public delegate void PlayerAction();
    public event PlayerAction OnDrawEnd;
    public event PlayerAction OnFriendShipChooseEnd;





    private void Awake()
    {
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        // Setting up references.
        Debug.Log("cardPlayer Awake");


        for (int i = 0; i < 50; ++i)
        {
            CardData cardData = new CardData();
            m_deck.Enqueue(cardData);
        }

       
        // draw card ;

        for (int i = 0; i < 5; ++i)
        {
            draw();
        }

        for (int i = 0; i < 5; ++i)
        {
            CardData cardData = m_deck.Dequeue();
            putToLife(cardData);

            if (OnCardDraw != null)
                OnCardDraw(m_deck.Count);


        }

        GameMaster.OnPhaseStart += pharseStart;
        GameMaster.OnPhaseEnd += pharseEnd;


    }

    private void FixedUpdate()
    {
      
    }

    void Start () {
        Debug.Log("cardPlayer start");
	}




    // Update is called once per frame
    void Update()
    {

       
    }

    public void draw( )
    {

        CardData cardData = m_deck.Dequeue();

        GameObject newCard = Instantiate(m_handCardAsset) as GameObject;
        newCard.transform.SetParent(m_handCardArea.transform);

        HandCard newHandCard = newCard.GetComponent<HandCard>();

        newHandCard.cardData = cardData;

        m_handCard.Add(newHandCard);

        GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        for (int i = 0; i < m_handCard.Count; ++i)
        {
            GameObject handCardObject = m_handCard[i].assetObject;

            Vector3 position = new Vector3(i * 10.0F / m_handCard.Count, 0, -i) + m_handCardArea.transform.position;

            handCardObject.transform.position = position;
        }

        if (gm.currentPhase > GameMaster.Phase.PREPARE)
        {
            BoxCollider boxCollider = m_deckArea.GetComponent<BoxCollider>();
            boxCollider.enabled = false;
        }

        if (OnCardDraw != null)
            OnCardDraw(m_deck.Count);

        if (OnDrawEnd != null)
            OnDrawEnd();

        if (gm.currentPhase == GameMaster.Phase.START)
            gm.nextPhase();



    }

    public void putToLife(CardData cardData)
    {
        m_life.Enqueue(cardData);

    }

    public void pharseStart(CardPlayer cardPlayer, GameMaster.Phase current)
    {
        if (cardPlayer != this)
            return;

        switch(current)
        {
            case GameMaster.Phase.START:
                StartCoroutine(costRecover());
                break;
            case GameMaster.Phase.FRIENDSHIP:
                Information.instance.UpdateState(m_playerName + " 絆階段開始");

                // open hand card collision for player;
                for (int i = 0; i < m_handCard.Count; ++i)
                {
                    BoxCollider2D boxCollider = m_handCard[i].assetObject.GetComponent<BoxCollider2D>();
                    boxCollider.enabled = true;
                }

                break;

        }
    }

    public void pharseEnd(CardPlayer cardPlayer, GameMaster.Phase current)
    {

    }

    public IEnumerator unTap ()
    {
        if (GameMaster.instance.round > 0)
        {
            Information.instance.UpdateState(m_playerName + " 回 unTap 中");
            yield return new WaitForSeconds(3);
        }


        if (GameMaster.instance.round == 0)
        {
            Information.instance.UpdateState(m_playerName + " 第一回合不能抽牌");
            // if frist round , skip draw;
            yield return new WaitForSeconds(1);
            
            GameMaster.instance.nextPhase();
        }else
        {
            Information.instance.UpdateState(m_playerName + " 請抽牌");
            BoxCollider boxCollider = m_deckArea.GetComponent<BoxCollider>();
            boxCollider.enabled = true;
        }
    }

    public IEnumerator costRecover()
    {
        if (GameMaster.instance.round > 0)
        {
            Information.instance.UpdateState(m_playerName + " 回 cost 中");
            yield return new WaitForSeconds(3);

        }
        



        StartCoroutine( unTap() );
    }

    public void chooseHandToFriendShip()
    {
        for( int i = 0; i < m_handCard.Count; ++i)
        {
            BoxCollider2D boxCollider = m_handCard[i].assetObject.GetComponent<BoxCollider2D>();
            boxCollider.enabled = true;
        }
    }

        
}
