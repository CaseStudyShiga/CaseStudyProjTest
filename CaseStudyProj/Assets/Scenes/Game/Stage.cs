using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : StageBase
{
	// 変数
	Player[] _player;
	Enemy[] _enemy;
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
		SaveManager.Instance.Save();
		SaveManager.Instance.Load();
	}

	void Update()
	{
	}

	void InitField()
	{
		var data = CSVDataReader.Instance.Data;
		for (int i = 0; i < data.GetLength(0); i++)
		{
			for (int j = 0; j < data.GetLength(1); j++)
			{
				if (data[i, j].IndexOf('P') != -1)
				{
					Player player = new Player();
					int type = 0;
					GameManager.Instance.StageTable.TryGetValue(data[i, j], out type);
					player.Create(this.transform, type, j, i);
				}

				if (data[i, j].IndexOf('E') != -1)
				{
					Enemy enemy = new Enemy();
					int type = 2;
					GameManager.Instance.StageTable.TryGetValue(data[i, j], out type);
					enemy.Create(this.transform, type, j, i);
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

		this.InitField();
		this.ClearPossibleMovePanel();
	}
}
