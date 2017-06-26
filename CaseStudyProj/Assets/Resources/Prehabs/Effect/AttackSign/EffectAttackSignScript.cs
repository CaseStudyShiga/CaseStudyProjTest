using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectAttackSignScript : MonoBehaviour {

    public int animationSpd = 2;

    private Sprite[] spr;
    private Image img;
    private int animationCnt = 0;
    private int animationIdx = 0;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
        spr = Resources.LoadAll<Sprite>("Prehabs/Effect/AttackSign/sprite_effect_attacksign");
    }

    void Update()
    {
        animationCnt++;
        if (animationCnt > animationSpd)
        {
            animationCnt = 0;
            animationIdx++;

            if (animationIdx < spr.Length - 3)
            {
                img.sprite = spr[animationIdx];
            }
            else
            {
                animationIdx = 0;
                //Destroy(gameObject);
            }
        }
    }
}
