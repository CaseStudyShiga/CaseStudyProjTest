using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharBase
{
	GameObject _player;

	void ClickEvent()
	{
		var stage = this.Status.Stage.GetComponent<Stage>();

		if (this.Status.IsSelect == false)
		{
			this.OtherPlayersSelectOff();
			this.Status.SelectOn();

			stage.ClearPossibleMovePanel();
			stage.Search(this.Status.X, this.Status.Y - 1, this.Status.Move, 1);
			stage.Search(this.Status.X + 1, this.Status.Y, this.Status.Move, 2);
			stage.Search(this.Status.X, this.Status.Y + 1, this.Status.Move, 3);
			stage.Search(this.Status.X - 1, this.Status.Y, this.Status.Move, 4);
		}
		else
		{
			this.Status.SelectOff();
			stage.ClearPossibleMovePanel();
		}
	}

	public GameObject Create(Transform stage, StatusBase.eType type, int x, int y)
	{
		_player = this.CreateChild(type, "player", stage.Find("Players"), stage.gameObject, null, new Vector2(60, 60));
		_player.GetComponent<StatusBase>().SelectOff();
		_player.GetComponent<Button>().onClick.AddListener(this.ClickEvent);
		this.Status.SetPos(x, y);
		this.Status.IsPlayer = true;
		this.Status.SetColor(new Color32(0, 0, 255, 255), new Color32(0, 127, 255, 255), new Color32(255, 193, 143, 255));

		return _player;
	}

	void OtherPlayersSelectOff()
	{
		foreach (Transform child in this._player.transform.parent)
		{
			child.GetComponent<StatusBase>().SelectOff();
		}
		this.Status.SelectOn();
	}
}