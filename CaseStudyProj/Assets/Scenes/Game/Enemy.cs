using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharBase
{
	void ClickEvent()
	{
		var stage = this.Status.Stage.GetComponent<Stage>();

		this.SetStatusUI();

		if (this.Status.IsSelect == false)
		{
			this.OtherSelectOff();

			if (this.Status.IsBetween == false)
			{
				this.Status.SelectOn();
			}

			stage.ClearPossibleMovePanel();
			stage.Search(this.Status.X, this.Status.Y - 1, this.Status.Move, 1, this.Status.Range);
			stage.Search(this.Status.X + 1, this.Status.Y, this.Status.Move, 2, this.Status.Range);
			stage.Search(this.Status.X, this.Status.Y + 1, this.Status.Move, 3, this.Status.Range);
			stage.Search(this.Status.X - 1, this.Status.Y, this.Status.Move, 4, this.Status.Range);
			stage.AllBannPanelCol();
		}
		else
		{
			this.Status.SelectOff();
			stage.ClearPossibleMovePanel();
		}
	}

	public GameObject Create(int idx, Transform statge, string type, int x, int y)
	{
		this._instance = this.CreateChild(type, "enemy" + idx.ToString(), statge.Find("Enemys"), statge.gameObject, new Vector2(90, 90));
		this.Status.SetColor(new Color32(200, 120, 255, 255), new Color32(255, 200, 0, 255));
		this.Status.SetPos(x, y);
		this.Status.IsPlayer = false;
		this.Status.Index = idx;
		this.Status.MovedOff();
		this._instance.GetComponent<Button>().onClick.AddListener(this.ClickEvent);

		return this._instance;
	}
}