using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : StageBase
{
	// 変数
	int[,] _stageData = {
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1},
	};

	// 外部読み取り用　関数
	public int[,] StageData { get { return _stageData; } }

	void Start()
	{
		this.CreateStageBase(_stageData);
		this.InitField();
		this.AllCheckBetween();
	}

	void Update()
	{
	}

	void InitField()
	{
		var data = CSVDataReader.Instance.Data;
		for (int y = 0; y < data.GetLength(0); y++)
		{
			for (int x = 0; x < data.GetLength(1); x++)
			{
				this.GetPanelData(x, y).DataReset();

				if (data[y, x].IndexOf('P') != -1)
				{
					Player player = new Player();
					player.Create(this.transform, data[y, x], x, y);
					this.PlayerMaxNum++;
				}

				if (data[y, x].IndexOf('E') != -1)
				{
					Enemy enemy = new Enemy();
					enemy.Create(this.transform, data[y, x], x, y);
				}

				if (data[y, x].IndexOf('X') != -1)
				{
					this.GetPanelData(x, y).Type = 0;
				}
			}
		}
	}

	public void Reset()
	{
		foreach (Transform child in this.transform.Find("Players"))
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in this.transform.Find("Enemys"))
		{
			Destroy(child.gameObject);
		}

		this.PlayerMaxNum = 0;
		this.InitField();
		this.ClearPossibleMovePanel();
		this.AllCheckBetween();
	}
}
