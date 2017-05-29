using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;

public class ButtonSystem : MonoBehaviour
{

    //------ 変数の生成 ------
    public static string SceneName;

    //// Use this for initialization
    void Start()
    {
        //------ クリックした際にオブジェクト名取得 ------
        GetGameObjName();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //-----------------------------------------------------
    //Sceneの移動
    //-----------------------------------------------------
    public void MoveScene()
    {
        Fader.instance.BlackOut();                      // フェードアウト
        StartCoroutine(DelayMethod(1.2f, SceneName));				// 1.2秒後に実行する
    }

    //-----------------------------------------------------
    //画面選択時の際に出現させる確認パネルを表示
    //-----------------------------------------------------
    public void onConfirmationPanel()
    {
        //W:450 H:795

        GameObject ConfirmationPanel = GameObject.Find("ConfirmationPanel");

        //--- もしパネルが指定の値まで伸びきったら表示 ---
        //if(ConfirmationPanel.transform.position.x)
        //{

        //}

        GameObject game_object = ConfirmationPanel.transform.FindChild("ConfirmationImage").gameObject;

        game_object.SetActive(true);
    }

    //-----------------------------------------------------
    //画面選択時の際に出現させる確認パネルを非表示に変更
    //-----------------------------------------------------
    public void offConfirmationPanel()
    {
        GameObject.Find("ConfirmationImage").SetActive(false);
        //gameObject.GetComponent<>
    }

    //-----------------------------------------------------
    //オブジェクト名取得
    //-----------------------------------------------------
    public void GetGameObjName()
    {

        this.transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log(this.gameObject.name);

            SceneName = this.gameObject.name;

            TextObject(SceneName);

            //Fader.instance.BlackOut();                        // フェードアウト
            //StartCoroutine(DelayMethod(1.2f, SceneName));		// 1.2秒後に実行する
        });
    }

    //-----------------------------------------------------
    //フェード使う際のScene遷移関数
    //-----------------------------------------------------
    private IEnumerator DelayMethod(float waitTime, string name)
    {
        yield return new WaitForSeconds(waitTime);          // waitTime後に実行する
        SceneManager.LoadScene(name.ToString());                    // シーン切り替え
    }

    public void TextObject(string name)
    {
        this.GetComponent<TextMesh>().text = name;
    }
}
