using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy
{
	GameObject _enemy;
	CharBase _charBase;

	public GameObject Create(GameObject parent, CharBase.eType type, int x, int y)
	{
		_enemy = this.CreateChild(type, "enemy", parent, null, new Vector2(60, 60));
		_charBase.SetPos(x, y);

		return _enemy;
	}

	GameObject CreateChild(CharBase.eType type, string name, GameObject parent, Sprite sp, Vector2 size)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent.transform.Find("Enemys"));
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.AddComponent<Image>().sprite = sp;
		child.GetComponent<Image>().color = new Color32(200, 0, 255, 255);

		switch (type)
		{
			case CharBase.eType.eAttacker:
				_charBase = child.AddComponent<AttackChar>();
				break;

			case CharBase.eType.eDefender:
				break;
		}

		_charBase.Stage = parent;

		return child;
	}
}