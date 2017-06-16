using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectFrame : MonoBehaviour {

    public GameObject SelectFrame;
    public StatusBase statusBase;

    float alpha = 1.0f;
    bool alphaCheck = true;

    // Use this for initialization
    void Start ()
    {
        gameObject.AddComponent<IgnoreTouch>();

        //代入変数
        Vector3 pos;

        //---プレハブ情報取得---
        var res = Resources.Load<GameObject>("Prehabs/gameObject/SelectFrame");

        //---ゲームオブジェクト生成---
        var obj = Instantiate(res) as GameObject;

        //---親子情報作成---
        obj.transform.SetParent(this.transform);

        //---位置情報初期化---
        obj.transform.localPosition = Vector3.zero;

        //---ステータス情報取得---
        statusBase = this.GetComponent<StatusBase>();

        alphaCheck = true;
        alpha = 1.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {

        //加算α値設定
        float plusAlpha = 0.03f;

        if( statusBase.IsSelect == true )
        {
            //オブジェクトを表示
            this.gameObject.transform.Find("SelectFrame(Clone)").gameObject.SetActive(true);

            //--- 点滅操作 ---
            if (alphaCheck == true)
            {
                alpha -= plusAlpha;

                if (alpha < 0.5f)
                {
                    alphaCheck = false;
                }
            }
            else
            {
                alpha += plusAlpha;

                if (alpha > 1.0f)
                {
                    alphaCheck = true;
                }
            }
        }
        else
        {
            //--- セレクトされていないので非表示 ---
            this.gameObject.transform.Find("SelectFrame(Clone)").gameObject.SetActive(false);
        }

        //--- カラー更新 ---
        this.gameObject.transform.Find("SelectFrame(Clone)").GetComponent<Image>().color =
                                                                          new Color(1, 1, 1, alpha);
    }
}
