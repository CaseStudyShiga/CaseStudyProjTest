using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

	GameObject _configUI;

	void Start()
	{
		this.InitField();
		this.NotActiveMethod();
	}

	void Update()
	{
	}

	// ステージリセット
	void ResetAction()
	{
		var stage = this.transform.parent.parent.Find("Stage").GetComponent<Stage>();
		stage.Reset();
		this.transform.parent.Find("TopUI").GetComponent<TopUI>().Reset();
		GameManager.Instance.Reset();

		this.NotActiveMethod();
	}

	// ステージ選択へ
	void SelectAction()
	{
		Fader.instance.BlackOut();
		StartCoroutine(DelayMethod(1.0f, () => {
			SceneManager.LoadScene("Select");
		}));
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._background = this.CreateChild("BackGround", this.transform, Vector2.one, Vector3.zero);
		this._background.GetComponent<Image>().color = new Color32(0,0,0,100);
		this._frame = this.CreateChild("Frame", this.transform, Vector2.one, Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_menuwindow"));

		this._configUI = this.CreateChild("ConfigUI", this.transform, Vector2.one, Vector3.zero);
		var config = this._configUI.AddComponent<ConfigUI>();
		config.IsMenu = true;

		this._resetBtn = this.CreateButton("ResetBtn", this.transform, Vector2.one, new Vector3(0, POS.y + 250), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_retry"), this.ResetAction);
		this._selectBtn = this.CreateButton("SelectBtn", this.transform, Vector2.one, new Vector3(0, POS.y + 100), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_stageselect"), this.SelectAction);
		this._configButton = this.CreateButton("ConfigBtn", this.transform, Vector2.one, new Vector3(0, POS.y - 50), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_config"), config.ActiveMethod);
		this._resumeBtn = this.CreateButton("ResumeBtn", this.transform, Vector2.one, new Vector3(0,POS.y - 200), Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_resume"), this.NotActiveMethod);

		this._background.transform.SetSiblingIndex(0);
		this._configUI.transform.SetAsLastSibling();
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
	}

	IEnumerator DelayMethod(float waitTime, System.Action ac)
	{
		yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
		ac();
	}
}

