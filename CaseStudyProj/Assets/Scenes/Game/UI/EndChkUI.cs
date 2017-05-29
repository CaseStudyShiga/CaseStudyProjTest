using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class EndChkUI : UIBase
{
	readonly Vector2 SIZE = new Vector3(450f, 150f, 0f);
	readonly Vector3 POS = new Vector3(0, -50, 0);

	GameObject _background;
	GameObject _frame;

	GameObject _okBtn;
	GameObject _resumeBtn;

	bool _endchkOk;
	public bool IsEndChkOk { get { return _endchkOk; } }

	void Start()
	{
		this.InitField();
	}

	void Update()
	{
	}

	void OkBtnAction()
	{
		_endchkOk = true;
		this.NotActiveMethod();
	}

	void ResumeAction()
	{
		_endchkOk = false;
		this.NotActiveMethod();
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._background = this.CreateChild("BackGround", this.transform, Vector2.one, Vector3.zero);
		this._background.GetComponent<Image>().color = new Color32(0, 0, 0, 100);

		this._frame = this.CreateChild("Frame", this.transform, Vector2.one, Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_menuwindow"));

		this._okBtn = this.CreateButton("OkBtn", this.transform, Vector2.one, new Vector3(0, POS.y - 50), null, this.OkBtnAction);
		this._resumeBtn = this.CreateButton("ResumeBtn", this.transform, Vector2.one, new Vector3(0, POS.y - 200), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_resume"),this.ResumeAction);

		this._background.transform.SetSiblingIndex(0);
	}

	public void ActiveMethod()
	{
		this.gameObject.SetActive(true);

		this._background.transform.localScale = new Vector3(750f, 1334f, 0f);

		_frame.transform.DOScale(new Vector3(750f, 1334f, 0), 0.25f).OnComplete(() => {
			_resumeBtn.transform.DOScale(SIZE, 0.2f);
			_okBtn.transform.DOScale(SIZE, 0.2f);
		});
	}

	void NotActiveMethod()
	{
		var bottomUI = this.transform.parent.Find("BottomUI");
		var ui = bottomUI.GetComponent<BottomUI>();

		this._background.transform.localScale = Vector3.zero;

		_okBtn.transform.DOScale(Vector3.zero, 0.2f);
		_resumeBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
			_frame.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {
				if (this._endchkOk)
				{
					StartCoroutine(ui.TurnEndAction());
				}
			});
		});

	}

}