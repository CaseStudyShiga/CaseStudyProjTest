using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleTouch : MonoBehaviour {
    private Image renderer;

    public bool bUse = false;      // 使用されているか
    private bool bChange = false;   // アルファ値増減が変わったか否か

    public float a_color = 0.0f;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<Image>();
        bUse = false;
        bChange = false;
        a_color = 0.0f;
        renderer.color = new Color(255, 255, 255, a_color);
    }
	
	// Update is called once per frame
	void Update () {
        if (bUse)
        {
            if (!bChange)
            {
                renderer.color = new Color(255, 255, 255, a_color);

                // アルファ値増減　true増加　false減少
                a_color += 0.02f;
                if (a_color >= 1.0f) bChange = true;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Game");
                }
            }
        }
        else
        {
            var bEnd3 = this.transform.parent.Find("3").GetComponent<TitleTexture3>().bEnd;
            if (bEnd3) bUse = true;
        }
        
    }
}
