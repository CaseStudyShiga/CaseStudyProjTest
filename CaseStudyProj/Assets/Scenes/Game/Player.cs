using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharBase
{
	void ClickEvent()
	{
		var stage = this.Status.Stage.GetComponent<Stage>();

		this.SetStatusUI();

		if (this.Status.IsSelect == false)
		{
			this.OtherSelectOff();
			this.Status.SelectOn();

			stage.ClearPossibleMovePanel();
			stage.SetALlBanPanelCol();
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

		this.Status.SetEffect(stage.GetPanelLocalPosition(this.Status.X, this.Status.Y));
	}

	public GameObject Create(Transform stage, int type, int x, int y)
	{
		this._instance = this.CreateChild(type, "player", stage.Find("Players"), stage.gameObject, new Vector2(90, 90));
		this._instance.GetComponent<StatusBase>().SelectOff();
		this._instance.GetComponent<Button>().onClick.AddListener(this.ClickEvent);

		this.Status.SetColor(new Color32(200, 200, 255, 255), new Color32(255, 193, 143, 255));
		this.Status.SetPos(x, y);
		this.Status.MovedOff();
		this.Status.IsPlayer = true;

		return _instance;
	}
}