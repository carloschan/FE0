using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailText : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {



    }

    public void setText(string text)
    {
        Text textObject = this.GetComponentInChildren<Text>();
        textObject.text = text;
    }

}
