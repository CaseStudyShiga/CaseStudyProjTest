using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	void EndAction()
	{
		if (GameManager.Instance.isEnemyTurn == false)
		{
			if (GameManager.Instance.isTurnEndChk)
			{
				var endchkUI = this.transform.parent.Find("EndChkUI");
				var end = endchkUI.GetComponent<EndChkUI>();
				end.ActiveMethod();
			}
			else
			{
				StartCoroutine(this.TurnEndAction());
			}
		}
	}

	// ターン終了
	public IEnumerator TurnEndAction()
	{
		if (GameManager.Instance.isEnemyTurn == false)
		{
			var stagebase = this.transform.parent.parent.Find("Stage").GetComponent<StageBase>();
			stagebase.ClearPossibleMovePanel();

			stagebase.AttackPlayers();
			stagebase.DamageEnemy();
			yield return StartCoroutine(stagebase.EnemysTurn());

			stagebase.ClearStackPlayer();
			stagebase.AllMovedOff();
			stagebase.ClearPossibleMovePanel();
			stagebase.AllCheckBetween();

			stagebase.CheckGameComplete();
			GameManager.Instance.AddTurn();

			if (GameManager.Instance.isComplete)
			{
				yield return new WaitForSeconds(1.0f);          // waitTime後に実行する

				var result = this.transform.parent.Find("ResultUI").GetComponent<ResultUI>();
				result.ActiveMethod();
			}
		}
	}

	// 一手戻る
	void ReturnAction()
	{
		if (GameManager.Instance.isEnemyTurn == false)
		{
			var stagebase = this.transform.parent.parent.Find("Stage").GetComponent<StageBase>();
			stagebase.UndoPlayer();
		}
	}

	// メニュー
	void MenuAction()
	{
		var menuUI = this.transform.parent.Find("MenuUI");

		menuUI.GetComponent<MenuUI>().ActiveMethod();
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

		this._turnEndButton = this.CreateButton("TurnEnd", this._buttons.transform, SIZE, new Vector3(300, -547), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_turnEnd"), this.EndAction);
		this._returnButton = this.CreateButton("Return", this._buttons.transform, SIZE, new Vector3(155, -547), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_undo"), this.ReturnAction);
		this._menuButton = this.CreateButton("Menu", this._buttons.transform, SIZE, new Vector3(-287, -547), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_menu"), this.MenuAction);
	}
}
