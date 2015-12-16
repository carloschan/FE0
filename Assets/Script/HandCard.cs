using UnityEngine;
using System.Collections;

public class HandCard : MonoBehaviour {

    [SerializeField] private GameObject m_gameObject;
    [SerializeField] private GameObject m_illustration;

    private CardData m_cardData;
    public CardData cardData { get { return m_cardData; } set { m_cardData = value; refreshAsset(); }  }
    public GameObject assetObject { get { return m_gameObject; } }
    // Use this for initialization

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
