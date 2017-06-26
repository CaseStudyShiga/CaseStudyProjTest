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
		eAttackSign,
	}

	void Start()
	{
		this.transform.SetParent(GameObject.Find("Canvas/UI").transform);
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
			case eEffect.eAttackSign:
				effect = Resources.Load<GameObject>("Prehabs/Effect/AttackSign/effect_attacksign");
				scl = 1f;
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

	public GameObject AttackSign(int dir, int range, Vector3 pos, Transform parent)
	{
		GameObject effect = Resources.Load<GameObject>("Prehabs/Effect/AttackSign/effect_attacksign");
		Quaternion qua = Quaternion.identity;
		Vector3 scl = new Vector3(range * 1f, 1f, 1f);

		qua.eulerAngles = new Vector3(0, 0, 90 - (dir * 45));

		if (dir % 2 == 1)
		{
			scl = new Vector3(scl .x * 1.414f, scl.y, scl.z);
		}

		if (effect)
		{
			GameObject obj = Instantiate(effect) as GameObject;
			obj.transform.SetParent(parent);
			obj.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
			obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
			obj.GetComponent<RectTransform>().localScale = scl;
			obj.GetComponent<RectTransform>().localRotation = qua;
			obj.transform.SetAsFirstSibling();
			obj.name = "AttackSign" + dir.ToString();

			return obj;
		}

		return null;
	}
}
