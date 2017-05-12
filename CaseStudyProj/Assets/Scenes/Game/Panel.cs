using UnityEngine;

public class Panel : MonoBehaviour
{
	public int _x;
	public int _y;
	public int _type;		// パネルの種類　※ダメージパネルなのか 通常パネルなのか
	public int _value;     // 各タイプによる　値　※ダメージ値や回復値など
	public bool _isCheck;  // 探索済みかどうか
	public Transform _onObj; // 上のやつ

	public int X { get { return _x; } set { _x = value; } }
	public int Y { get { return _y; } set { _y = value; } }
	public int Type { get { return _type; } set { _type = value; } }
	public int Value { get { return _value; } set { _value = value; } }
	public bool IsCheck { get { return _isCheck; } set { _isCheck = value; } }
	public Transform OnObj { get { return _onObj; } set { _onObj = value; } }

	public void DataReset()
	{
		_isCheck = false;
		_onObj = null;
	}
}