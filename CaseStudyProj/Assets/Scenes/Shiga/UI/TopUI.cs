using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : UIBase
{
	GameObject _background;
	GameObject _face;
	GameObject _statusUI;
	GameObject _totalTurn;
	public GameObject StatusUI { get { return _statusUI; } }
	int _turn;

	void Start ()
	{
		this.InitField();
	}

	void Update ()
	{
		_turn = GameManager.Instance.TotalTurnNum;
		this._totalTurn.transform.Find("Value").GetComponent<Text>().text = this._turn.ToString().PadLeft(2, '0');
	}

	void InitField()
	{
		this._background = this.CreateChild("BackGround", this.transform, new Vector2(661, 177), new Vector3(0, -555));
		this._face = this.CreateChild("FaceImage", this.transform, new Vector2(160, 160), new Vector3(-225, -555));
		this._statusUI = this.CreateStatusUI();
		this._totalTurn = this.CreateChild("TotalTurn", this.transform, new Vector2(160, 160), new Vector3(225, -555));
		this._totalTurn.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
		this.CreateText("Name", "TotalTurn", this._totalTurn.transform, new Vector3(0, 50), 25);
		this.CreateText("Value", "00", this._totalTurn.transform, new Vector3(0, -10), 80);

		this._background.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
		this._background.transform.SetSiblingIndex(0);
		this._face.transform.SetSiblingIndex(1);
	}

	GameObject CreateStatusUI()
	{
		GameObject child = new GameObject("StatusUI");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>().localPosition = new Vector3(-7, -560);
		child.AddComponent<StatusUI>();

		return child;
	}
}
