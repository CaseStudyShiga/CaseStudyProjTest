using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class StatusUI : UIBase
{
	GameObject _name;
	GameObject _hp;
	GameObject _attack;
	GameObject _move;
	GameObject _range;

	void Start()
	{
		this.InitField();
	}

	void Update()
	{
	}

	void InitField()
	{
		_name = this.CreateName(new Vector3(1.25f, 92));
		_hp = this.CreateHp(new Vector3(-61, -6));
		_attack = this.CreateSimple("Attack", "攻撃", "00", new Vector3(-70, -72));
		_move = this.CreateSimple("Move", "移動量", "0", new Vector3(125, -72));
		_range = this.CreateSimple("Range", "射程", "00", new Vector3(128, -6));
	}

	public void SetHpText(string value, string max)
	{
		this.GetText(this._hp.transform, "Value").text = value;
		this.GetText(this._hp.transform, "Max").text = "/" + max;
	}

	public void SetSimpleText(string name, string value)
	{
		switch (name)
		{
			case "Name":
				this.GetText(this._name.transform, "Text").text = value;
				break;
			case "Attack":
				this.GetText(this._attack.transform, "Value").text = value;
				break;
			case "Move":
				this.GetText(this._move.transform, "Value").text = value;
				break;
			case "Range":
				this.GetText(this._range.transform, "Value").text = value;
				break;
		}
	}

	GameObject CreateName(Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject("Name");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;

		GameObject text = this.CreateText("Text", "Name", child.transform, Vector3.zero, 30, false);
		text.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);

		return child;
	}

	GameObject CreateHp(Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject("Hp");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;

		GameObject valueText = this.CreateText("Value", "00", child.transform, Vector3.zero, 25, false);
		GameObject maxText = this.CreateText("Max", "/99", child.transform, new Vector3(36, -5), 21);

		return child;
	}

	GameObject CreateSimple(string name, string type, string value, Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;

		GameObject valueText = this.CreateText("Value", value, child.transform, new Vector3(24, 0), 25, false);

		return child;
	}
}
