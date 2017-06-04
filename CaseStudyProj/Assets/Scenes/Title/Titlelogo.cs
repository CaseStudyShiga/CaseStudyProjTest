using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titlelogo : MonoBehaviour {
    
    private Image renderer;

    public bool bUse = false;      // 使用されているか
    private bool bAdd = true;
    public float a_color = 0.0f;

    // Use this for initialization
    void Start()
    {
		this.transform.localPosition = new Vector3(0,330);
        renderer = GetComponent<Image>();
		renderer.sprite = Resources.Load<Sprite>("Sprites/Title/logo");
		a_color = 0.0f;
        renderer.color = new Color(255, 255, 255, a_color);
        bUse = false;
        bAdd = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bUse)
        {
            renderer.color = new Color(255, 255, 255, a_color);

            // アルファ値増減　true増加　false減少
            if (bAdd)
            {
                a_color += 0.02f;
                if (a_color >= 1.0f) bAdd = false;
            }
            else
            {
                a_color -= 0.02f;
                if (a_color <= 0.0f) bAdd = true;
            }
        }
        else
        {
            var bMainChange = this.transform.parent.Find("TestTitle").GetComponent<TitleTouch>().bUse;
            if (bMainChange)
            {
                bUse = true;
            }
        }
    }
   
}
