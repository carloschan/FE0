using UnityEngine;
using System.Collections;

public class EnegyType : MonoBehaviour {

    [SerializeField]
    private bool m_useAttend;
    [SerializeField]
    private bool m_useCost;

    public bool useAttend { get { return m_useAttend; } set { m_useAttend = value; } }
    public bool useCost { get { return m_useCost; } set { m_useCost = value; } }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
