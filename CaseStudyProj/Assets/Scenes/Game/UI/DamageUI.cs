using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class DamageUI : MonoBehaviour
{
	static DamageUI _instance;
	public static DamageUI Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("DamageUI");
				obj.AddComponent<RectTransform>();
				_instance = obj.AddComponent<DamageUI>();
				_instance.transform.SetParent(GameObject.Find("Canvas").transform);
				_instance.GetComponent<RectTransform>().localPosition = Vector3.zero;

			}
			return _instance;
		}
	}

	public void SetDamage(int damage, Vector3 pos)
	{
		GameObject text = this.CreateText("Text", damage.ToString(), _instance.transform, new Vector3(pos.x, pos.y + 40), 70, false);
		Text t = text.GetComponent<Text>();
		RectTransform rect = text.GetComponent<RectTransform>();

		rect.DOLocalMoveY(30f, 0.14f).SetRelative().OnComplete(() =>
		{
			rect.DOLocalMoveY(-65, 0.14f).SetRelative().OnComplete(()=> 
			{
				DOTween.ToAlpha(
					() => t.color,
					color => t.color = color,
					0f,                         // 最終的なalpha値
					0.5f
				).OnComplete(() => 
				{
					Destroy(text);
				});
			});
		});
	}

	GameObject CreateText(string name, string textValue, Transform parent, Vector3 pos, int size, bool large = true)
	{
		GameObject obj = new GameObject(name);
		Text text = obj.AddComponent<Text>();
		obj.AddComponent<Outline>();
		obj.AddComponent<IgnoreTouch>();

		if (large)
		{
			text.font = Resources.Load<Font>("Fonts/JKG-L");
		}
		else
		{
			text.font = Resources.Load<Font>("Fonts/JKG-M");
		}

		text.text = textValue;
		text.color = Color.red;
		text.fontSize = size;
		text.alignment = TextAnchor.MiddleCenter;
		obj.transform.SetParent(parent);
		obj.GetComponent<RectTransform>().localPosition = pos;
		obj.GetComponent<RectTransform>().sizeDelta = new Vector2(90, 90);

		return obj;
	}
}

// ボタンの邪魔をするタッチ判定を消すクラス
public class IgnoreTouch : Button, ICanvasRaycastFilter
{
	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		return false;
	}
}