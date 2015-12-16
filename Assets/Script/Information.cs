using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Information : MonoBehaviour {

    [SerializeField] private Text m_deckRemainCard;
    [SerializeField]
    private Text m_state;
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
}
