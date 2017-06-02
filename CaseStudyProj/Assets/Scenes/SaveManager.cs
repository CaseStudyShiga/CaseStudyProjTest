using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
	static SaveManager _instance;
	public static SaveManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("SaveManager");
				_instance = obj.AddComponent<SaveManager>();
			}
			return _instance;
		}
	}

	readonly int STAGE_NUJM = 3;
	static string SavePath;
	SaveData _saveData;

	void Awake()
	{
		SavePath = Application.dataPath + "/save.json";
		_saveData = new SaveData();
	}

	void Start()
	{
		// iOSでは下記設定を行わないとエラーになる
#if UNITY_IPHONE
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
#endif
	}

	///// 保存 /////////////////////////////////////
	public void Save()
	{
		for (int i = 0; i < STAGE_NUJM; i++)
		{
			this._saveData.data[i].Id = 0;
			this._saveData.data[i].Name = "stage" + i.ToString().PadLeft(2, '0');
			this._saveData.data[i].IsStar = new bool[3];
			this._saveData.data[i].IsStar[0] = true;
			this._saveData.data[i].IsStar[1] = false;
			this._saveData.data[i].IsStar[2] = true;
		}		

		string json = JsonUtility.ToJson(this._saveData);
		using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, json);
		}

		for (int i = 0; i < STAGE_NUJM; i++)
		{
			Debug.Log("[Save]Name:" + this._saveData.data[i].Name);
		}
	}

	///// 読み込み /////////////////////////////////////
	public void Load()
	{
		string json;
		using (FileStream fs = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
		{
			BinaryFormatter bf = new BinaryFormatter();
			json = bf.Deserialize(fs) as string;
		}

		var obj = JsonUtility.FromJson<SaveData>(json);

		for (int i = 0; i < STAGE_NUJM; i++)
		{
			Debug.Log("[Load]Name:" + obj.data[i].Name);
		}
	}
}