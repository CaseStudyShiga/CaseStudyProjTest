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
		_name = this.CreateName(new Vector2(246, 48), new Vector3(1.25f, 55));
		_hp = this.CreateHp(new Vector2(246, 28), new Vector3(1.25f, 10));
		_attack = this.CreateSimple("Attack", "攻撃", "00", new Vector2(120, 25), new Vector3(-62, -24));
		_move = this.CreateSimple("Move", "移動量", "0", new Vector2(120, 25), new Vector3(62, -24));
		_range = this.CreateSimple("Range", "射程", "00", new Vector2(120, 25), new Vector3(-62, -55));
	}

	public void SetHpText(string value, string max)
	{
		this.GetText(this._hp.transform, "Value").text = value;
		this.GetText(this._hp.transform, "Max").text = max;
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

	GameObject CreateName(Vector2 size, Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject("Name");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;
		child.AddComponent<Image>().sprite = sp;
		child.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

		GameObject text = this.CreateText("Text", "Name", child.transform, Vector3.zero);
		text.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);

		return child;
	}

	GameObject CreateHp(Vector2 size, Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject("Hp");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;
		child.AddComponent<Image>().sprite = sp;
		child.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

		GameObject typeText = this.CreateText("Type", "Hp", child.transform, new Vector3(-64, 0), 25);
		GameObject valueText = this.CreateText("Value", "00", child.transform, new Vector3(-5, 0), 25);
		GameObject maxText = this.CreateText("Max", "99", child.transform, new Vector3(60, 0), 25);
		GameObject slashText = this.CreateText("Slash", "/", child.transform, new Vector3(28, 0), 25);

		return child;
	}

	GameObject CreateSimple(string name, string type, string value, Vector2 size, Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;
		child.AddComponent<Image>().sprite = sp;
		child.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

		GameObject typeText = this.CreateText("Type", type, child.transform, new Vector3(-25, 0), 20);
		GameObject valueText = this.CreateText("Value", value, child.transform, new Vector3(24, 0), 20);

		return child;
	}
}
