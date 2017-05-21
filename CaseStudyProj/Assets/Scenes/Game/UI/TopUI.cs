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
	int _turn;
	GameObject _obj;

	public GameObject StatusUI { get { return _statusUI; } }
	public GameObject Obj { get { return _obj; } set { _obj = value; } }

	void Start ()
	{
		this.InitField();
	}

	void Update ()
	{
		_turn = GameManager.Instance.TotalTurnNum;
		this._totalTurn.transform.Find("Value").GetComponent<Text>().text = this._turn.ToString();

		if (_statusUI && _obj)
		{
			var status = _obj.GetComponent<StatusBase>();
			var statusUI = _statusUI.GetComponent<StatusUI>();
			statusUI.SetHpText(status.Hp.ToString(), status.HpMax.ToString());
			statusUI.SetSimpleText("Name", status.Name);
			statusUI.SetSimpleText("Attack", status.Attack.ToString());
			statusUI.SetSimpleText("Move", status.Move.ToString());
			statusUI.SetSimpleText("Range", status.Range.ToString());
			statusUI.transform.parent.GetComponent<TopUI>().SetFaceSprite(status.CharSp);
		}
	}

	void InitField()
	{
		this._background = this.CreateChild("BackGround", this.transform, new Vector2(750, 278), new Vector3(0, -542), Resources.Load<Sprite>("Sprites/GUI/topUI"));
		this._face = this.CreateChild("FaceImage", this.transform, new Vector2(260, 260), new Vector3(-245, -533));
		this._statusUI = this.CreateStatusUI();
		this._totalTurn = this.CreateChild("TotalTurn", this.transform, new Vector2(160, 160), new Vector2(303, -471));
		this._totalTurn.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
		this._obj = null;

		this.CreateText("Value", "0", this._totalTurn.transform, new Vector3(0, -10), 48, false);

		this._face.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
		this._face.transform.SetSiblingIndex(0);
		this._background.transform.SetSiblingIndex(1);
	}

	GameObject CreateStatusUI()
	{
		GameObject child = new GameObject("StatusUI");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>().localPosition = new Vector3(116, -557);
		child.AddComponent<StatusUI>();

		return child;
	}

	public void SetFaceSprite(Sprite sp)
	{
		this._face.GetComponent<Image>().sprite = sp;
		this._face.GetComponent<Image>().color = Color.white;
	}
}
