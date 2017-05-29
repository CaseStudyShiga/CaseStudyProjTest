using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharBase
{
	protected GameObject _instance;

	StatusBase _status;
	public StatusBase Status { get { return _status; } set { _status = value; } }

	protected void SetStatusUI()
	{
		var stage = this.Status.Stage.GetComponent<Stage>();
		var TopUIObj = stage.transform.parent.Find("UI/TopUI");

		if (TopUIObj)
		{
			var ui = TopUIObj.GetComponent<TopUI>();
			ui.Obj = this._instance;
		}
	}

	protected GameObject CreateChild(int type, string name, Transform parent,GameObject stage, Vector2 size)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.AddComponent<Image>();
		child.AddComponent<Button>();

		switch (type)
		{
			case 0:
				_status = child.AddComponent<AttackStatus>();
				break;

			case 1:
				break;

			case 2:
				_status = child.AddComponent<EnemyStatus0>();
				break;
		}

		child.GetComponent<Image>().sprite = _status.CharSp;

		this._status.InitField();
		this._status.Stage = stage;

		return child;
	}

	protected void OtherSelectOff()
	{
		foreach (Transform child in this._status.Stage.transform.Find("Players"))
		{
			child.GetComponent<StatusBase>().SelectOff();
		}

		foreach (Transform child in this._status.Stage.transform.Find("Enemys"))
		{
			if(child.GetComponent<StatusBase>().IsBetween == false)
				child.GetComponent<StatusBase>().SelectOff();
		}
	}
}
