using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    [SerializeField] private GameObject m_gameObject;
    [SerializeField] private GameObject m_illustration;
    [SerializeField]
    private Sprite m_cardBack;

    private CardData m_cardData;
    public CardData cardData { get { return m_cardData; } set { m_cardData = value; if(cardType != CardType.LIFE) refreshAsset(); }  }
    public GameObject assetObject { get { return m_gameObject; } }
    // Use this for initialization
    public enum CardType { HAND, LIFE, ENEGY, CHARATER }

    private CardType mCardTyp;
   
    public CardType cardType { get { return mCardTyp; } set { _refreshCardType(value); mCardTyp = value; } }
    private void Awake()
    {

    }

    void Start () {
	
	}

    public void setIllustration(Sprite illustration)
    {
        m_illustration.GetComponent<SpriteRenderer>().sprite = illustration;

    }

    public void refreshAsset()
    {
        setIllustration( Resources.Load<Sprite>("Card illustration/"+cardData.illustrationPath) );
    }

   


    // Update is called once per frame
    void Update () {
	
	}

    public void filp()
    {
        m_illustration.GetComponent<SpriteRenderer>().sprite = m_cardBack;
    }

    void _refreshCardType(CardType nextType)
    {
        // remove Before Compent
        {
            switch (mCardTyp)
            {
                case CardType.ENEGY:
                    Destroy(m_gameObject.GetComponent<EnegyType>());
                    break;
                case CardType.CHARATER:
                    Destroy(m_gameObject.GetComponent<CharType>());
                    break;
            }
        }

        // add new Compent
        {
            switch (nextType)
            {
                case CardType.ENEGY:
                    m_gameObject.AddComponent<EnegyType>();
                    break;
                case CardType.CHARATER:
                    m_gameObject.AddComponent<CharType>();
                    break;
            }
        }
    }
}
