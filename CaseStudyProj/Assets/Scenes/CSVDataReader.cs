using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.IO;

class CSVDataReader
{
	static CSVDataReader _instance;
	string[,] _sdataArrays;
	int _areaID;
	int _stageID;
	int _minTotalTurn;

	public int AreaID { get { return this._areaID; } }
	public int StageID { get { return this._stageID; } }
	public int MinTotalTurn { get { return this._minTotalTurn; } }

	public static CSVDataReader Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new CSVDataReader();
				_instance.Load(0, 0);
			}
			return _instance;
		}
	}

	public string[,] Data { get { return _sdataArrays; } }

	void Start()
	{
	}

	void UpDate()
	{
	}

	public void Load(int areaID, int stageID)
	{
		this._areaID = areaID;
		this._stageID = stageID;
		this.LoadText(areaID, "stage" + stageID);
	}

	void ReadCSVData(string path, ref string[,] sdata)
	{
		StreamReader srOrg = new StreamReader(path);
		StreamReader sr = new StreamReader(path);
		char str = (char)sr.Read();

		int min = 0;
		if (int.TryParse(str.ToString(), out min))
		{
			this._minTotalTurn = min;
		}
		else
		{
			this._minTotalTurn = 88;
		}


		sr = srOrg;
		sr.ReadLine(); // 1行読み捨てる。
		string strStream = sr.ReadToEnd();

		// StringSplitOptionを設定
		System.StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries;

		// 行に分ける
		string[] lines = strStream.Split(new char[] { '\r', '\n' }, option);

		// カンマ分けの準備(区分けする文字を設定する)
		char[] spliter = new char[1] { ',' };

		int h = lines.Length;
		int w = lines[0].Split(spliter, option).Length;

		sdata = new string[h, w];
		for (int i = 0; i < h; i++)
		{
			string[] splitedData = lines[i].Split(spliter, option);

			for (int j = 0; j < w; j++)
			{
				sdata[i, j] = splitedData[j];
			}
		}
	}

	//確認表示用の関数
	//引数：2次元配列データ,行数,列数
	void WriteMapDatas(string[,] sarrays, int hgt, int wid)
	{
		for (int i = 0; i < hgt; i++)
		{
			for (int j = 0; j < wid; j++)
			{
				//行番号-列番号:データ値 と表示される
				Debug.Log(i + "-" + j + ":" + sarrays[i, j]);
			}
		}
	}

	void LoadText(int id, string f)
	{
		//データパスを設定
		//このデータパスは、Assetフォルダ以下の位置を書くので/で階層を区切り、CSVデータ名まで書かないと読み込んでくれない
		string textFileName = "/CSV" + "/Area" + id.ToString() + "/" + f + ".csv";
		string path = "";

#if UNITY_EDITOR
		Debug.Log("Unity Editor");
		path = Application.streamingAssetsPath + textFileName;

#elif UNITY_IPHONE
		Debug.Log("Unity iPhone");
		path = Application.streamingAssetsPath + textFileName;	

#elif UNITY_ANDROID
		string p = "jar:file://" + Application.dataPath + "!/assets" + "/" + textFileName;
		WWW www = new WWW(p);
		TextReader txtReader = new StringReader(www.text);
		path = txtReader.ToString();
#endif

		ReadCSVData(path, ref this._sdataArrays);
		//WriteMapDatas(this._sdataArrays, this._sdataArrays.GetLength(0), this._sdataArrays.GetLength(1));
	}
}

