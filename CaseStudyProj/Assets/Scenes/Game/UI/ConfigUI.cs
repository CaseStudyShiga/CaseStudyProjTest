using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

class ConfigUI : UIBase
{
	readonly Vector2 SIZE = new Vector3(450f, 150f, 0f);
	readonly Vector3 CONFIG_POS = new Vector3(0, 0, 0);

	GameObject _background;
	GameObject _frame;
	GameObject _speedupBtn;
	GameObject _turnendChkBtn;
	GameObject _returnMenuBtn;
	GameObject _dataDeleteBtn;

	GameObject _okBtn;
	GameObject _noBtn;

	public bool IsMenu;

	void Start()
	{
		this.InitField();
	}

	void Update()
	{
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._background = this.CreateChild("BackGround", this.transform, Vector2.one, Vector3.zero);
		this._background.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
		this._frame = this.CreateChild("Frame", this.transform, Vector2.one, Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/configwindow"));

		this._speedupBtn = this.CreateButton("SpeedBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y + 200), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx1"), this.SpeedUpAction);
		this._turnendChkBtn = this.CreateButton("TurnChkBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y + 50), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_turnendcheckoff"), this.TurnEndChkAction);
		this._dataDeleteBtn = this.CreateButton("DeleteBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y - 100), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_data_delete"), this.DeleteBtnAction);
		this._returnMenuBtn = this.CreateButton("ReturnMenuBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y - 250), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_backtomenu"), this.ReturnMenuBtnAction);

		this._okBtn = this.CreateButton("OkBtn", this.transform, Vector2.one, new Vector3(0, -100), Resources.Load<Sprite>("Sprites/GUI/turnendWindow_yes"), this.OkBtnAction);
		this._noBtn = this.CreateButton("ResumeBtn", this.transform, Vector2.one, new Vector3(0, -250), Resources.Load<Sprite>("Sprites/GUI/turnendWindow_no"), this.NoBtnAction);

		this.SpeedUpIconChk();
		this.TurnEndIconChk();	
	}

	void SpeedUpIconChk()
	{
		switch (GameManager.Instance.SpeedUpType)
		{
			case GameManager.SpeedUp.x1:
				this._speedupBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx1");
				break;
			case GameManager.SpeedUp.x2:
				this._speedupBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx2");
				break;
			case GameManager.SpeedUp.x3:
				this._speedupBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx3");
				break;
		}
	}

	void TurnEndIconChk()
	{
		bool chk = GameManager.Instance.isTurnEndChk;
		Image image = this._turnendChkBtn.GetComponent<Image>();
		image.sprite = (chk) ? Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_turnendcheck") : Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_turnendcheckoff");
	}


	// 設定画面へ
	public void ConfigAction()
	{
		this.SpeedUpIconChk();
		this.TurnEndIconChk();

		_speedupBtn.transform.DOScale(SIZE, 0.2f);
		_turnendChkBtn.transform.DOScale(SIZE, 0.2f);
		_dataDeleteBtn.transform.DOScale(SIZE, 0.2f);
		_returnMenuBtn.transform.DOScale(SIZE, 0.2f);
	}

	void SpeedUpAction()
	{
		switch (GameManager.Instance.SpeedUpType)
		{
			case GameManager.SpeedUp.x1:
				GameManager.Instance.SpeedUpType = GameManager.SpeedUp.x2;
				break;
			case GameManager.SpeedUp.x2:
				GameManager.Instance.SpeedUpType = GameManager.SpeedUp.x3;
				break;
			case GameManager.SpeedUp.x3:
				GameManager.Instance.SpeedUpType = GameManager.SpeedUp.x1;
				break;
		}

		this.SpeedUpIconChk();
	}

	void TurnEndChkAction()
	{
		GameManager.Instance.isTurnEndChk ^= true;
		this.TurnEndIconChk();
	}

	void ReturnMenuBtnAction()
	{
		if (IsMenu)
		{
			this._background.transform.localScale = Vector3.zero;
			this.NotActiveMethod();
		}
		else
		{
			this._background.transform.localScale = Vector3.zero;

			_speedupBtn.transform.DOScale(Vector3.zero, 0.2f);
			_turnendChkBtn.transform.DOScale(Vector3.zero, 0.2f);
			_dataDeleteBtn.transform.DOScale(Vector3.zero, 0.2f);
			_returnMenuBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
				_frame.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=> {
					GameManager.Instance.SaveConfigData();
				});
			});
		}
	}

	void DeleteBtnAction()
	{
		_speedupBtn.transform.DOScale(Vector3.zero, 0.2f);
		_turnendChkBtn.transform.DOScale(Vector3.zero, 0.2f);
		_dataDeleteBtn.transform.DOScale(Vector3.zero, 0.2f);
		_returnMenuBtn.transform.DOScale(Vector3.zero, 0.2f);

		_frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/data_delete_window");
		_noBtn.transform.DOScale(new Vector2(400, 150), 0.2f);
		_okBtn.transform.DOScale(new Vector2(400, 150), 0.2f);
	}

	void OkBtnAction()
	{
		SaveManager.Instance.Reset();
		_frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/configwindow");

		if (IsMenu)
		{
			this._background.transform.localScale = Vector3.zero;
			this.NoBtnAction();
		}
		else
		{
			this._background.transform.localScale = Vector3.zero;

			var select = this.transform.parent.GetComponent<Select>();
			if (select.Mode == Select.eMode.eStage)
			{
				var stageSelect = select.StageSelect.GetComponent<StageSelect>();
				stageSelect.DeleteStages();
				stageSelect.CreateStages();
			}

			_noBtn.transform.DOScale(Vector3.zero, 0.2f);
			_okBtn.transform.DOScale(Vector3.zero, 0.2f);
			_speedupBtn.transform.DOScale(Vector3.zero, 0.2f);
			_turnendChkBtn.transform.DOScale(Vector3.zero, 0.2f);
			_dataDeleteBtn.transform.DOScale(Vector3.zero, 0.2f);
			_returnMenuBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
				_frame.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
					GameManager.Instance.SaveConfigData();
				});
			});
		}

	}

	void NoBtnAction()
	{
		_frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/configwindow");
		_speedupBtn.transform.DOScale(SIZE, 0.2f);
		_turnendChkBtn.transform.DOScale(SIZE, 0.2f);
		_dataDeleteBtn.transform.DOScale(SIZE, 0.2f);
		_returnMenuBtn.transform.DOScale(SIZE, 0.2f);

		_noBtn.transform.DOScale(Vector3.zero, 0.2f);
		_okBtn.transform.DOScale(Vector3.zero, 0.2f);
	}

	public void ActiveMethod()
	{
		if (IsMenu)
		{
			this._background.transform.localScale = Vector3.zero;
			_frame.transform.localScale = new Vector3(750f, 1334f, 0);
			_speedupBtn.transform.DOScale(SIZE, 0.2f);
			_turnendChkBtn.transform.DOScale(SIZE, 0.2f);
			_dataDeleteBtn.transform.DOScale(SIZE, 0.2f);
			_returnMenuBtn.transform.DOScale(SIZE, 0.2f);
		}
		else
		{
			this._background.transform.localScale = new Vector3(750f, 1334f, 0f);

			_frame.transform.DOScale(new Vector3(750f, 1334f, 0), 0.25f).OnComplete(() => {
				_speedupBtn.transform.DOScale(SIZE, 0.2f);
				_turnendChkBtn.transform.DOScale(SIZE, 0.2f);
				_dataDeleteBtn.transform.DOScale(SIZE, 0.2f);
				_returnMenuBtn.transform.DOScale(SIZE, 0.2f);
			});
		}
	}

	void NotActiveMethod()
	{
		_speedupBtn.transform.DOScale(Vector3.zero, 0.2f);
		_turnendChkBtn.transform.DOScale(Vector3.zero, 0.2f);
		_dataDeleteBtn.transform.DOScale(Vector3.zero, 0.2f);
		_returnMenuBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
			_frame.transform.localScale = new Vector3(0f, 0f, 0f);
			GameManager.Instance.SaveConfigData();
		});
	}
}
