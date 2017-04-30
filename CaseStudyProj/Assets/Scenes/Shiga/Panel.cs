using UnityEngine;

public class Panel : MonoBehaviour
{
	int _index;		// パネル番号
	int _type;		// パネルの種類　※ダメージパネルなのか 通常パネルなのか
	int _value;		// 各タイプによる　値　※ダメージ値や回復値など

	public void SetData(int idx, int type, int val)
	{
		this._index = idx;
		this._type = type;
		this._value = val;
	}
}