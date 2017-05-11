using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BottomUI : UIBase
{
	GameObject _background;
	GameObject _buttons;
	GameObject _turnEndButton;
	GameObject _returnButton;
	GameObject _menuButton;

	void Start ()
	{
		this.InitField();
	}
	
	void Update ()
	{
	}

	void InitField()
	{
		this._background = this.CreateChild("BackGround", this.transform, new Vector2(661, 123), new Vector3(-7, -555));
		this._background.GetComponent<Image>().color = new Color32(255,255,255,100);
		this._background.transform.SetAsFirstSibling();

		this._buttons = new GameObject("Buttons");
		this._buttons.transform.SetParent(this.transform);
		this._buttons.transform.localPosition = Vector3.zero;

		this._turnEndButton = this.CreateButton("TurnEnd", "ターン\n終了", new Vector2(80, 80), new Vector3(-257, -560), () => {
		});
		this._returnButton = this.CreateButton("Return", "一手\n戻る", new Vector2(80, 80), new Vector3(-7, -560), () => {
		});
		this._menuButton = this.CreateButton("Menu", "メニュー", new Vector2(80, 80), new Vector3(243, -560), () => {
		});
	}

	GameObject CreateButton(string name, string text, Vector2 size, Vector3 pos, UnityAction buttonMethod, Sprite sp = null)
	{
		GameObject child = this.CreateChild(name, this._buttons.transform, size, pos, sp);
		child.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 45);
		child.AddComponent<Button>().onClick.AddListener(buttonMethod);

		this.CreateText("Text", text, child.transform, new Vector3(-1, -2.5f), 25);

		return child;
	}
}
