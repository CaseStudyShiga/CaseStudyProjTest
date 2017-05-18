using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharBase
{
	GameObject _enemy;

	void ClickEvent()
	{
		this.SetStatusUI();
	}

	void SetStatusUI()
	{
		var stage = this.Status.Stage.GetComponent<Stage>();
		var statusUI = stage.transform.parent.Find("TopUI/StatusUI").GetComponent<StatusUI>();

		if (statusUI)
		{
			statusUI.SetHpText(this.Status.Hp.ToString(), this.Status.HpMax.ToString());
			statusUI.SetSimpleText("Name", this.Status.Name);
			statusUI.SetSimpleText("Attack", this.Status.Attack.ToString());
			statusUI.SetSimpleText("Move", this.Status.Move.ToString());
			statusUI.SetSimpleText("Range", this.Status.Range.ToString());
			statusUI.transform.parent.GetComponent<TopUI>().SetFaceSprite(this._enemy.GetComponent<Image>().sprite);
		}
	}

	public GameObject Create(Transform statge, StatusBase.eType type, int x, int y)
	{
		_enemy = this.CreateChild(type, "enemy", statge.Find("Enemys"), statge.gameObject, new Vector2(90, 90));
		this.Status.SetColor(new Color32(255, 255, 255, 255), new Color32(200, 120, 255, 255), new Color32(255, 200, 0, 255), new Color32(150, 0, 255, 255));
		this.Status.SetPos(x, y);
		this.Status.IsPlayer = false;
		this.Status.MovedOff();
		this.Status.Name = "村瀬克磨";
		_enemy.GetComponent<Button>().onClick.AddListener(this.ClickEvent);

		return _enemy;
	}
}