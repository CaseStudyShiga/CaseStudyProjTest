using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class UI : MonoBehaviour
{
	GameObject _topUI;
	GameObject _bottomUI;
	GameObject _menuUI;

	void Start()
	{
		this.InitField();
	}

	void Update()
	{
	}

	void InitField()
	{
		_topUI = CreateChild("TopUI");
		_bottomUI = CreateChild("BottomUI");
		_menuUI = CreateChild("MenuUI");

		_topUI.AddComponent<TopUI>();
		_bottomUI.AddComponent<BottomUI>();
		_menuUI.AddComponent<MenuUI>();
	}

	GameObject CreateChild(string name)
	{
		GameObject obj = new GameObject(name);
		obj.transform.SetParent(this.transform);

		return obj;
	}
}

