using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
	static EffectManager _instance;
	public static EffectManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("EffectManager");
				_instance = obj.AddComponent<EffectManager>();
			}
			return _instance;
		}
	}

	public enum eEffect
	{
		eSlash = 0,
		ePunchAttack,
		eExplosionOrange,
		eExplosionPurple,
		eGunAttack,
		eEnemyAttack,
	}

	void Start()
	{
		this.transform.SetParent(GameObject.Find("Canvas").transform);
		this.transform.SetAsLastSibling();
		this.transform.localPosition = Vector3.zero;
	}

	public void SetEffect(eEffect e, Vector3 v)
	{
		GameObject effect = null;
		float scl = 2f;

		switch (e)
		{
			case eEffect.eSlash:
				effect = Resources.Load<GameObject>("Prehabs/Effect/Slash/effect_slash");
				break;
			case eEffect.ePunchAttack:
				effect = Resources.Load<GameObject>("Prehabs/Effect/PunchAttack/effect_punchattack");
				break;
			case eEffect.eExplosionOrange:
				effect = Resources.Load<GameObject>("Prehabs/Effect/Explosion/effect_explosion");
				break;
			case eEffect.eExplosionPurple:
				effect = Resources.Load<GameObject>("Prehabs/Effect/Explosion/effect_explosion_purple");
				break;
			case eEffect.eGunAttack:
				effect = Resources.Load<GameObject>("Prehabs/Effect/GunAttack/effect_gunattack");
				scl = 100f;
				break;
			case eEffect.eEnemyAttack:
				effect = Resources.Load<GameObject>("Prehabs/Effect/EnemyAttack/effect_enemyattack");
				break;
		}

		if (effect)
		{
			GameObject obj = Instantiate(effect) as GameObject;
			obj.transform.SetParent(this.transform);
			obj.GetComponent<RectTransform>().localPosition = v;
			obj.GetComponent<RectTransform>().localScale = Vector3.one * scl;
			obj.transform.SetAsFirstSibling();
		}
	}
}
