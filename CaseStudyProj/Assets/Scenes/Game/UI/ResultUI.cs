using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._frame = this.CreateChild("Frame", this.transform, new Vector2(750f, 1334f), Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/result_frame"));
		this._nextBtn = this.CreateButton("NextBtn", this.transform, SIZE, new Vector3(0, POS.y + 250), Resources.Load<Sprite>("Sprites/GUI/result_nextstage"), this.NextBtnAction);
		this._selectBtn = this.CreateButton("SelectBtn", this.transform, SIZE, new Vector3(0, POS.y + 100), Resources.Load<Sprite>("Sprites/GUI/result_stageselect"), this.SelectBtnAction);
		this._retryBtn = this.CreateButton("RetryBtn", this.transform, SIZE, new Vector3(0, POS.y - 50), Resources.Load<Sprite>("Sprites/GUI/result_retry"), this.RetryBtnAction);

		this._stageIcon = this.CreateButton("stage" + CSVDataReader.Instance.StageID.ToString(), this.transform, new Vector2(200, 200), new Vector3(-265, 400), Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_0"), () => { });
		GameObject txt = this.CreateText("Text", CSVDataReader.Instance.StageID.ToString(), this._stageIcon.transform, new Vector3(0, 9), 60, false);

		this._CompleteTxt = this.CreateText("Text", "作戦成功", this.transform, new Vector3(0, 385), 60, false);
		this._CompleteSubTxt = this.CreateText("SubText", "WIN", this.transform, new Vector3(0, 385 + 45), 30, false);
		this._CompleteTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);

		Vector3 rPos = new Vector3(40f,220f);
		this._mission[0] = this.CreateChild("Mission0", this.transform, new Vector2(90,90), new Vector3(rPos.x - 300f, 0 + rPos.y + 2), Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._mission[1] = this.CreateChild("Mission1", this.transform, new Vector2(90,90), new Vector3(rPos.x - 300f, -75 + rPos.y + 2),Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._mission[2] = this.CreateChild("Mission2", this.transform, new Vector2(90,90), new Vector3(rPos.x - 300f, -150 + rPos.y + 2),Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));

		this._missionTxt[0] = this.CreateText("MissionText0", "ステージをクリアした", this.transform, new Vector3(rPos.x, 0 + rPos.y), 40, false);
		this._missionTxt[1] = this.CreateText("MissionText1", "全員生存した", this.transform, new Vector3(rPos.x, -75 + rPos.y), 40, false);
		this._missionTxt[2] = this.CreateText("MissionText2", "5ターン以内にクリアした", this.transform, new Vector3(rPos.x, -150 + rPos.y), 40, false);

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
		});
	}

	IEnumerator DelayMethod(float waitTime, System.Action ac)
	{
		yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
		ac();
	}
}

