using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSystem : MonoBehaviour {

    GameObject ButtonSystem;

    public Text myText;

	// Use this for initialization
	void Start () {

        ButtonSystem = GameObject.Find("ButtonSystem");

        TextObject();

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void TextObject()
    {
        myText.text = "abc";

    }
}
