using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectEnemyAttackScript : MonoBehaviour {

    public int animationSpd = 2;

    private Sprite[] spr;
    private Image img;
    private int animationCnt = 0;
    private int animationIdx = 0;

    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
        spr = Resources.LoadAll<Sprite>("Prehabs/Effect/EnemyAttack/sprite_effect_enemy");
    }

    void Update()
    {
        animationCnt++;
        if (animationCnt > animationSpd)
        {
            animationCnt = 0;
            animationIdx++;

            if (animationIdx < spr.Length)
            {
                img.sprite = spr[animationIdx];
            }
            else
            {
                animationIdx = 0;
                Destroy(gameObject);
            }
        }
    }
}
