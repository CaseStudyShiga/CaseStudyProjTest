using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class MenuUI : UIBase
{
	const int ALLOW_TIME = 60 * 3;
	readonly Vector2 SIZE = new Vector3(450f, 150f, 0f);
	readonly Vector3 POS = new Vector3(0,-50,0);

	GameObject _background;
	GameObject _frame;
	GameObject _resumeBtn;
	GameObject _selectBtn;
	GameObject _resetBtn;
	GameObject _configButton;

	private bool _isPushReloadButton = false;	// ボタン押下許可フラグ
	private int _time;							// 前回ボタンが押された時点と現在時間との差分を格納

	void Start()
	{
		this.InitField();
		this.NotActiveMethod();
	}

	void Update()
	{
		// 3秒後にボタン押下を許可
		if (_isPushReloadButton)
		{
			this._time += 1;

			if (_time >= ALLOW_TIME)
			{
				_isPushReloadButton = false;
			}
		}
	}

	// ステージリセット
	void ResetAction()
	{
		if (this._isPushReloadButton) return;
		this._isPushReloadButton = true;

		Debug.Log("Reset");
		var stage = this.transform.parent.parent.Find("Stage").GetComponent<Stage>();

		stage.Reset();
		GameManager.Instance.Reset();

		this._time = 0;
	}

	// ステージ選択へ
	void SelectAction()
	{
	}

	// 設定画面へ
	void ConfigAction()
	{
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
				this._time = ALLOW_TIME;
				this._isPushReloadButton = false;
				this.gameObject.SetActive(false);
			});
		});
	}
}

