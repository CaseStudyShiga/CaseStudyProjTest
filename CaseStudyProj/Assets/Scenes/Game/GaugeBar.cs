using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GaugeBar : MonoBehaviour {

    public Slider slider;
    public StatusBase statusBase;

    float _hp;

    void Start()
    {
        //代入変数
        Vector3 pos;

        //ステータスベース情報取得(プレハブから取得)
        slider = GetComponent<Slider>();

        //---プレハブ情報取得---
        var res = Resources.Load<GameObject>("Prehabs/gameObject/HPGuage");

        //---ゲームオブジェクト生成---
        var obj = Instantiate(res) as GameObject;

        //---親子情報作成---
        obj.transform.SetParent(this.transform);

        //---位置情報初期化---
        obj.transform.localPosition = Vector3.zero;
        pos = obj.transform.localPosition;
        pos.y = -40;
        obj.transform.localPosition = pos;

        //---ステータス情報取得---
        statusBase = this.GetComponent<StatusBase>();

        //---スライダー情報取得---
        slider = obj.GetComponent<Slider>();

        //---最大値をプレイヤーと同期---
        _hp = statusBase.HpMax;
        slider.maxValue = _hp;


    }

    void Update()
    {
        //---小数点四捨五入---
        float LowHp = Mathf.Round(_hp * 0.3f);

        //---スライダーとプレイヤーのHP値を同期---
        slider.value = statusBase.Hp;

        if (slider.value > LowHp)
        {
            //---HPが30％以上の場合---
            this.gameObject.transform.Find("HPGuage(Clone)/Fill Area/Fill").GetComponent<Image>().color =
                                                                           new Color(0, 1, 0, 1.0f);
        }
        else
        {
            //---HPが30％以下の場合---
            this.gameObject.transform.Find("HPGuage(Clone)/Fill Area/Fill").GetComponent<Image>().color =
                                                                           new Color(1, 0, 0, 1.0f);
        }

        
    }
}
