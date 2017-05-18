using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using DG.Tweening;

public class StageBase : MonoBehaviour
{
	// 定数
	const int PANEL_SIZE = 93;
	const float SPACE_COEFFICIENT = 1.0f; // パネルごとの間隔
	readonly Vector3 BASE_POS = new Vector3(-325.5f, 343.0f, 0); // パネルの左上　※開始地点
	readonly Color32 PANEL_COL = new Color32(255,255,255,0);

	// メンバ変数
	GameObject[,] _panelData;
	GameObject _background;
	Stack<Transform> _stackPlayerObj = new Stack<Transform>() { };
	Stack<Vector2> _stackPlayerPos = new Stack<Vector2>() { };

	// 外部読み取り用
	public GameObject[,] PanelData { get { return _panelData; } }
	public GameObject BackGround { get { return _background; } }

	void Start()
	{
	}

	void Update()
	{
	}

	public void CreateStageBase(int[,] stageData)
	{
		GameObject panels = new GameObject("Panels");
		GameObject players = new GameObject("Players");
		GameObject enemys = new GameObject("Enemys");

		panels.transform.SetParent(this.transform);
		panels.transform.localPosition = Vector3.zero;

		players.transform.SetParent(this.transform);
		players.transform.localPosition = Vector3.zero;

		enemys.transform.SetParent(this.transform);
		enemys.transform.localPosition = Vector3.zero;

		_background = CreateBackGround("BackGround", this.transform, new Vector2(750, 1334), Resources.Load<Sprite>("Sprites/GUI/gameUI_v2_background"));
		_background.transform.SetAsFirstSibling();

		_panelData = new GameObject[stageData.GetLength(0), stageData.GetLength(1)];

		for (int y = 0; y < stageData.GetLength(0); y++)
		{
			for (int x = 0; x < stageData.GetLength(1); x++)
			{
				if (stageData[y, x] != 0)
				{
					_panelData[y, x] = this.CreateChild("panel" + (x + ((stageData.GetLength(1)) * y)), panels, null, new Vector2(PANEL_SIZE, PANEL_SIZE));

					// パネルデータの設定
					Panel panelData = this.GetPanelData(x, y);
					panelData.X = x;
					panelData.Y = y;
					panelData.Type = stageData[y, x];
					panelData.Value = 0;
					panelData.DataReset();

					// コードの二次元配列に合わせるため Y軸反転
					_panelData[y, x].GetComponent<RectTransform>().localPosition = BASE_POS + new Vector3(x * (PANEL_SIZE * SPACE_COEFFICIENT), -y * (PANEL_SIZE * SPACE_COEFFICIENT), 0.0f);
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
		if (panelData.OnObj) return;

		// 現在の地点にあるマップ情報を取り出して、ウェイトを計算
		int down = 0;
		switch (panelData.Type)
		{
			case 0: // 進行不可
				down = -999;
				break;
			case 1: // 平地
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
				_stackPlayerPos.Push(new Vector2(status.X, status.Y));
				_stackPlayerObj.Push(child);

				this.GetPanelData(status.X, status.Y).DataReset();
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
		Color32 moveCol = PANEL_COL;
		moveCol.r -= 80;
		moveCol.a += 100;

		// Imageの色変更
		var image = this._panelData[y, x].GetComponent<Image>();
		DOTween.To(
			() => image.color,                  // 何を対象にするのか
			color => image.color = color,       // 値の更新
			moveCol,    // 最終的な値
			0.175f                              // アニメーション時間
		);
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
					var image = this._panelData[y, x].GetComponent<Image>();
					DOTween.To(
						() => image.color,                  // 何を対象にするのか
						color => image.color = color,       // 値の更新
						PANEL_COL,                        // 最終的な値
						0.175f                               // アニメーション時間
					);

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
		child.GetComponent<Image>().color = PANEL_COL;

		Panel panel = child.AddComponent<Panel>();
		child.AddComponent<Button>().onClick.AddListener(() =>
		{
			this.CharMove(panel);
		});
		return child;
	}

	public bool SearchBetween(int x, int y, int di) // center X, center Y, search dir
	{
		if (x < 0 || y < 0 || x >= this._panelData.GetLength(1) || y >= this._panelData.GetLength(0)
			|| this._panelData[y, x] == null)
			return false;

		bool result = false;
		int enemyCount = 0;

		int nextX = x;
		int nextY = y;
		int moveX = (int)GameManager.Instance.DirTable[di].x;
		int moveY = (int)GameManager.Instance.DirTable[di].y;

		List<StatusBase> enemyStatus = new List<StatusBase>();
		while (!result)
		{
			nextX += moveX;
			nextY += moveY;
			Panel panel = this.GetPanelData(nextX, nextY);

			if (panel == null || panel.OnObj == null)
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
			return true;
		}

		return false;
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
			playerStatus.Dir.Clear();

			for (int i = 0; i < 8; i++)
			{
				if (this.SearchBetween(playerStatus.X, playerStatus.Y, i))
				{
					playerStatus.Dir.Add(i);
				}
			}
		}
	}

	public void AllMovedOff()
	{
		foreach (Transform child in this.transform.Find("Enemys"))
		{
			var enemyStatus = child.GetComponent<StatusBase>();
			enemyStatus.MovedOff();
		}

		foreach (Transform child in this.transform.Find("Players"))
		{
			var playerStatus = child.GetComponent<StatusBase>();
			playerStatus.MovedOff();
		}
	}

	public void UndoPlayer()
	{
		if (_stackPlayerObj.Count > 0)
		{
			Transform obj = _stackPlayerObj.Pop();
			StatusBase status = obj.GetComponent<StatusBase>();
			Vector2 pos = _stackPlayerPos.Pop();

			this.GetPanelData(status.X, status.Y).DataReset();
			status.MovedOff();
			status.SetPos((int)pos.x, (int)pos.y);
			status.MovedOff();

			foreach (Transform child in this.transform.Find("Players"))
			{
				var playerStatus = child.GetComponent<StatusBase>();
				playerStatus.SelectOff();
			}

			this.ClearPossibleMovePanel();
			this.AllCheckBetween();
		}
	}

	public void ClearStackPlayer()
	{
		_stackPlayerObj.Clear();
		_stackPlayerPos.Clear();
	}

	public void AttackPlayers()
	{
		foreach (Transform child in this.transform.Find("Players"))
		{
			var playerStatus = child.GetComponent<StatusBase>();

			for (int d = 0; d < playerStatus.Dir.Count; d++)
			{
				for (int i = 0; i < playerStatus.Range; i++)
				{
					int enemyX = ((int)GameManager.Instance.DirTable[playerStatus.Dir[d]].x * (i + 1)) + playerStatus.X;
					int enemyY = ((int)GameManager.Instance.DirTable[playerStatus.Dir[d]].y * (i + 1)) + playerStatus.Y;

					var panel = this.GetPanelData(enemyX, enemyY);
					var enemy = (panel) ? panel.OnObj : null;

					if (enemy)
					{
						var enemyStatus = enemy.GetComponent<StatusBase>();

						if (enemyStatus.IsPlayer == false)
						{
							enemyStatus.Hp -= playerStatus.Attack;

							if (enemyStatus.Hp <= 0)
							{
								panel.DataReset();
							}
						}
					}
				}
			}
		}
	}

	GameObject CreateBackGround(string name, Transform parent, Vector2 size, Sprite sp = null)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = Vector3.zero;
		child.AddComponent<Image>().sprite = sp;

		return child;
	}
}