using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class TutorialUI : UIBase
{
	readonly Vector2 SIZE = new Vector3(550f, 150f, 0f);

	GameObject _background;
	GameObject _frame;
	GameObject _button;
	int _areaId;
	int _stageId;
	bool _ischk = false;

	void Start()
	{
		this.InitField();
		if (this._ischk == false)
		{
			this.ActiveMethod();
		}
	}

	void Update()
	{
	}

	void ButtonAction()
	{
		this.NotActiveMethod();
	}

	public void ActiveMethod()
	{
		if (this._ischk == false)
		{
			this._background.transform.localScale = new Vector3(750f, 1334f, 0f);

			_frame.transform.DOScale(Vector3.one, 0.25f).OnComplete(() =>
			{
				this._ischk = true;
				_button.transform.DOScale(Vector3.one, 0.2f);
			});
		}
	}

	void NotActiveMethod()
	{
		this._background.transform.localScale = Vector3.zero;

		_button.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => 
		{
			_frame.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => 
			{
				GameManager.Instance.SaveConfigData();
			});
		});
	}

	void InitField()
	{
		this._areaId = CSVDataReader.Instance.AreaID;
		this._stageId = CSVDataReader.Instance.StageID;
		this._ischk = false;

		this.transform.localPosition = Vector3.zero;
		this._background = this.CreateChild("BackGround", this.transform, Vector2.one, Vector3.zero);
		this._background.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
		this._frame = this.CreateChild("Frame", this.transform, new Vector2(750f, 1334f), Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/result_frame"));
		this._button = this.CreateButton("NextBtn", this.transform, SIZE, new Vector3(0, -410), Resources.Load<Sprite>("Sprites/GUI/tip_button"), this.ButtonAction);
		this._background.transform.SetAsFirstSibling();

		var image = this._frame.GetComponent<Image>();
		switch (this._stageId)
		{
			case 0:
				image.sprite = Resources.Load<Sprite>("Sprites/GUI/tip_01");
				break;
			case 1:
				image.sprite = Resources.Load<Sprite>("Sprites/GUI/tip_02");
				break;
			case 2:
				image.sprite = Resources.Load<Sprite>("Sprites/GUI/tip_03");
				break;
			case 3:
				image.sprite = Resources.Load<Sprite>("Sprites/GUI/tip_04");
				break;
		}

		if (_areaId == 0 && (_stageId == 0 || _stageId == 1 || _stageId == 2 || _stageId == 3))
		{
			_ischk = false;
		}
		else
		{
			_ischk = true;
		}

		this._background.transform.localScale = Vector3.zero;
		this._frame.transform.localScale = Vector3.zero;
		this._button.transform.localScale = Vector3.zero;
	}
}
