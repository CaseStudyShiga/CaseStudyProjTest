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
	GameObject _endchkUI;

	void Start()
	{
		this.InitField();
	}

	void Update()
	{
	}

	void InitField()
	{
		this._topUI = CreateChild("TopUI");
		this._bottomUI = CreateChild("BottomUI");
		this._menuUI = CreateChild("MenuUI");
		this._endchkUI = CreateChild("EndChkUI");

		this._topUI.AddComponent<TopUI>();
		this._bottomUI.AddComponent<BottomUI>();
		this._menuUI.AddComponent<MenuUI>();
		this._endchkUI.AddComponent<EndChkUI>();
	}

	GameObject CreateChild(string name)
	{
		GameObject obj = new GameObject(name);
		obj.transform.SetParent(this.transform);

		return obj;
	}
}

