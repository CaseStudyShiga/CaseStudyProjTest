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

	// ターン終了
	IEnumerator TurnDndAction()
	{
		if (GameManager.Instance.isEnemyTurn == false)
		{
			var stagebase = this.transform.parent.Find("Stage").GetComponent<StageBase>();
			stagebase.ClearPossibleMovePanel();

			GameManager.Instance.TotalTurnNum++;

			stagebase.AttackPlayers();
			stagebase.DamageEnemy();
			yield return StartCoroutine(stagebase.EnemysTurn());

			stagebase.ClearStackPlayer();
			stagebase.AllMovedOff();
			stagebase.ClearPossibleMovePanel();
			stagebase.AllCheckBetween();
		}
	}

	// 一手戻る
	void ReturnAction()
	{
		if (GameManager.Instance.isEnemyTurn == false)
		{
			var stagebase = this.transform.parent.Find("Stage").GetComponent<StageBase>();
			stagebase.UndoPlayer();
		}
	}

	// メニュー
	void MenuAction()
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

		this._turnEndButton = this.CreateButton("TurnEnd", new Vector3(300, -547), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_turnEnd"), () => { StartCoroutine(this.TurnDndAction()); });
		this._returnButton = this.CreateButton("Return", new Vector3(155, -547), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_undo"), this.ReturnAction);
		this._menuButton = this.CreateButton("Menu", new Vector3(-287, -547), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_menu"), this.MenuAction);
	}

	// ボタン生成
	GameObject CreateButton(string name, Vector3 pos, Sprite sp, UnityAction buttonMethod)
	{
		GameObject child = this.CreateChild(name, this._buttons.transform, SIZE, pos, sp);
		child.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
		child.AddComponent<Button>().onClick.AddListener(buttonMethod);

		this.CreateText("Text", "", child.transform, new Vector3(-1, -2.5f), 25);

		return child;
	}
}
