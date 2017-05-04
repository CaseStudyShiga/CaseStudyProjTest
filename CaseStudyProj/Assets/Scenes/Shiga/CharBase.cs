using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharBase
{
	StatusBase _status;
	public StatusBase Status { get { return _status; } set { _status = value; } }

	public GameObject CreateChild(StatusBase.eType type, string name, Transform parent,GameObject stage, Sprite sp, Vector2 size)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.AddComponent<Image>().sprite = sp;
		child.AddComponent<Button>();

		switch (type)
		{
			case StatusBase.eType.eAttacker:
				_status = child.AddComponent<AttackStatus>();
				break;

			case StatusBase.eType.eDefender:
				break;
		}

		_status.Stage = stage;

		return child;
	}
}
