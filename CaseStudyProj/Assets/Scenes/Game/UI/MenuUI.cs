using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class MenuUI : UIBase
{
	readonly Vector2 SIZE = new Vector3(450f, 150f, 0f);
	readonly Vector3 POS = new Vector3(0,-50,0);
	readonly Vector3 CONFIG_POS = new Vector3(0, -70, 0);

	GameObject _background;
	GameObject _frame;

	// menu
	GameObject _resumeBtn;
	GameObject _selectBtn;
	GameObject _resetBtn;
	GameObject _configButton;

	// setting
	GameObject _speedupBtn;
	GameObject _turnendChkBtn;
	GameObject _returnMenuBtn;

	void Start()
	{
		this.InitField();
		this.NotActiveMethod();
	}

	void Update()
	{
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

	// ステージリセット
	void ResetAction()
	{
		Debug.Log("Reset");
		var stage = this.transform.parent.parent.Find("Stage").GetComponent<Stage>();
		var topUI = this.transform.parent.Find("TopUI").GetComponent<TopUI>();

		stage.Reset();
		topUI.Reset();
		GameManager.Instance.Reset();

		this.NotActiveMethod();
	}

	// ステージ選択へ
	void SelectAction()
	{
	}

	// 設定画面へ
	void ConfigAction()
	{
		_resetBtn.transform.DOScale(Vector3.zero, 0.2f);
		_selectBtn.transform.DOScale(Vector3.zero, 0.2f);
		_configButton.transform.DOScale(Vector3.zero, 0.2f);
		_resumeBtn.transform.DOScale(Vector3.zero, 0.2f);

		this.SpeedUpIconChk();
		this.TurnEndIconChk();

		_speedupBtn.transform.DOScale(SIZE, 0.2f);
		_turnendChkBtn.transform.DOScale(SIZE, 0.2f);
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
		_speedupBtn.transform.DOScale(Vector3.zero, 0.2f);
		_turnendChkBtn.transform.DOScale(Vector3.zero, 0.2f);
		_returnMenuBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=> {
			_resumeBtn.transform.DOScale(SIZE, 0.2f);
			_selectBtn.transform.DOScale(SIZE, 0.2f);
			_resetBtn.transform.DOScale(SIZE, 0.2f);
			_configButton.transform.DOScale(SIZE, 0.2f);
		});
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._background = this.CreateChild("BackGround", this.transform, Vector2.one, Vector3.zero);
		this._background.GetComponent<Image>().color = new Color32(0,0,0,100);

		this._frame = this.CreateChild("Frame", this.transform, Vector2.one, Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_menuwindow"));

		this._resetBtn = this.CreateButton("ResetBtn", this.transform, Vector2.one, new Vector3(0, POS.y + 250), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_retry"), this.ResetAction);
		this._selectBtn = this.CreateButton("SelectBtn", this.transform, Vector2.one, new Vector3(0, POS.y + 100), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_stageselect"), this.SelectAction);
		this._configButton = this.CreateButton("ConfigBtn", this.transform, Vector2.one, new Vector3(0, POS.y - 50), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_config"), this.ConfigAction);
		this._resumeBtn = this.CreateButton("ResumeBtn", this.transform, Vector2.one, new Vector3(0,POS.y - 200), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_resume"), this.NotActiveMethod);

		this._speedupBtn = this.CreateButton("SpeedBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y + 200), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx1"), this.SpeedUpAction);
		this._turnendChkBtn = this.CreateButton("TurnChkBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y + 50), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_turnendcheckoff"), this.TurnEndChkAction);
		this._returnMenuBtn = this.CreateButton("ReturnMenuBtn", this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y - 100), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_backtomenu"), this.ReturnMenuBtnAction);

		this._background.transform.SetSiblingIndex(0);
	}

	public void ActiveMethod()
	{
		this.gameObject.SetActive(true);

		this._background.transform.localScale = new Vector3(750f, 1334f, 0f);

		_frame.transform.DOScale(new Vector3(750f, 1334f, 0), 0.25f).OnComplete(()=> {
			_resumeBtn.transform.DOScale(SIZE, 0.2f);
			_selectBtn.transform.DOScale(SIZE, 0.2f);
			_resetBtn.transform.DOScale(SIZE, 0.2f);
			_configButton.transform.DOScale(SIZE, 0.2f);
		});
	}

	void NotActiveMethod()
	{
		this._background.transform.localScale = Vector3.zero;

		_resetBtn.transform.DOScale(Vector3.zero, 0.2f);
		_selectBtn.transform.DOScale(Vector3.zero, 0.2f);
		_configButton.transform.DOScale(Vector3.zero, 0.2f);
		_resumeBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=> {
			_frame.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {
				this.gameObject.SetActive(false);
				GameManager.Instance.SaveConfigData();
			});
		});

		_speedupBtn.transform.DOScale(Vector3.zero, 0.2f);
		_turnendChkBtn.transform.DOScale(Vector3.zero, 0.2f);
		_returnMenuBtn.transform.DOScale(Vector3.zero, 0.2f);
	}
}

