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
		_enemy.GetComponent<Image>().color = new Color32(200, 0, 255, 255);
		this.Status.SetPos(x, y);

		return _enemy;
	}
}