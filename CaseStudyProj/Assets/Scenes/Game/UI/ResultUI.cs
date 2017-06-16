using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using DG.Tweening;

class ResultUI : UIBase
{
	readonly Vector2 SIZE = new Vector3(550f, 150f, 0f);
	readonly Vector3 POS = new Vector3(0, -370, 0);

	GameObject _frame;
	GameObject _nextBtn;
	GameObject _retryBtn;
	GameObject _selectBtn;
	GameObject _stageIcon;
	GameObject _CompleteTxt;
	GameObject _CompleteSubTxt;
	GameObject[] _missionTxt = new GameObject[3];
	GameObject[] _mission = new GameObject[3];

	void Start()
	{
		this.InitField();
		//this.ActiveMethod();
	}

	void Update()
	{
	}

	void CheckMission()
	{
		var stageIcon = this._stageIcon.GetComponent<Image>();

		int missionCompleteNum = 0;
		for (int i = 0; i < 3; i++)
		{
			var icon = this._mission[i].GetComponent<Image>();
			var txt = this._missionTxt[i].GetComponent<Text>();
			if (GameManager.Instance.isMission[i])
			{
				icon.sprite = Resources.Load<Sprite>("Sprites/GUI/result_normaclear");
				txt.color = Color.white;
				missionCompleteNum++;
			}
			else
			{
				icon.sprite = Resources.Load<Sprite>("Sprites/GUI/result_normafail");
				txt.color = new Color32(100,100,100,255);
			}
		}

		stageIcon.sprite = Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_" + missionCompleteNum.ToString());

		SaveData.Data data;
		data.AreaID = CSVDataReader.Instance.AreaID;
		data.StageID = CSVDataReader.Instance.StageID;
		data.Name = "stage" + data.StageID.ToString();
		data.IsStar = new bool[3];

		for (int i = 0; i < 3; i++)
		{
			data.IsStar[i] = GameManager.Instance.isMission[i];
		}

		var save = SaveManager.Instance.SaveData.data.Where(d => d.AreaID == data.AreaID && d.StageID == data.StageID).ToList();
		if (save.Count <= 0)
		{
			SaveManager.Instance.SaveData.data.Add(data);
		}
		else
		{
			save.ForEach(d =>
			{
				for (int i = 0; i < 3; i++)
				{ 
					if (d.IsStar[i] == false)
					{
						d.IsStar[i] = data.IsStar[i];
					}
				}
			});
		}

		SaveManager.Instance.Save();
	}

	void NextBtnAction()
	{
	}

	void SelectBtnAction()
	{
		Fader.instance.BlackOut();
		StartCoroutine(DelayMethod(1.0f, () => {
			SceneManager.LoadScene("Select");
		}));
	}

