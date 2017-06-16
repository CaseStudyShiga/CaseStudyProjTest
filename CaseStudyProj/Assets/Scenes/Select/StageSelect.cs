using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;

class StageSelect : UIBase
{
	Area _area;
	GameObject _background;
	GameObject _areaName;
	GameObject _stages;
	GameObject _confimationBack;
	GameObject _confirmationPanel;
	GameObject _yesBtn;
	GameObject _noBtn;

	void Awake()
	{
		this.InitField();
	}

	void Start()
	{
	}

	void Update()
	{
	}

	//-----------------------------------------------------
	//画面選択時の際に出現させる確認パネルを表示
	//-----------------------------------------------------
	void onConfirmationPanel()
	{
		this._confimationBack.SetActive(true);
		this._confirmationPanel.SetActive(true);
		this._confirmationPanel.transform.SetAsLastSibling();
		this._confirmationPanel.transform.localScale = new Vector3(0, 0, 0);
		this._confirmationPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).OnComplete(()=> {
		});
	}

	void YesAction()
	{
		Fader.instance.BlackOut();
		StartCoroutine(DelayMethod(1.2f, () => {
			CSVDataReader.Instance.Load(this._area.ID, SelectManager.Instance.StageID);
		}));
	}

	//-----------------------------------------------------
	//画面選択時の際に出現させる確認パネルを非表示に変更
	//-----------------------------------------------------
	void NoAction()
	{
		this._confimationBack.SetActive(false);
		this._confirmationPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 0.25f).OnComplete(() => {
			this._confirmationPanel.SetActive(false);
		});
	}

	void InitField()
	{
		this._background = this.CreateChild("BackGround", this.transform, new Vector2(750, 1334), Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_frame"));
		this._areaName = this.CreateText("AreaName", "test", this.transform, new Vector3(0, 400), 60, false);
		this._areaName.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 100);
		this._stages = new GameObject("Stages");
		this._stages.transform.SetParent(this.transform);
		this._stages.transform.localPosition = Vector3.zero;

		this._confimationBack = this.CreateChild("ConfirmationPanel", this.transform.parent.transform, new Vector2(750, 1334), Vector3.zero);
		this._confimationBack.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
		this._confimationBack.SetActive(false);
		this._confirmationPanel = this.CreateChild("ConfirmationPanel", this.transform.parent.transform, new Vector2(750, 1334), Vector3.zero, Resources.Load<Sprite>("Sprites/GUI/stage_select_window"));
		this._confirmationPanel.transform.localScale = new Vector3(0, 0, 0);
		this._confirmationPanel.AddComponent<Confirmation>();

		Vector3 rPos = new Vector3(0, -100);
		this._yesBtn = this.CreateButton("yesButton", this._confirmationPanel.transform, new Vector2(350, 150), new Vector3(0,-100) + rPos, Resources.Load<Sprite>("Sprites/GUI/turnendWindow_yes"), this.YesAction);
		this._noBtn = this.CreateButton("noButton", this._confirmationPanel.transform, new Vector2(350, 150), new Vector3(0, -250) + rPos, Resources.Load<Sprite>("Sprites/GUI/turnendWindow_no"), this.NoAction);

		this._background.transform.SetAsFirstSibling();
	}

	public void SetArea(Area area)
	{
		_area = area;
		this._areaName.GetComponent<Text>().text = _area.Name;

		int cnt = this._area.StageNumMax;

		if (cnt == 0) return;

		const int RowMax = 4;
		double colum = (double)cnt / (double)RowMax;
		int maxColumn = (int)System.Math.Ceiling(colum);
		if (maxColumn <= 0)	maxColumn = 1;
		int r = (int)cnt % (int)RowMax;
		int rest = (r > 0) ? RowMax - (int)cnt % (int)RowMax : 0;
		int notCharStartIdx = maxColumn * RowMax - rest;    // 要らないキャラの開始添字

		int count = 0;
		for (int y = 0; y < maxColumn; y++)
		{
			for (int x = 0; x < RowMax; x++)
			{
				if (notCharStartIdx <= count)
				{
					continue;
				}

				GameObject obj = this.CreateButton("stage" + count.ToString(), this._stages.transform, new Vector2(150, 150), new Vector3(-270 + (x * 180), 200 - (y * 200)), Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_0"), () => { });
				GameObject txt = this.CreateText("Text", (count + 1).ToString(), obj.transform, new Vector3(0, 9), 45, false);

				int starCnt = 0;
				var saveData = SaveManager.Instance.SaveData.data.Where(d => d.AreaID == this._area.ID).ToList();
				for (int i = 0; i < 3; i++)
				{
					if (saveData[count].IsStar[i])
					{
						starCnt++;
					}
				}
				obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GUI/stageSelectUI_stageButton_" + starCnt.ToString());

				var btnSystem = obj.AddComponent<ButtonSystem>();
				btnSystem.StageID = count;
				obj.GetComponent<Button>().onClick.AddListener(() => {
					this._confirmationPanel.GetComponent<Confirmation>().CheckMission(area, obj.GetComponent<ButtonSystem>().StageID);
					this.onConfirmationPanel();
				});
				count++;
			}
		}
	}

	public void CreateStages()
	{
		this.SetArea(this._area);
	}

	public void DeleteStages()
	{
		Transform children = _stages.GetComponentInChildren<Transform>();

		if (children.childCount == 0)
			return;

		foreach (Transform obj in children)
		{
			Destroy(obj.gameObject);
		}
	}

	//-----------------------------------------------------
	//フェード使う際のScene遷移関数
	//-----------------------------------------------------
	IEnumerator DelayMethod(float waitTime, System.Action ac)
	{
		yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
		ac();
		SceneManager.LoadScene("Game");                 // シーン切り替え
	}
}

