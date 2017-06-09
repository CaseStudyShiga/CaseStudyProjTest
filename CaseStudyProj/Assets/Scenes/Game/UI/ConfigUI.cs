using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;

class ConfigUI : UIBase
{
	readonly Vector2 SIZE = new Vector3(450f, 150f, 0f);
	readonly Vector3 CONFIG_POS = new Vector3(0, 0, 0);

	GameObject _background;
	GameObject _frame;

	public bool IsMenu;

	Dictionary<string, string> _btnDic;
	List<System.Action> _btnAcList;
	Dictionary<string, GameObject> _btnObjDic;

	void Awake()
	{
		_btnDic = new Dictionary<string, string>()
		{
			{"SpeedBtn", "gameUI_v3_speedx1"},
			{"TurnChkBtn", "gameUI_v3_turnendcheckoff"},
			{"DeleteBtn", "gameUI_v3_data_delete"},
			{"ReturnMenuBtn", "gameUI_v3_backtomenu"},
			{"OkBtn", "turnendWindow_yes"},
			{"ResumeBtn", "turnendWindow_no"},

		};
		_btnAcList = new List<System.Action>();
		_btnAcList.Add(this.SpeedUpAction);
		_btnAcList.Add(this.TurnEndChkAction);
		_btnAcList.Add(this.DeleteBtnAction);
		_btnAcList.Add(this.ReturnMenuBtnAction);
		_btnAcList.Add(this.OkBtnAction);
		_btnAcList.Add(this.NoBtnAction);

		_btnObjDic = new Dictionary<string, GameObject>();
	}

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

		_btnDic.Select((data, idx) =>
		{
			GameObject obj = null;

			switch (data.Key)
			{
				case "OkBtn":
					obj = this.CreateButton(data.Key, this.transform, Vector2.one, new Vector3(0, -100), Resources.Load<Sprite>("Sprites/GUI/" + data.Value), () => { _btnAcList[idx](); });
					break;
				case "ResumeBtn":
					obj = this.CreateButton(data.Key, this.transform, Vector2.one, new Vector3(0, -250), Resources.Load<Sprite>("Sprites/GUI/" + data.Value), () => { _btnAcList[idx](); });
					break;
				default:
					obj = this.CreateButton(data.Key, this.transform, Vector2.one, new Vector3(0, CONFIG_POS.y + 200 - (150 * idx)), Resources.Load<Sprite>("Sprites/GUI/" + data.Value), () => { _btnAcList[idx](); });
					break;
			}

			_btnObjDic.Add(data.Key, obj);
			return data;
		}).ToList();

		this.SpeedUpIconChk();
		this.TurnEndIconChk();
	}

	void SpeedUpIconChk()
	{
		switch (GameManager.Instance.SpeedUpType)
		{
			case GameManager.SpeedUp.x1:
				this._btnObjDic["SpeedBtn"].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx1");
				break;
			case GameManager.SpeedUp.x2:
				this._btnObjDic["SpeedBtn"].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx2");
				break;
			case GameManager.SpeedUp.x3:
				this._btnObjDic["SpeedBtn"].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_speedx3");
				break;
		}
	}

	void TurnEndIconChk()
	{
		bool chk = GameManager.Instance.isTurnEndChk;
		Image image = this._btnObjDic["TurnChkBtn"].GetComponent<Image>();
		image.sprite = (chk) ? Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_turnendcheck") : Resources.Load<Sprite>("Sprites/GUI/gameUI_v3_turnendcheckoff");
	}


	// 設定画面へ
	public void ConfigAction()
	{
		this.SpeedUpIconChk();
		this.TurnEndIconChk();

		_btnObjDic.Select((data) =>
		{
			if (!(data.Key == "OkBtn" || data.Key == "ResumeBtn"))
			{
				data.Value.transform.DOScale(SIZE, 0.2f);
			}
			return data;
		});
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

			_btnObjDic.Select((data) =>
			{
				switch (data.Key)
				{
					case "ReturnMenuBtn":
						data.Value.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
							_frame.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
							});
						});
						break;
					default:
						if (!(data.Key == "OkBtn" || data.Key == "ResumeBtn"))
						{
							data.Value.transform.DOScale(Vector3.zero, 0.2f);
						}
						break;
				}

				return data;
			}).ToList();

			GameManager.Instance.SaveConfigData();
		}
	}

	void DeleteBtnAction()
	{
		_btnObjDic.Select((data, idx) =>
		{
			switch (data.Key)
			{
				case "OkBtn":
				case "ResumeBtn":
					data.Value.transform.DOScale(new Vector2(400, 150), 0.2f);
					break;
				default:
					data.Value.transform.DOScale(Vector3.zero, 0.2f);
					break;
			}
			return data;
		}).ToList();
		_frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/data_delete_window");
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

			_btnObjDic.Select((data, idx) =>
			{
				switch (data.Key)
				{
					case "ReturnMenuBtn":
						data.Value.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
							_frame.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
							});
						});
						break;
					default:
						data.Value.transform.DOScale(Vector3.zero, 0.2f);
						break;
				}
				return data;
			}).ToList();

			GameManager.Instance.SaveConfigData();
		}

	}

	void NoBtnAction()
	{
		_frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/configwindow");

		_btnObjDic.Select((data, idx) =>
		{
			switch (data.Key)
			{
				case "OkBtn":
				case "ResumeBtn":
					data.Value.transform.DOScale(Vector3.zero, 0.2f);
					break;
				default:
					data.Value.transform.DOScale(SIZE, 0.2f);
					break;
			}
			return data;
		}).ToList();
	}

	public void ActiveMethod()
	{
		if (IsMenu)
		{
			this._background.transform.localScale = Vector3.zero;
			_frame.transform.localScale = new Vector3(750f, 1334f, 0);

			_btnObjDic.Select((data, idx) =>
			{
				if (!(data.Key == "OkBtn" || data.Key == "ResumeBtn"))
				{
					data.Value.transform.DOScale(SIZE, 0.2f);
				}
				return data;
			}).ToList();
		}
		else
		{
			this._background.transform.localScale = new Vector3(750f, 1334f, 0f);

			_frame.transform.DOScale(new Vector3(750f, 1334f, 0), 0.25f).OnComplete(() => {

				_btnObjDic.Select((data, idx) =>
				{
					if (!(data.Key == "OkBtn" || data.Key == "ResumeBtn"))
					{
						data.Value.transform.DOScale(SIZE, 0.2f);
					}
					return data;
				}).ToList();
			});
		}
	}

	void NotActiveMethod()
	{
		_btnObjDic.Select((data, idx) =>
		{
			switch (data.Key)
			{
				case "ReturnMenuBtn":
					data.Value.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
						_frame.transform.localScale = new Vector3(0f, 0f, 0f);
					});
					break;
				default:
					if (!(data.Key == "OkBtn" || data.Key == "ResumeBtn"))
					{
						data.Value.transform.DOScale(Vector3.zero, 0.2f);
					}
					break;
			}
			return data;
		}).ToList();
		GameManager.Instance.SaveConfigData();
	}
}
