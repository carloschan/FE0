using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

    [SerializeField] private GameObject m_gameObject;
    [SerializeField] private GameObject m_illustration;

    private CardData m_cardData;
    public CardData cardData { get { return m_cardData; } set { m_cardData = value; if(cardType != CardType.LIFE) refreshAsset(); }  }
    public GameObject assetObject { get { return m_gameObject; } }
    // Use this for initialization

    public enum CardType {  HAND, LIFE, ENEGY, CHARATER }
    public CardType cardType { get; set; }
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
}
