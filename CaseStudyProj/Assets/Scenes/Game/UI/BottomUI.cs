using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BottomUI : UIBase
{
	readonly Vector2 SIZE = new Vector2(150,150);

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
		this.transform.localPosition = new Vector3(0, -50, 0);

		this._background = this.CreateChild("BackGround", this.transform, new Vector2(750, 129), new Vector3(0, -550), Resources.Load<Sprite>("Sprites/GUI/bottomUI"));
		this._background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
		this._background.transform.SetSiblingIndex(0);

		this._buttons = new GameObject("Buttons");
		this._buttons.transform.SetParent(this.transform);
		this._buttons.transform.localPosition = Vector3.zero;

		this._turnEndButton = this.CreateButton("TurnEnd", "", SIZE, new Vector3(300, -547), () => {
			GameManager.Instance.TotalTurnNum++;
			var stagebase = this.transform.parent.Find("Stage").GetComponent<StageBase>();
			stagebase.AllMovedOff();
			stagebase.ClearPossibleMovePanel();
			stagebase.ClearStackPlayer();
			stagebase.AttackPlayers();
			stagebase.AllCheckBetween();
		}, Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_turnEnd"));

		this._returnButton = this.CreateButton("Return", "", SIZE, new Vector3(155, -547), () => {
			var stagebase = this.transform.parent.Find("Stage").GetComponent<StageBase>();
			stagebase.UndoPlayer();
		}, Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_undo"));

		this._menuButton = this.CreateButton("Menu", "", SIZE, new Vector3(-287, -547), () => {
		}, Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_menu"));
	}

	GameObject CreateButton(string name, string text, Vector2 size, Vector3 pos, UnityAction buttonMethod, Sprite sp = null)
	{
		GameObject child = this.CreateChild(name, this._buttons.transform, size, pos, sp);
		child.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
		child.AddComponent<Button>().onClick.AddListener(buttonMethod);

		this.CreateText("Text", text, child.transform, new Vector3(-1, -2.5f), 25);

		return child;
	}
}
