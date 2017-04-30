using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class StageBase : MonoBehaviour {

	// 定数
	const int MAX_PANEL = 80;
	const float SPACE_COEFFICIENT = 1.2f;

	// 変数
	GameObject[,] _panelData;

	void Start () {
	}

	void Update () {
	}

	public void CreateStageBase(int[,] stageData, int panelSize) {
		GameObject panels = new GameObject("Panels");
		GameObject players = new GameObject("Players");
		GameObject enemys = new GameObject("Enemys");
		Vector3 BasePos = new Vector3(-300,350,0);
		int index = 0;

		panels.transform.SetParent(this.transform);
		panels.transform.localPosition = Vector3.zero;
		players.transform.SetParent(this.transform);
		players.transform.localPosition = Vector3.zero;
		enemys.transform.SetParent(this.transform);
		enemys.transform.localPosition = Vector3.zero;

		_panelData = new GameObject[stageData.GetLength(0), stageData.GetLength(1)];

		for (int y = 0; y < stageData.GetLength(0); y++)
		{
			for (int x = 0; x < stageData.GetLength(1); x++)
			{
				if (stageData[y, x] != 0)
				{
					_panelData[y, x] = this.CreateChild("panel" + index, panels, null, new Vector2(panelSize, panelSize));

					// コードの二次元配列に合わせるため Y軸反転
					_panelData[y, x].GetComponent<RectTransform>().localPosition = BasePos + new Vector3(x * (panelSize * SPACE_COEFFICIENT), -y * (panelSize * SPACE_COEFFICIENT), 0.0f);
					index++;
				}
			}
		}
	}

	public bool IsPanelExist(int x, int y)
	{
		return (_panelData[y, x] == null) ? false : true;
	}

	public Vector3 GetPanelLocalPosition(int x, int y)
	{
		return _panelData[y, x].transform.localPosition;
	}

	GameObject CreateChild(string name, GameObject parent, Sprite sp, Vector2 size) {
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent.transform);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.AddComponent<Image>().sprite = sp;
		child.AddComponent<Panel>();
		return child;
	}
}
