using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class Select : MonoBehaviour
{
	public enum eMode
	{
		eArea = 0,
		eStage,
	}

	GameObject _areaSelect;
	GameObject _stageSelect;
	GameObject _backBtn;
	GameObject _configBtn;
	eMode _mode;
	GameObject _configUI;

	public eMode Mode { get { return _mode; } }
	public GameObject AreaSelect { get { return _areaSelect; } set { _areaSelect = value; } }
	public GameObject StageSelect { get { return _stageSelect; } set { _stageSelect = value; } }

	void Awake()
	{
		this.InitField();
	}

	void Start ()
	{
		SaveManager.Instance.Load();
		Fader.instance.BlackIn();
	}

	void Update ()
	{
		
	}

	void BackBtnAction()
	{
		switch (this._mode)
		{
			case eMode.eArea:
				break;
			case eMode.eStage:
				this.SetStageSelectActive(false);
				break;
		}
	}

	void ConfigBtnAction()
	{
	}

	void InitField()
	{
		this._areaSelect = new GameObject("AreaSelect");
		this._areaSelect.transform.SetParent(this.transform);
		this._areaSelect.AddComponent<AreaSelect>();
		this._areaSelect.transform.localPosition = Vector3.zero;

		this._stageSelect = new GameObject("StageSelect");
		this._stageSelect.transform.SetParent(this.transform);
		this._stageSelect.AddComponent<StageSelect>();
		this._stageSelect.transform.localPosition = Vector3.zero;

		this._configUI = this.CreateChild("ConfigUI", this.transform, Vector2.one, Vector3.zero);
		var config = this._configUI.AddComponent<ConfigUI>();
		config.IsMenu = false;

		Vector2 btnSize = new Vector2(150, 150);
		this._backBtn = this.CreateButton("BackBtn", this.transform, btnSize, new Vector3(-260.5f, 570), Resources.Load<Sprite>("Sprites/GUI/areaSelectUI_backButton"), this.BackBtnAction);
		this._configBtn = this.CreateButton("ConfigBtn", this.transform, btnSize, new Vector3(270, 570), Resources.Load<Sprite>("Sprites/GUI/areaSelectUI_configButton"), config.ActiveMethod);

		this._configUI.transform.SetAsLastSibling();
		this._stageSelect.SetActive(false);
	}

	public void SetStageSelectActive(bool flag, Area area = null)
	{
		if (flag)
		{
			this._mode = eMode.eStage;
			this._areaSelect.SetActive(false);
			this._stageSelect.SetActive(true);
			var stage = this._stageSelect.GetComponent<StageSelect>();
			stage.SetArea(area);
		}
		else
		{
			this._mode = eMode.eArea;
			this._stageSelect.GetComponent<StageSelect>().DeleteStages();
			this._areaSelect.SetActive(true);
			this._stageSelect.SetActive(false);
		}
	}

	GameObject CreateButton(string name, Transform parent, Vector2 size, Vector3 pos, Sprite sp, UnityAction buttonMethod)
	{
		GameObject child = this.CreateChild(name, parent, size, pos, sp);
		child.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
		child.AddComponent<Button>().onClick.AddListener(buttonMethod);

		return child;
	}

	GameObject CreateChild(string name, Transform parent, Vector2 size, Vector3 pos, Sprite sp = null)
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(parent);
		child.AddComponent<RectTransform>();
		child.GetComponent<RectTransform>().sizeDelta = size;
		child.GetComponent<RectTransform>().localScale = Vector3.one;
		child.GetComponent<RectTransform>().localPosition = pos;
		child.AddComponent<Image>().sprite = sp;

		return child;
	}
}
