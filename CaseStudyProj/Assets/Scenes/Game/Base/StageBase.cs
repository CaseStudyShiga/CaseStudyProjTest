using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
	List<Transform> _targetList = new List<Transform>() { };

	// 外部読み取り用
	public GameObject[,] PanelData { get { return _panelData; } }
	public GameObject BackGround { get { return _background; } }

	void Start()
	{
	}

	void Update()
	{
	}

	// int型の2次元配列を基準にパネルを生成
	protected void CreateStageBase(int[,] stageData)
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
				if (stageData[y, x] != -1)
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

	public void Search(int x, int y, int mv, int di, int ra = 0)
	{
		if (GameManager.Instance.isEnemyTurn == false)
		{
			Panel panelData = this.GetPanelData(x, y);

			if (panelData == null)
				return;

			if (panelData.OnObj)
				return;

			// 現在の地点にあるマップ情報を取り出して、ウェイトを計算
			int down = this.WeightCheck(panelData);

			// 歩数がマイナスになった地点へは進めないので中断
			if (mv + down < 0)
			{
				if (ra > 0)
				{
					if (di != 3) this.SearchPossibleRange(x, y, ra, 1);
					if (di != 4) this.SearchPossibleRange(x, y, ra, 2);
					if (di != 1) this.SearchPossibleRange(x, y, ra, 3);
					if (di != 2) this.SearchPossibleRange(x, y, ra, 4);
				}

				return;
			}

			// マーク
			panelData.IsCheck = true;
			this.SetPossibleMovePanel(x, y);

			if (di != 3) this.Search(x, y - 1, mv + down, 1, ra);
			if (di != 4) this.Search(x + 1, y, mv + down, 2, ra);
			if (di != 1) this.Search(x, y + 1, mv + down, 3, ra);
			if (di != 2) this.Search(x - 1, y, mv + down, 4, ra);
		}
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

		if (result && enemyCount > 0)
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
							enemyStatus.Damage += playerStatus.Attack;
						}
					}
				}
			}
		}
	}

	public void DamageEnemy()
	{
		foreach (Transform child in this.transform.Find("Enemys"))
		{
			if (child)
			{
				var enemyStatus = child.GetComponent<StatusBase>();

				if (enemyStatus.Damage > 0)
				{
					DamageUI.Instance.SetDamage(enemyStatus.Damage, this.GetPanelLocalPosition(enemyStatus.X, enemyStatus.Y));
					enemyStatus.Hp -= enemyStatus.Damage;

					if (enemyStatus.Hp <= 0)
					{
						this.GetPanelData(enemyStatus.X, enemyStatus.Y).DataReset();
					}
				}

				enemyStatus.Damage = 0;
			}
		}
	}

	public IEnumerator EnemysTurn()
	{
		GameManager.Instance.isEnemyTurn = true;

		float speed = 0.5f;
		switch (GameManager.Instance.SpeedUpType)
		{
			case GameManager.SpeedUp.x1:
				speed = 0.5f;
				break;
			case GameManager.SpeedUp.x2:
				speed = 0.25f;
				break;
			case GameManager.SpeedUp.x3:
				speed = 0.125f;
				break;
		}

		// 全敵キャラ移動
		yield return StartCoroutine(AllEnemyMove(speed));
		
		// 全敵キャラ攻撃
		yield return StartCoroutine(AllEnemyAttack(speed));

		GameManager.Instance.isEnemyTurn = false;
	}

	IEnumerator AllEnemyMove(float waitTime)
	{
		foreach (Transform child in this.transform.Find("Enemys"))
		{
			yield return new WaitForSeconds(waitTime);

			if (child)
			{
				var status = child.GetComponent<StatusBase>();

				int oldX = status.X;
				int oldY = status.Y;

				this.EnemyMove(child, status.X, status.Y - 1, status.Move, 1);
				this.EnemyMove(child, status.X + 1, status.Y, status.Move, 2);
				this.EnemyMove(child, status.X, status.Y + 1, status.Move, 3);
				this.EnemyMove(child, status.X - 1, status.Y, status.Move, 4);

				// 移動していたら
				if (!(oldX == status.X && oldY == status.Y))
				{
					// もともといたパネルのデータをリセット
					this.GetPanelData(oldX, oldY).DataReset();
				}
			}
		}
	}

	IEnumerator AllEnemyAttack(float waitTime)
	{
		foreach (Transform child in this.transform.Find("Enemys"))
		{
			yield return new WaitForSeconds(waitTime);

			if (child)
			{
				var status = child.GetComponent<StatusBase>();

				_targetList.Clear();
				this.SearchPossibleRange(status.X, status.Y - 1, status.Range, 1);
				this.SearchPossibleRange(status.X + 1, status.Y, status.Range, 2);
				this.SearchPossibleRange(status.X, status.Y + 1, status.Range, 3);
				this.SearchPossibleRange(status.X - 1, status.Y, status.Range, 4);

				_targetList.Sort((a, b) =>
				a.GetComponent<StatusBase>().Hp - b.GetComponent<StatusBase>().Hp
				);

				for (int i = 0; i < status.AttackNum; i++)
				{
					if (i < _targetList.Count)
					{
						var targetStatus = _targetList[i].GetComponent<StatusBase>();

						DamageUI.Instance.SetDamage(status.Attack, this.GetPanelLocalPosition(targetStatus.X, targetStatus.Y));
						targetStatus.Hp -= status.Attack;
					}
				}
			}
		}
	}

	void EnemyRangeMove(Transform enemy, int startDir, int startX, int startY, int x, int y, int mv, int di)
	{
		Panel panelData = this.GetPanelData(x, y);

		if (panelData == null || enemy.GetComponent<StatusBase>().IsMoved)
			return;

		// パネルの上にすでに何か存在していたら終了
		if (panelData.OnObj)
		{
			// それがプレイヤーだったら近くに移動
			if (panelData.OnObj.GetComponent<StatusBase>().IsPlayer)
			{
				StatusBase status = enemy.GetComponent<StatusBase>();

				if (startDir == 1) status.SetPos(startX, startY + 1);
				if (startDir == 2) status.SetPos(startX - 1, startY);
				if (startDir == 3) status.SetPos(startX, startY - 1);
				if (startDir == 4) status.SetPos(startX + 1, startY);
			}

			return;
		}

		// 現在の地点にあるマップ情報を取り出して、ウェイトを計算
		int down = this.WeightCheck(panelData);

		// 歩数がマイナスになった地点へは進めないので中断
		if (mv + down < 0)
			return;

		if (di != 3) this.EnemyRangeMove(enemy, startDir, startX, startY, x, y - 1, mv + down, 1);
		if (di != 4) this.EnemyRangeMove(enemy, startDir, startX, startY, x + 1, y, mv + down, 2);
		if (di != 1) this.EnemyRangeMove(enemy, startDir, startX, startY, x, y + 1, mv + down, 3);
		if (di != 2) this.EnemyRangeMove(enemy, startDir, startX, startY, x - 1, y, mv + down, 4);
	}

	void EnemyMove(Transform enemy, int x, int y, int mv, int di)
	{
		Panel panelData = this.GetPanelData(x, y);

		if (panelData == null || enemy.GetComponent<StatusBase>().IsMoved)
			return;

		// パネルの上にすでに何か存在していたら終了
		if (panelData.OnObj)
		{
			// それがプレイヤーだったら近くに移動
			if (panelData.OnObj.GetComponent<StatusBase>().IsPlayer)
			{
				StatusBase status = enemy.GetComponent<StatusBase>();

				if (di == 1) status.SetPos(x, y + 1);
				if (di == 2) status.SetPos(x - 1, y);
				if (di == 3) status.SetPos(x, y - 1);
				if (di == 4) status.SetPos(x + 1, y);
			}

			return;
		}

		// 現在の地点にあるマップ情報を取り出して、ウェイトを計算
		int down = this.WeightCheck(panelData);

		// 歩数がマイナスになった地点へは進めないので中断
		if (mv + down < 0)
		{
			StatusBase status = enemy.GetComponent<StatusBase>();

			int range = (status.Range - 1);

			if (range > 0)
			{
				if (di != 3) this.EnemyRangeMove(enemy, di, x, y, x, y - 1, range + down, 1);
				if (di != 4) this.EnemyRangeMove(enemy, di, x, y, x + 1, y, range + down, 2);
				if (di != 1) this.EnemyRangeMove(enemy, di, x, y, x, y + 1, range + down, 3);
				if (di != 2) this.EnemyRangeMove(enemy, di, x, y, x - 1, y, range + down, 4);
			}
			return;
		}

		if (di != 3) this.EnemyMove(enemy, x, y - 1, mv + down, 1);
		if (di != 4) this.EnemyMove(enemy, x + 1, y, mv + down, 2);
		if (di != 1) this.EnemyMove(enemy, x, y + 1, mv + down, 3);
		if (di != 2) this.EnemyMove(enemy, x - 1, y, mv + down, 4);
	}

	// 指定したパネルに移動
	void PlayerMove(Panel panel)
	{
		if (GameManager.Instance.isEnemyTurn == false)
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
					this.AllCheckBetween();
				}
			}
		}
	}

	int WeightCheck(Panel p)
	{
		int down = 0;
		switch (p.Type)
		{
			case 0: // 進行不可
				down = -999;
				break;
			case 1: // 平地
				down = -1;
				break;
		}

		return down;
	}

	void SearchPossibleRange(int x, int y, int ra, int di)
	{
		if (ra <= 0)
			return;

		Panel panelData = this.GetPanelData(x, y);

		if (panelData == null)
			return;

		if (panelData.OnObj)
		{
			if (panelData.OnObj.GetComponent<StatusBase>().IsPlayer)
				_targetList.Add(panelData.OnObj);

			return;
		}

		if (panelData.IsCheck)
			return;

		int down = this.WeightCheck(panelData);

		if (ra + down < 0)
			return;

		// マーク
		this.SetPossibleRangePanel(x, y);

		if (di != 3) this.SearchPossibleRange(x, y - 1, ra + down, 1);
		if (di != 4) this.SearchPossibleRange(x + 1, y, ra + down, 2);
		if (di != 1) this.SearchPossibleRange(x, y + 1, ra + down, 3);
		if (di != 2) this.SearchPossibleRange(x - 1, y, ra + down, 4);
	}

	void SetPossibleMovePanel(int x, int y)
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

	void SetPossibleRangePanel(int x, int y)
	{
		Color32 moveCol = PANEL_COL;
		moveCol.g -= 80;
		moveCol.b -= 80;
		moveCol.a += 100;

		// Imageの色変更
		var image = this._panelData[y, x].GetComponent<Image>();
		DOTween.To(
			() => image.color,
			color => image.color = color,
			moveCol,
			0.175f
		);
	}

	public void SetALlBanPanelCol()
	{
		Color32 moveCol = PANEL_COL;
		moveCol.g = 0;
		moveCol.b = 0;
		moveCol.a += 150;

		for (int y = 0; y < _panelData.GetLength(0); y++)
		{
			for (int x = 0; x < _panelData.GetLength(1); x++)
			{
				if (this.GetPanelData(x,y).Type == 0)
				{
					var image = this._panelData[y, x].GetComponent<Image>();
					DOTween.To(
						() => image.color,
						color => image.color = color,
						moveCol,
						0.175f
					);
				}
			}
		}
	}

	// パネルの作成
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
			this.PlayerMove(panel);
		});

		return child;
	}

	// 背景の生成
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