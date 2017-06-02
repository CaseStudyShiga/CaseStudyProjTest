using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// LoadSceneを使うために必要！！！！！
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonSystem : MonoBehaviour
{
    static GameObject[] stage = new GameObject[11];

    //------ 変数の生成 ------
    public static string SceneName;

    //// Use this for initialization
    void Start () {

        //------ クリックした際にオブジェクト名取得 ------
        GetGameObjName();
        //------ データを配列に書き込み ------
        GetStageData();
        //------ オブジェクトの変形アクション ------
        OpenButtonAction();
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
        CSVDataReader.Instance.Load(SceneName);			// 1.2秒後に実行する
        StartCoroutine(DelayMethod(1.2f, "Game"));				// 1.2秒後に実行する
    }

    //-----------------------------------------------------
    //画面選択時の際に出現させる確認パネルを表示
    //-----------------------------------------------------
    public void onConfirmationPanel()
    {
        //W:450 H:795

        GameObject ConfirmationPanel = GameObject.Find("ConfirmationPanel");

        ConfirmationPanel.transform.localScale = new Vector3(0, 0, 0);

        ConfirmationPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);

        GameObject game_object = ConfirmationPanel.transform.FindChild("ConfirmationImage").gameObject;

        game_object.SetActive(true);
    }

    //-----------------------------------------------------
    //画面選択時の際に出現させる確認パネルを非表示に変更
    //-----------------------------------------------------
    public void offConfirmationPanel()
    {
        GameObject ConfirmationPanel = GameObject.Find("ConfirmationPanel");

        ConfirmationPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
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

        });
    }

    //-----------------------------------------------------
    //------ データ入れ込み -------
    //-----------------------------------------------------
    public void GetStageData()
    {
        int idx = this.gameObject.name.LastIndexOf("e") + 1;
        string n = this.gameObject.name.Substring(idx);
        stage[int.Parse(n)] = this.gameObject;

    }

    //-----------------------------------------------------
    //オブジェクトのアクション（動き）
    //-----------------------------------------------------
     void OpenButtonAction()
    {
        this.transform.localScale = new Vector3(0, 0, 0);

        transform.DOScale(new Vector3(2.5f, 5.7f, 1.8f), 1f);
    }

    //-----------------------------------------------------
    //フェード使う際のScene遷移関数
    //-----------------------------------------------------
    private IEnumerator DelayMethod(float waitTime , string name)
    {
        yield return new WaitForSeconds(waitTime);          // waitTime後に実行する
        SceneManager.LoadScene(name.ToString());                    // シーン切り替え

    }

    private IEnumerator DelayMethod(float waitTime, System.Action ac)
    {
        yield return new WaitForSeconds(waitTime);      // waitTime後に実行する
        ac();
        SceneManager.LoadScene("Game");                 // シーン切り替え
    }
}
