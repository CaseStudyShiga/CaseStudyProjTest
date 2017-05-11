using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
	public GameObject CreateChild(string name, Transform parent, Vector2 size, Vector3 pos, Sprite sp = null)
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

	public GameObject CreateText(string name, string textValue, Transform parent, Vector3 pos, int size = 29)
	{
		GameObject obj = new GameObject(name);
		Text text = obj.AddComponent<Text>();
		//text.font = Resources.Load<Font>("Font/Arial");
		text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text.text = textValue;
		text.color = Color.black;
		text.fontSize = size;
		text.alignment = TextAnchor.MiddleCenter;
		obj.transform.SetParent(parent);
		obj.GetComponent<RectTransform>().localPosition = pos;
		obj.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);

		return obj;
	}

	public Text GetText(Transform obj, string name)
	{
		Transform child = obj.Find(name);
		return child.GetComponent<Text>();
	}

}