using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : UIBase
{
	GameObject _background;
	GameObject _face;
	GameObject _statusUI;

	public GameObject StatusUI { get { return _statusUI; } }

	void Start ()
	{
		this.InitField();
	}

	void Update ()
	{
	}

	void InitField()
	{
		_background = this.CreateChild("BackGround", this.transform, new Vector2(661, 177), new Vector3(-7, -555));
		_face = this.CreateChild("FaceImage", this.transform, new Vector2(160, 160), new Vector3(-225, -555));
		_statusUI = this.CreateStatusUI();

		_background.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
		_background.transform.SetSiblingIndex(0);
		_face.transform.SetSiblingIndex(1);
	}

	GameObject CreateStatusUI()
	{
		GameObject child = new GameObject("StatusUI");
		child.transform.SetParent(this.transform);
		child.AddComponent<RectTransform>().localPosition = new Vector3(-7, -560);
		child.AddComponent<StatusUI>();

		return child;
	}
}
