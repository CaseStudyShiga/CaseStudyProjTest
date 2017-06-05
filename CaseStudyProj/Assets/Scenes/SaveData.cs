using UnityEngine;
using System;
using System.Collections.Generic;

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

	public List<Data> data = new List<Data>();
}