	void RetryBtnAction()
	{
		var stage = this.transform.parent.parent.Find("Stage").GetComponent<Stage>();
		stage.Reset();
		this.transform.parent.Find("TopUI").GetComponent<TopUI>().Reset();
		GameManager.Instance.Reset();

		this.NotActiveMethod();
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._frame = this.CreateChild("Frame", this.transform, new Vector2(750f, 1334f), Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/result_frame"));
		this._nextBtn = this.CreateButton("NextBtn", this.transform, SIZE, new Vector3(0, POS.y + 250), Resources.Load<Sprite>("Sprites/GUI/result_nextstage"), this.NextBtnAction);
		this._selectBtn = this.CreateButton("SelectBtn", this.transform, SIZE, new Vector3(0, POS.y + 100), Resources.Load<Sprite>("Sprites/GUI/result_stageselect"), this.SelectBtnAction);
		this._retryBtn = this.CreateButton("RetryBtn", this.transform, SIZE, new Vector3(0, POS.y - 50), Resources.Load<Sprite>("Sprites/GUI/result_retry"), this.RetryBtnAction);

		this._stageIcon = this.CreateButton("stage" + CSVDataReader.Instance.StageID.ToString(), this.transform, new Vector2(200, 200), new Vector3(-265, 400), Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_0"), () => { });
		GameObject txt = this.CreateText("Text", (CSVDataReader.Instance.StageID + 1).ToString(), this._stageIcon.transform, new Vector3(0, 9), 60, false);

		this._CompleteTxt = this.CreateText("Text", "作戦成功", this.transform, new Vector3(0, 385), 60, false);
		this._CompleteSubTxt = this.CreateText("SubText", "WIN", this.transform, new Vector3(0, 385 + 45), 30, false);
		this._CompleteTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);

		Vector3 rPos = new Vector3(40f,220f);
		this._mission[0] = this.CreateChild("Mission0", this.transform, new Vector2(90,90), new Vector3(rPos.x - 300f, 0 + rPos.y + 2), Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._mission[1] = this.CreateChild("Mission1", this.transform, new Vector2(90,90), new Vector3(rPos.x - 300f, -75 + rPos.y + 2),Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._mission[2] = this.CreateChild("Mission2", this.transform, new Vector2(90,90), new Vector3(rPos.x - 300f, -150 + rPos.y + 2),Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._missionTxt[0] = this.CreateText("MissionText0", "ステージをクリアした", this.transform, new Vector3(rPos.x, 0 + rPos.y), 40, false);
		this._missionTxt[1] = this.CreateText("MissionText1", "全員生存した", this.transform, new Vector3(rPos.x, -75 + rPos.y), 40, false);
		this._missionTxt[2] = this.CreateText("MissionText2", CSVDataReader.Instance.MinTotalTurn.ToString() + "ターン以内にクリアした", this.transform, new Vector3(rPos.x, -150 + rPos.y), 40, false);

		for (int i = 0; i < 3; i++)
		{
			this._missionTxt[i].GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);
			this._missionTxt[i].GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
		}

		this._frame.transform.localScale = Vector3.zero;
		this._nextBtn.transform.localScale = Vector3.zero;
		this._selectBtn.transform.localScale = Vector3.zero;
		this._retryBtn.transform.localScale = Vector3.zero;
		this._CompleteTxt.transform.localScale = Vector3.zero;
		this._CompleteSubTxt.transform.localScale = Vector3.zero;
		this._stageIcon.transform.localScale = Vector3.zero;
		for (int i = 0; i < 3; i++)
		{
			this._mission[i].transform.localScale = Vector3.zero;
			this._missionTxt[i].transform.localScale = Vector3.zero;
		}

		this._frame.transform.SetSiblingIndex(0);
	}

	public void ActiveMethod()
	{
		this._frame.transform.DOScale(Vector3.one, 0.25f).OnComplete(() => {
			this._nextBtn.transform.DOScale(Vector3.one, 0.2f);
			this._selectBtn.transform.DOScale(Vector3.one, 0.2f);
			this._retryBtn.transform.DOScale(Vector3.one, 0.2f);
			this._CompleteTxt.transform.DOScale(Vector3.one, 0.2f);
			this._CompleteSubTxt.transform.DOScale(Vector3.one, 0.2f);
			this._stageIcon.transform.DOScale(Vector3.one, 0.2f);
			for (int i = 0; i < 3; i++)
			{
				this._mission[i].transform.DOScale(Vector3.one, 0.2f);
				this._missionTxt[i].transform.DOScale(Vector3.one, 0.2f);
			}

			this.CheckMission();
		});
	}

	void NotActiveMethod()
	{
		_CompleteTxt.transform.DOScale(Vector3.zero, 0.2f);
		_CompleteSubTxt.transform.DOScale(Vector3.zero, 0.2f);
		_stageIcon.transform.DOScale(Vector3.zero, 0.2f);
		for (int i = 0; i < 3; i++)
		{
			this._mission[i].transform.DOScale(Vector3.zero, 0.2f);
			this._missionTxt[i].transform.DOScale(Vector3.zero, 0.2f);
		}

		_nextBtn.transform.DOScale(Vector3.zero, 0.2f);
		_selectBtn.transform.DOScale(Vector3.zero, 0.2f);
		_retryBtn.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {
			_frame.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {
			});
		});
	}

	IEnumerator DelayMethod(float waitTime, System.Action ac)
	{
		yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
		ac();
	}
}

