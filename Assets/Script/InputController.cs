using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    [SerializeField]
    private Camera m_Camera;

    [SerializeField]
    private Camera m_HandCardCamera;

    [SerializeField]
    private CardDetail m_cardDetail;


    private CardPlayer m_cardPlayer;
    

    public bool allowInput { get; set; }

    // Use this for initialization
    void Start () {

        m_cardPlayer = GetComponent<CardPlayer>();
        allowInput = true;

    }
	
	// Update is called once per frame
	void Update () {

        if (allowInput && Input.GetMouseButtonDown(0))
        { // if left button pressed...
            {
                Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Deck")
                    {
                  
                        m_cardPlayer.draw();
                    }
                }
            }
            {
                Ray ray = m_HandCardCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(m_HandCardCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hitInfo)
                {
                    Debug.Log(hitInfo.transform.gameObject.name);

                    Card card = hitInfo.transform.gameObject.GetComponent<Card>();


                    if( card != null)
                    {
                        // handCard on Touch;
                        //hitInfo.transform.gameObject.transform.Translate(0, 3, 0);
                        allowInput = false;

                        GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

                        if (gm.currentPhase == GameMaster.Phase.ATTEND)
                        {

                            m_cardDetail.activeHandCard(m_cardPlayer, card);
                            m_cardDetail.setStatusText("你想點，講");
                        }else
                        {
                            m_cardDetail.activeHandCard(m_cardPlayer, card);
                            m_cardDetail.setBtnState(CardDetail.InformationState.VIEW_ONLY);
                            m_cardDetail.setStatusText("望下好啦");
                        }

                        MenuRespone.OnRespone += MenuRespone_OnRespone;
                    }
                }
            }

        }

    }

    private void MenuRespone_OnRespone(string response)
    {
       
        if (response == "CANCEL")
        {
            allowInput = true;
            m_cardDetail.inactive();
            MenuRespone.OnRespone -= MenuRespone_OnRespone;
        }
        else if (response == "FRIENDSHIP")
        {
            if (m_cardPlayer.isEngry)
            {
                allowInput = true;
                m_cardDetail.inactive();
                m_cardPlayer.putHandCardToEngry(m_cardDetail.card);
                m_cardPlayer.isEngry = false;
                MenuRespone.OnRespone -= MenuRespone_OnRespone;
            }
            else
            {
                m_cardDetail.setStatusText("你不能放能量啦，訓醒未");
            }

        }
        else if (response == "CRITICAL")
        {
            m_cardDetail.setStatusText("你未攻擊啊，訓醒未");
        }
        else if (response == "DEF")
        {
            m_cardDetail.setStatusText("冇人攻擊你，訓醒未");
        }else if( response == "IN_FRONT_AREA")
        {
            GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

            if (gm.currentPhase != GameMaster.Phase.ATTEND)
            {
                m_cardDetail.setStatusText("過左出角的時間啦，訓醒未");

            }
            else if (m_cardPlayer.putHandCardToBattleField(m_cardDetail.card, true))
            {
                allowInput = true;
                m_cardDetail.inactive();
                m_cardPlayer.isEngry = false;
                MenuRespone.OnRespone -= MenuRespone_OnRespone;
                //m_cardPlayer.isCharater = false;
            }
            else
            {
                m_cardDetail.setStatusText("唔夠cost 出啊，儲錢啦");
            }
        }
        else if (response == "IN_BACK_AREA")
        {
            GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

            if (gm.currentPhase != GameMaster.Phase.ATTEND)
            {
                m_cardDetail.setStatusText("過左出角的時間啦，訓醒未");

            }
            else if (m_cardPlayer.putHandCardToBattleField(m_cardDetail.card, false))
            {
                allowInput = true;
                m_cardDetail.inactive();
                m_cardPlayer.isEngry = false;
                MenuRespone.OnRespone -= MenuRespone_OnRespone;
                //m_cardPlayer.isCharater = false;
            }
            else
            {
                m_cardDetail.setStatusText("唔夠cost 出啊，儲錢啦");
            }
        }
        else if (response == "ATTEND")
        {
            GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

            if (gm.currentPhase != GameMaster.Phase.ATTEND)
            {
                m_cardDetail.setStatusText("過左出角的時間啦，訓醒未");

            }
            else if (m_cardDetail.card.cardData.attendCost <= m_cardPlayer.mAvailableEngry )
            {
                m_cardDetail.setBtnState(CardDetail.InformationState.PERAPARE_IN_BATTLE_FIELD);
            }
            else
            {
                m_cardDetail.setStatusText("唔夠cost 出啊，儲錢啦");
            }
        }
        else if (response == "CLASS_CHANGE")
        {
            GameMaster gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

            if (gm.currentPhase != GameMaster.Phase.ATTEND)
            {
                m_cardDetail.setStatusText("過左出角的時間啦，訓醒未");

            }
            else if (m_cardDetail.card.cardData.classChangeCost <= m_cardPlayer.mAvailableEngry )
            {
               // m_cardDetail.setBtnState(CardDetail.InformationState.PERAPARE_IN_BATTLE_FIELD);
            }
            else
            {
                m_cardDetail.setStatusText("唔夠cost 出啊，儲錢啦");
            }
        }
        else if (response == "ACTIVE_SKILL")
        {
            m_cardDetail.setStatusText("未做好啦");
        }
        else if (response == "ABANDOM")
        {
            m_cardDetail.setStatusText("shoot q左?訓醒未");
        }
    }
}
