using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Information : MonoBehaviour {

    [SerializeField] private Text m_deckRemainCard;
    [SerializeField]
    private Text m_remainEngry;

    [SerializeField]
    private Text m_state;

    [SerializeField]
    private GameObject m_btnNextPhase;

    private CardPlayer m_contorler;

    public static Information instance { get; set; }

    private void Awake()
    {
        m_contorler = GameObject.FindWithTag("Player").GetComponent<CardPlayer>();

        m_contorler.OnCardDraw += UpdateText;
        instance = this;

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       
	}

    public void UpdateText( int remainText )
    {
         m_deckRemainCard.text = remainText.ToString();
    }

    public void UpdateState( string state )
    {
        m_state.text = state;
    }

    public void UpdateRemainEngry(int remainEngry, int remainCost)
    {
        m_remainEngry.text = "出角色的cost值:"+ remainEngry.ToString()+ "\n"+"餘下cost:"+remainCost;
    }

    public void nextPhaseBtnOn()
    {
        m_btnNextPhase.SetActive(true);
    }

    public void nextPhaseBtnOff()
    {
        m_btnNextPhase.SetActive(false);
    }
}
