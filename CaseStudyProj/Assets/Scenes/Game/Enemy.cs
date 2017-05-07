using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharBase
{
	GameObject _enemy;

	public GameObject Create(Transform statge, StatusBase.eType type, int x, int y)
	{
		_enemy = this.CreateChild(type, "enemy", statge.Find("Enemys"), statge.gameObject, null, new Vector2(60, 60));
		this.Status.SetPos(x, y);
		this.Status.IsPlayer = false;
		this.Status.SetColor(new Color32(200, 0, 255, 255), new Color32(200, 120, 255, 255), new Color32(255, 200, 0, 255));

		return _enemy;
	}
}