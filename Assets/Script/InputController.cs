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

                        m_cardDetail.ActiveHandCard(m_cardPlayer, card);
                    }
                }
            }

        }

    }
}
