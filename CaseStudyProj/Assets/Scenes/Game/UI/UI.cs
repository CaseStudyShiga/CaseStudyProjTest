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
	GameObject _resultUI;
	GameObject _tutorialUI;

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
		this._resultUI = CreateChild("ResultUI");
		this._tutorialUI = CreateChild("TutorialUI");

		this._topUI.AddComponent<TopUI>();
		this._bottomUI.AddComponent<BottomUI>();
		this._menuUI.AddComponent<MenuUI>();
		this._endchkUI.AddComponent<EndChkUI>();
		this._resultUI.AddComponent<ResultUI>();
		this._tutorialUI.AddComponent<TutorialUI>();
	}

	GameObject CreateChild(string name)
	{
		GameObject obj = new GameObject(name);
		obj.transform.SetParent(this.transform);

		return obj;
	}
}

