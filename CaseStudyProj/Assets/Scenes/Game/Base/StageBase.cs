using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class StageBase : MonoBehaviour
{
	const float SPACE_COEFFICIENT = 1.2f; // パネルごとの間隔

	GameObject[,] _panelData;
	public GameObject[,] PanelData { get { return _panelData; } }

	void Start ()
	{
	}

	void Update ()
	{
	}

	public void CreateStageBase(int[,] stageData, int panelSize)
	{
		GameObject panels = new GameObject("Panels");
		GameObject players = new GameObject("Players");
		GameObject enemys = new GameObject("Enemys");
		Vector3 BasePos = new Vector3(-300,350,0);

		panels.transform.SetParent(this.transform);
		panels.transform.localPosition = Vector3.zero;
		players.transform.SetParent(this.transform);
		players.transform.localPosition = Vector3.zero;
		enemys.transform.SetParent(this.transform);
		enemys.transform.localPosition = Vector3.zero;

		_panelData = new GameObject[stageData.GetLength(0), stageData.GetLength(1)];

		for (int y = 0; y < stageData.GetLength(0); y++)
		{
			for (int x = 0; x < stageData.GetLength(1); x++)
			{
				if (stageData[y, x] != 0)
				{
					_panelData[y, x] = this.CreateChild("panel" + (x + ((stageData.GetLength(1)) * y)), panels, null, new Vector2(panelSize, panelSize));

					// パネルデータの設定
					Panel panelData = this.GetPanelData(x, y);
					panelData.X = x;
					panelData.Y = y;
					panelData.Type = stageData[y, x];
					panelData.Value = 0;
					panelData.IsCheck = false;
					panelData.IsOnObj = false;

					// コードの二次元配列に合わせるため Y軸反転
					_panelData[y, x].GetComponent<RectTransform>().localPosition = BasePos + new Vector3(x * (panelSize * SPACE_COEFFICIENT), -y * (panelSize * SPACE_COEFFICIENT), 0.0f);
				}
			}
		}
	}

	public void Search(int x, int y, int mv, int di)
	{
		Panel panelData = this.GetPanelData(x, y);

		if (panelData == null)
			return;

		// パネルの上にすでに何か存在していたら終了
		if (panelData.IsOnObj) return;

		// 現在の地点にあるマップ情報を取り出して、ウェイトを計算
		int down = 0;
		switch (panelData.Type)
		{
			case 0: // 進行不可
				down = -999;
			break;
			case 1:	// 平地
				down = -1;
			break;
		}

		// 歩数がマイナスになった地点へは進めないので中断
		if (mv + down < 0)
		{
			return;
		}

		// マーク
		panelData.IsCheck = true;
		this.SetPossibleMovePanel(x, y);

		if (di != 3) this.Search(x, y - 1, mv + down, 1);
		if (di != 4) this.Search(x + 1, y, mv + down, 2);
		if (di != 1) this.Search(x, y + 1, mv + down, 3);
		if (di != 2) this.Search(x - 1, y, mv + down, 4);
	}

	void CharMove(Panel panel)
	{
		foreach (Transform child in this.transform.Find("Players"))
		{
			StatusBase status = child.GetComponent<StatusBase>();
			if (status.IsSelect && panel.IsCheck)
			{
				this.GetPanelData(status.X, status.Y).IsOnObj = false;
				this.GetPanelData(status.X, status.Y).OnObj = null;
				status.SetPos(panel.X, panel.Y);
				status.SelectOff();
				this.ClearPossibleMovePanel();

				//for (int i = 1; i <= 8; i++)
				//{
				//	this.SearchBetween(panel.X, panel.Y, i);
				//}

				// ※今だけ
				this.AllCheckBetween();
			}
		}
	}

	public void SetPossibleMovePanel(int x, int y)
	{
		_panelData[y, x].GetComponent<Image>().color = new Color32(175, 255, 255, 255);
	}

	public void ClearPossibleMovePanel()
	{
		for (int y = 0; y < _panelData.GetLength(0); y++)
		{
			for (int x = 0; x < _panelData.GetLength(1); x++)
			{
				if (_panelData[y, x])
				{
					// パネルデータの設定
					Panel panel = this.GetPanelData(x, y);
					panel.IsCheck = false;
					_panelData[y, x].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
				}
			}
		}
	}

	public Panel GetPanelData(int x, int y)
	{
		// 例外処理 ( 範囲外 || 存在しない )
		if (x < 0 || y < 0 || x >= _panelData.GetLength(1) || y >= _panelData.GetLength(0)
			|| _panelData[y, x] == null)
		{
			return null;
		}

		return _panelData[y, x].GetComponent<Panel>();
	}

	public Vector3 GetPanelLocalPosition(int x, int y)
	{
		return _panelData[y, x].transform.localPosition;
	}

	GameObject CreateChild(string name, GameObject parent, Sprite sp, Vector2 size)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.AddComponent<Image>().sprite = sp;

		Panel panel = child.AddComponent<Panel>();
		child.AddComponent<Button>().onClick.AddListener(() => {
			this.CharMove(panel);
		});
		return child;
	}

	public void SearchBetween(int x, int y, int di) // center X, center Y, search dir
	{
		if (x < 0 || y < 0 || x >= this._panelData.GetLength(1) || y >= this._panelData.GetLength(0)
			|| this._panelData[y, x] == null)
			return;

		int moveX = 0;
		int moveY = 0;

		switch (di)
		{
			case 1:
				moveX = 0;
				moveY = -1;
				break;
			case 2:
				moveX = 1;
				moveY = -1;
				break;
			case 3:
				moveX = 1;
				moveY = 0;
				break;
			case 4:
				moveX = 1;
				moveY = 1;
				break;
			case 5:
				moveX = 0;
				moveY = 1;
				break;
			case 6:
				moveX = -1;
				moveY = 1;
				break;
			case 7:
				moveX = -1;
				moveY = 0;
				break;
			case 8:
				moveX = -1;
				moveY = -1;
				break;
			default:
				return;
		}

		bool result = false;
		int enemyCount = 0;

		int nextX = x;
		int nextY = y;

		List<StatusBase> enemyStatus = new List<StatusBase>();
		while (!result)
		{
			nextX += moveX;
			nextY += moveY;
			Panel panel = this.GetPanelData(nextX, nextY);

			if (panel == null || panel.IsOnObj == false)
			{
				break;
			}

			if (panel.OnObj.GetComponent<StatusBase>().IsPlayer == false)
			{
				enemyStatus.Add(panel.OnObj.GetComponent<StatusBase>());
				enemyCount++;
			}
			else
			{
				result = true;
			}
		}

		if (result)
		{
			for (int i = 0; i < enemyStatus.Count; i++)
			{
				enemyStatus[i].BetweenOn();
			}
			Debug.Log(enemyCount);
		}
	}

	public void AllCheckBetween()
	{
		foreach (Transform child in this.transform.Find("Enemys"))
		{
			StatusBase enemyStatus = child.GetComponent<StatusBase>();

			if (enemyStatus.IsBetween)
			{
				enemyStatus.BetweenOff();
			}
		}

		foreach (Transform child in this.transform.Find("Players"))
		{
			StatusBase playerStatus = child.GetComponent<StatusBase>();

			for (int i = 1; i <= 8; i++)
			{
				this.SearchBetween(playerStatus.X, playerStatus.Y, i);
			}
		}
	}
}
