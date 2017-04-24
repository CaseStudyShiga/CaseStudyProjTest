//------------------------------------------------------------------------------
// 作成者     : 志賀 麻言
// 作成日     : 2016/07/26
//------------------------------------------------------------------------------
// 更新履歴	- 2016/07/26
//			-V0.01 Initial Version
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

//------------------------------------------------------------------------------
// クラス
//------------------------------------------------------------------------------
public class ButtonManager : MonoBehaviour
{
	Texture2D _mouse;
	public class Data
	{
		string _audio;
		Action _ac;

		public string Audio { get { return this._audio; } set { this._audio = value; } }
		public Action Ac { get { return this._ac; } set { this._ac = value; } }

		public Data(string audio, Action ac)
		{
			this._audio = audio;
			this._ac = ac;
		}
	}

	static ButtonManager _instance;

	public static ButtonManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("ButtonManager");
				_instance = obj.AddComponent<ButtonManager>();
			}
			return _instance;
		}
	}

	void Awake()
	{
		this._mouse = Resources.Load<Texture2D>("mouse");
		DontDestroyOnLoad(this.gameObject);
	}

	void EventTriggerAddListener(EventTriggerType type, List<EventTrigger.Entry> list, Action callBack)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = type;
		entry.callback.AddListener((eventData) => callBack());
		list.Add(entry);
	}

	void SetMousePointerAction(Button btn)
	{
		List<EventTrigger.Entry> list = btn.gameObject.AddComponent<EventTrigger>().triggers;

		this.EventTriggerAddListener(EventTriggerType.PointerEnter, list, () => {
			Cursor.SetCursor(this._mouse, Vector2.zero, CursorMode.ForceSoftware);
		});
		this.EventTriggerAddListener(EventTriggerType.PointerExit, list, () => {
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		});
	}

	public void SetAction(GameObject obj, Action ac)
	{
		Button btn = obj.GetComponent<Button>();
		btn.onClick.AddListener(() => ac());
	}
}
