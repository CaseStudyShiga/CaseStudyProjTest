using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class SaveManager
{
	static SaveManager _instance;
	public static SaveManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SaveManager();
				_instance.InitField();
			}
			return _instance;
		}
	}

	static string SavePath;
	SaveData _saveData;

	public SaveData SaveData { get { return _saveData; } set { _saveData = value; } }

	void InitField()
	{
#if UNITY_EDITOR
		Debug.Log("Unity Editor");
		SavePath = Application.dataPath + "/save.json";

#elif UNITY_IPHONE
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		Debug.Log("Unity iPhone");
		SavePath = Application.persistentDataPath + "/save.json";

#elif UNITY_ANDROID
		string p = "jar:file://" + Application.dataPath + "!/assets" + "/" + "save.json";
		WWW www = new WWW(p);
		TextReader txtReader = new StringReader(www.text);
		SavePath = txtReader.ToString();
#endif


		_saveData = new SaveData();
	}

	///// 保存 /////////////////////////////////////
	public void Save()
	{
		string json = JsonUtility.ToJson(this._saveData);
		using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, json);
		}

		Debug.Log("SaveData - Save");
	}

	///// 読み込み /////////////////////////////////////
	public void Load()
	{
		if (File.Exists(SavePath) == false)
		{
			this.Reset();
		}

		string json;
		using (FileStream fs = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
		{
			BinaryFormatter bf = new BinaryFormatter();
			json = bf.Deserialize(fs) as string;
		}

		var obj = JsonUtility.FromJson<SaveData>(json);

		_saveData = obj;

		Debug.Log("SaveData - Load");
	}

	// セーブデータリセット
	public void Reset()
	{
		this._saveData.data.Clear();
		SelectManager.Instance.AreaList.Select((area,idx) =>
		{
			for (int i = 0; i < area.StageNumMax; i++)
			{
				SaveData.Data data;
				data.AreaID = area.ID;
				data.StageID = i;
				data.Name = "stage" + i.ToString();
				data.IsStar = new bool[3];
				data.IsStar[0] = false;
				data.IsStar[1] = false;
				data.IsStar[2] = false;

				var hoges = this._saveData.data.Where(d => d.AreaID == data.AreaID && d.StageID == data.StageID).ToList();
				if (hoges.Count <= 0)
				{
					this._saveData.data.Add(data);
				}
			}

			return area;
		}).ToList();

		this.Save();

		Debug.Log("SaveData - Reset");
	}
}