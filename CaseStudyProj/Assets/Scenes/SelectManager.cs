using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Area
{
	public int ID;
	public string Name;
	public int Difficulty;
	public int StageNumMax;

	public Area(int id, string t, int n)
	{
		this.ID = id;
		this.Name = t;
		this.Difficulty = n;

		switch (this.ID)
		{
			case 0:
				this.StageNumMax = 12;
				break;
			case 1:
				this.StageNumMax = 10;
				break;
			case 2:
				this.StageNumMax = 1;
				break;
			default:
				this.StageNumMax = 0;
				break;
		}
	}
}

class SelectManager
{
	static SelectManager _instance;

	public static SelectManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SelectManager();
				_instance.InitField();
			}
			return _instance;
		}
	}

	List<Area> _areaList;
	int _stageID;

	public List<Area> AreaList { get { return this._areaList; } }
	public int StageID { get { return this._stageID; } set { this._stageID = value; } }

	void InitField()
	{
		this._areaList = new List<Area>();
		this._areaList.Add(new Area(0, "街", 1));
		this._areaList.Add(new Area(1, "公園", 2));
		this._areaList.Add(new Area(2, "学校", 3));
		this._stageID = 0;
	}
}