using UnityEngine;

/// <summary>
/// GameObject クラスに関する汎用関数を管理するクラス
/// </summary>
public static class GameObjectUtils
{
	/// <summary>
	/// 指定された GameObject を複製して返します
	/// </summary>
	public static GameObject Clone(GameObject go)
	{
		var clone = GameObject.Instantiate(go) as GameObject;
		clone.transform.parent = go.transform.parent;
		clone.transform.localPosition = go.transform.localPosition;
		clone.transform.localScale = go.transform.localScale;
		return clone;
	}
}