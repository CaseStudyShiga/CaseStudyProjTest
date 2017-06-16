using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

class Confirmation : UIBase
{
	readonly Vector2 SIZE = new Vector3(550f, 150f, 0f);
	readonly Vector3 POS = new Vector3(0, -370, 0);

	GameObject _stageIcon;
	GameObject _stageTxt;
	GameObject[] _missionTxt = new GameObject[3];
	GameObject[] _mission = new GameObject[3];

	void Start()
	{
		this.InitField();
		this.ActiveMethod();
	}

	void Update()
	{
	}

	public void CheckMission(Area area, int stageNum)
	{
		var stageIcon = this._stageIcon.GetComponent<Image>();
		this._stageTxt.GetComponent<Text>().text = (stageNum + 1).ToString();
		var saveData = SaveManager.Instance.SaveData.data.Where(d => d.AreaID == area.ID).ToList();

		int missionCompleteNum = 0;
		for (int i = 0; i < 3; i++)
		{
			var icon = this._mission[i].GetComponent<Image>();
			var txt = this._missionTxt[i].GetComponent<Text>();
			if (saveData[stageNum].IsStar[i])
			{
				icon.sprite = Resources.Load<Sprite>("Sprites/GUI/result_normaclear");
				txt.color = Color.white;
				missionCompleteNum++;
			}
			else
			{
				icon.sprite = Resources.Load<Sprite>("Sprites/GUI/result_normafail");
				txt.color = new Color32(100, 100, 100, 255);
			}
		}

		stageIcon.sprite = Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_" + missionCompleteNum.ToString());

		CSVDataReader.Instance.Load(area.ID, stageNum);
		this._missionTxt[2].GetComponent<Text>().text = CSVDataReader.Instance.MinTotalTurn.ToString() + "ターン以内にクリアした";
	}

	void InitField()
	{
		this.transform.localPosition = Vector3.zero;

		this._stageIcon = this.CreateChild("stage" + CSVDataReader.Instance.StageID.ToString(), this.transform, new Vector2(200, 200), new Vector3(-265, 400), Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_0"));
		this._stageTxt = this.CreateText("Text", CSVDataReader.Instance.StageID.ToString(), this._stageIcon.transform, new Vector3(0, 9), 60, false);

		Vector3 rPos = new Vector3(40f, 220f);
		this._mission[0] = this.CreateChild("Mission0", this.transform, new Vector2(90, 90), new Vector3(rPos.x - 300f, 0 + rPos.y + 2), Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._mission[1] = this.CreateChild("Mission1", this.transform, new Vector2(90, 90), new Vector3(rPos.x - 300f, -75 + rPos.y + 2), Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._mission[2] = this.CreateChild("Mission2", this.transform, new Vector2(90, 90), new Vector3(rPos.x - 300f, -150 + rPos.y + 2), Resources.Load<Sprite>("Sprites/GUI/result_normaclear"));
		this._missionTxt[0] = this.CreateText("MissionText0", "ステージをクリアした", this.transform, new Vector3(rPos.x, 0 + rPos.y), 40, false);
		this._missionTxt[1] = this.CreateText("MissionText1", "全員生存した", this.transform, new Vector3(rPos.x, -75 + rPos.y), 40, false);
		this._missionTxt[2] = this.CreateText("MissionText2", CSVDataReader.Instance.MinTotalTurn.ToString() + "ターン以内にクリアした", this.transform, new Vector3(rPos.x, -150 + rPos.y), 40, false);

		for (int i = 0; i < 3; i++)
		{
			this._missionTxt[i].GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);
			this._missionTxt[i].GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
		}

		this._stageIcon.transform.localScale = Vector3.zero;
		for (int i = 0; i < 3; i++)
		{
			this._mission[i].transform.localScale = Vector3.zero;
			this._missionTxt[i].transform.localScale = Vector3.zero;
		}
	}

	public void ActiveMethod()
	{
		this._stageIcon.transform.DOScale(Vector3.one, 0.2f);
		for (int i = 0; i < 3; i++)
		{
			this._mission[i].transform.DOScale(Vector3.one, 0.2f);
			this._missionTxt[i].transform.DOScale(Vector3.one, 0.2f);
		}
	}

	void NotActiveMethod()
	{
		_stageIcon.transform.DOScale(Vector3.zero, 0.2f);
		for (int i = 0; i < 3; i++)
		{
			this._mission[i].transform.DOScale(Vector3.zero, 0.2f);
			this._missionTxt[i].transform.DOScale(Vector3.zero, 0.2f);
		}
	}
}

