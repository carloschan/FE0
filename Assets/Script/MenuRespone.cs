using UnityEngine;
using System.Collections;

public class MenuRespone : MonoBehaviour {

    //public enum Response { FRIENDSHOP, ATTEND, CLASS_CHANGE, CRITICAL, DEF, ACTIVE_SKILL, ABANDOM, CANCEL };
    public delegate void ResponeAction(string response);
    public static event ResponeAction OnRespone;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    new void SendMessage(string respone)
    {

        OnRespone(respone);
    }

}
