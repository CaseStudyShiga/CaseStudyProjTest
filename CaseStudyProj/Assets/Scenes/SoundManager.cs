﻿/*******************************************************************************
* タイトル   : サウンドマネージャ用スクリプト
* ファイル名 : SoundManager.cs
* 作成者     : 志賀 麻言
* 作成日     : 2016/07/28
********************************************************************************
* 更新履歴	- 2016/07/28
*			-V0.01 Initial Version
*******************************************************************************/

/*******************************************************************************
* ステートメント設定( 自動リソース開放機能 )
*******************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*******************************************************************************
* クラス設計
*******************************************************************************/
public class SoundManager
{
	/// SEチャンネル数
	const int SE_CHANNEL = 4;

	/// サウンド種別
	enum eType
	{
		Bgm, // BGM
		Se,  // SE
	}

	// シングルトン
	static SoundManager _singleton = null;
	public static SoundManager Instance
	{
		get
		{
			if (_singleton == null)
			{
				_singleton = new SoundManager();
				//GameObject obj = new GameObject("SoundManager");
				//_singleton = obj.AddComponent<SoundManager>();
			}
			return _singleton;
		}
	}

	// サウンド再生のためのゲームオブジェクト
	GameObject _object = null;
	// サウンドリソース
	AudioSource _sourceBgm = null; // BGM
	AudioSource _sourceSeDefault = null; // SE (デフォルト)
	AudioSource[] _sourceSeArray; // SE (チャンネル)
	// BGMにアクセスするためのテーブル
	Dictionary<string, _Data> _poolBgm = new Dictionary<string, _Data>();
	// SEにアクセスするためのテーブル 
	Dictionary<string, _Data> _poolSe = new Dictionary<string, _Data>();

	/// 保持するデータ
	class _Data
	{
		/// アクセス用のキー
		public string Key;
		/// リソース名
		public string ResName;
		/// AudioClip
		public AudioClip Clip;

		/// コンストラクタ
		public _Data(string key, string res)
		{
			Key = key;
			ResName = "Sounds/" + res;
			// AudioClipの取得
			Clip = Resources.Load(ResName) as AudioClip;
		}
	}

	/// コンストラクタ
	public SoundManager()
	{
		// チャンネル確保
		_sourceSeArray = new AudioSource[SE_CHANNEL];
	}

	/// AudioSourceを取得する
	AudioSource _GetAudioSource(eType type, int channel = -1)
	{
		if (_object == null)
		{
			// GameObjectがなければ作る
			_object = new GameObject("SoundManager");
			// 破棄しないようにする
			GameObject.DontDestroyOnLoad(_object);
			// AudioSourceを作成
			_sourceBgm = _object.AddComponent<AudioSource>();
			_sourceSeDefault = _object.AddComponent<AudioSource>();
			for (int i = 0; i < SE_CHANNEL; i++)
			{
				_sourceSeArray[i] = _object.AddComponent<AudioSource>();
			}
		}

		if (type == eType.Bgm)
		{
			// BGM
			return _sourceBgm;
		}
		else
		{
			// SE
			if (0 <= channel && channel < SE_CHANNEL)
			{
				// チャンネル指定
				return _sourceSeArray[channel];
			}
			else
			{
				// デフォルト
				return _sourceSeDefault;
			}
		}
	}

	// サウンドのロード
	// ※Resources/Soundsフォルダに配置すること
	public void LoadBgm(string key, string resName)
	{
		Instance._LoadBgm(key, resName);
	}
	public void LoadSe(string key, string resName)
	{
		Instance._LoadSe(key, resName);
	}
	void _LoadBgm(string key, string resName)
	{
		if (_poolBgm.ContainsKey(key))
		{
			// すでに登録済みなのでいったん消す
			_poolBgm.Remove(key);
		}
		_poolBgm.Add(key, new _Data(key, resName));
	}
	void _LoadSe(string key, string resName)
	{
		if (_poolSe.ContainsKey(key))
		{
			// すでに登録済みなのでいったん消す
			_poolSe.Remove(key);
		}
		_poolSe.Add(key, new _Data(key, resName));
	}

	/// BGMの再生
	/// ※事前にLoadBgmでロードしておくこと
	public bool PlayBgm(string key, bool loop = true)
	{
		return Instance._PlayBgm(key, loop);
	}
	bool _PlayBgm(string key, bool loop)
	{
		if (_poolBgm.ContainsKey(key) == false)
		{
			// 対応するキーがない
			return false;
		}

		// いったん止める
		_StopBgm();

		// リソースの取得
		var _data = _poolBgm[key];

		// 再生
		var source = _GetAudioSource(eType.Bgm);
		source.loop = loop;
		source.clip = _data.Clip;
		source.Play();

		return true;
	}
	/// BGMの停止
	public bool StopBgm()
	{
		return Instance._StopBgm();
	}
	bool _StopBgm()
	{
		_GetAudioSource(eType.Bgm).Stop();

		return true;
	}

	/// SEの再生
	/// ※事前にLoadSeでロードしておくこと
	public bool PlaySe(string key, int channel = -1)
	{
		return Instance._PlaySe(key, channel);
	}
	bool _PlaySe(string key, int channel = -1)
	{
		if (_poolSe.ContainsKey(key) == false)
		{
			// 対応するキーがない
			return false;
		}

		// リソースの取得
		var _data = _poolSe[key];

		if (0 <= channel && channel < SE_CHANNEL)
		{
			// チャンネル指定
			var source = _GetAudioSource(eType.Se, channel);
			source.clip = _data.Clip;
			source.Play();
		}
		else
		{
			// デフォルトで再生
			var source = _GetAudioSource(eType.Se);
			source.PlayOneShot(_data.Clip);
		}

		return true;
	}
}
