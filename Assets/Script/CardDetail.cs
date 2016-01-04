using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CardDetail : MonoBehaviour {


    [SerializeField]private DetailText[] m_cardDetailText;
    [SerializeField]private GameObject m_instance;
    [SerializeField]
    private Image m_illustration ;

    [SerializeField]
    private GameObject m_infomation;


    [SerializeField]
    private GameObject[] m_ButtonState;


    [SerializeField]
    private Text m_status_text;

    public enum DetailType { HAND_CARD, CHOOSET_TARGET, CHARATER, VIEW}
    public enum Response { FRIENDSHIP, ATTEND,  CLASS_CHANGE, CRITICAL, DEF, ACTIVE_SKILL, ABANDOM }

    public enum TextType { ATTACK_COST, CLASS_CHANGE_COST, CLASS, WARRIOR, ATTACK, SUPPORT, SYMBOL, SEX, ARMS, TYPE, RANGE }

    public enum InformationState {  HAND_CARD, PERAPARE_IN_BATTLE_FIELD, CHOOSE_TARGET, CHARTER, VIEW_ONLY}
    private DetailType m_detailType;



    public delegate void ResponeAction(Response response);
    public static event ResponeAction OnRespone;

    private CardPlayer m_cardPlayer;
    private Card m_card;


    public Card card { get { return m_card; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void setCardDetail(CardData cardData)
    {
        m_cardDetailText[(int)TextType.ATTACK_COST].setText(cardData.attendCost.ToString());

        if (cardData.classChangeCost == -1)
            m_cardDetailText[(int)TextType.CLASS_CHANGE_COST].setText("--");
        else
            m_cardDetailText[(int)TextType.CLASS_CHANGE_COST].setText(cardData.classChangeCost.ToString());
        m_cardDetailText[(int)TextType.CLASS].setText(cardData.characterClass.ToString());
        m_cardDetailText[(int)TextType.WARRIOR].setText(cardData.arms.ToString());
        m_cardDetailText[(int)TextType.ATTACK].setText(cardData.power.ToString());
        m_cardDetailText[(int)TextType.SUPPORT].setText(cardData.support.ToString());
        m_cardDetailText[(int)TextType.SYMBOL].setText(cardData.faction.ToString());
        m_cardDetailText[(int)TextType.SEX].setText(cardData.gender.ToString());
        m_cardDetailText[(int)TextType.ARMS].setText(cardData.weapon.ToString());

        String typeText = "";
        for( int i = 0; i < cardData.type.Count; ++i)
        {
            typeText += cardData.type[i].ToString();
        }
        m_cardDetailText[(int)TextType.TYPE].setText(typeText);

        if(cardData.range.Equals( CardData.RANGE.Range1_2))
             m_cardDetailText[(int)TextType.RANGE].setText("射程 1 - 2");
        else if(cardData.range.Equals(CardData.RANGE.Range1))
            m_cardDetailText[(int)TextType.RANGE].setText("射程 1");
        else if (cardData.range.Equals(CardData.RANGE.Range2))
            m_cardDetailText[(int)TextType.RANGE].setText("射程 2");

        m_illustration.sprite = Resources.Load<Sprite>("Card illustration/" + cardData.illustrationPath);
    }

    public void activeHandCard(CardPlayer cardPlayer, Card card)
    {
        if (m_instance.activeInHierarchy)
        {
            Debug.Log("Detail Already lanch");
            return;
        }
        m_instance.SetActive(true);
        m_infomation.SetActive(false);

        setCardDetail(card.cardData);
        setBtnState(InformationState.HAND_CARD);
        m_cardPlayer = cardPlayer;
        m_card = card;


    }

    public void inactive()
    {
        m_instance.SetActive(false);
        m_infomation.SetActive(true);
    }

    public void setStatusText(String msg)
    {
        m_status_text.text = msg;   
    }

    public void setBtnState(InformationState informationState)
    {
        for( int i = 0; i < m_ButtonState.Length; ++i)
        {
            m_ButtonState[i].SetActive(i == (int)informationState);
        }
    }

  }
