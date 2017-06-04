using UnityEngine;
using System;

[Serializable]
public class SaveData
{
	[Serializable]
	public struct Data {
		public int AreaID;
		public int StageID;
		public string Name;
		public bool[] IsStar;
	}

	public Data[] data = new Data[3];
}