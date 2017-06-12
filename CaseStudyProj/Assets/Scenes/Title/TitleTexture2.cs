using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTexture2 : MonoBehaviour {
    private Image renderer;

    private int activeTime = 0;     // 画像表示時間

    private bool bUse = false;      // 使用されているか
    private bool bAdd = true;      // アルファ値の増減変数
    private bool bChange = false;   // アルファ値増減が変わったか否か
    public bool bEnd = false;      // 表示を終えたかの判定

    public float a_color = 0.0f;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Image>();
        activeTime = 0;
        bUse = false;
        bAdd = true;
        bChange = false;
        bEnd = false;
        a_color = 0.0f;
        renderer.color = new Color(255, 255, 255, a_color);
    }

    // Update is called once per frame
    void Update()
    {
        if (bUse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bEnd = true;
                gameObject.SetActive(false);

                a_color = 0.0f;
                renderer.color = new Color(255, 255, 255, a_color);
            }
            renderer.color = new Color(255, 255, 255, a_color);

            // 画像の表示時間
            if (bChange)
            {
                activeTime++;
                if (activeTime >= 60) bChange = false;
            }
            else
            {
                // アルファ値増減　true増加　false減少
                if (bAdd)
                {
                    a_color += 0.02f;
                    if (a_color >= 1.0f)
                    {
                        bChange = true;
                        bAdd = false;
                    }
                }
                else
                {
                    a_color -= 0.02f;
                    if (a_color <= 0.0f)
                    {
                        bEnd = true;
                        gameObject.SetActive(false);

                        a_color = 0.0f;
                        renderer.color = new Color(255, 255, 255, a_color);
                    }
                }
            }
        }
        else
        {
            // 直前担当の画像が表示し終わったか
            var bEnd1 = this.transform.parent.Find("Aiming-logo").GetComponent<TitleTexture1>().bEnd;
            if (bEnd1) bUse = true;
        }
    }
}