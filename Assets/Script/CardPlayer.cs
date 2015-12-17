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

    [SerializeField] private GameObject m_cardAsset;

    [SerializeField] private String m_playerName;

    private Queue<CardData> m_deck = new Queue<CardData>();

    private List<Card> m_frontCount = new List<Card>();
    private List<Card> m_backCount = new List<Card>();

    private Queue<CardData> m_lounge = new Queue<CardData>();

    private Queue<Card> m_energy = new Queue<Card>();
    private Queue<Card> m_life = new Queue<Card>();
    // private Queue<CardData> m_handCard;


    private List <Card> m_handCard = new List<Card>();

    public List<Card> handCard { get { return m_handCard; } }
    public List<Card> frontCount { get { return m_frontCount; } }
    public List<Card> backCount { get { return m_backCount; } }


    public Queue<CardData> deck { get { return m_deck; } }
    public Queue<CardData> lounge { get { return m_lounge; } }

    public Queue<Card> energy { get { return m_energy; } }
    public Queue<Card> life { get { return m_life; } }



    public delegate void DrawCardAction(int remainText);
    public event DrawCardAction OnCardDraw;

    public delegate void PlayerAction();
    public event PlayerAction OnDrawEnd;
    public event PlayerAction OnFriendShipChooseEnd;


    private void Disable()
    {
        GameMaster.OnPhaseStart -= pharseStart;
        GameMaster.OnPhaseEnd -= pharseEnd;
    }


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

        // set basic life card ;

        for (int i = 0; i < 5; ++i)
        {
            recoverLife();
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

        GameObject newCardObject = Instantiate(m_cardAsset) as GameObject;
        newCardObject.transform.SetParent(m_handCardArea.transform);

        Card newCard = newCardObject.GetComponent<Card>();

        newCard.cardType = Card.CardType.HAND;
        newCard.cardData = cardData;

        m_handCard.Add(newCard);

        GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        for (int i = 0; i < m_handCard.Count; ++i)
        {
            GameObject handCardObject = m_handCard[i].assetObject;

            Vector3 position = new Vector3(i * 10.0F / (m_handCard.Count * 1.8f), 0, -i) + m_handCardArea.transform.position;

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

    public void recoverLife()
    {
        CardData cardData = m_deck.Dequeue();

        putCardToLifeArea(cardData);

        if (OnCardDraw != null)
            OnCardDraw(m_deck.Count);

    }

    public void putCardToLifeArea( CardData cardData )
    {


        GameObject newCardObject = Instantiate(m_cardAsset) as GameObject;

        // Vector3 position = new Vector3(m_life.Count * 10.0F / m_life.Count + 1, 0, -m_life.Count) + m_lifeArea.transform.position;
        newCardObject.transform.SetParent( m_lifeArea.transform,false);
        Vector3 position = new Vector3(0, 0, -m_life.Count /3.0f ) + m_lifeArea.transform.position;
        newCardObject.transform.position = position;
        // newCardObject.transform.SetParent(m_lifeArea.transform);

        newCardObject.layer = 0;

        Card newCard = newCardObject.GetComponent<Card>();



        newCard.cardType = Card.CardType.LIFE;
        newCard.cardData = cardData;

        m_life.Enqueue(newCard);

        GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

       



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
