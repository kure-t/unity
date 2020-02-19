using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    InputField inputField;
    private string inputValue;
    private GameObject obj;
    private CsvReader csvreader;
    private int i, j, k, l, N;
    private string inputchangeValue;
    private bool success;

    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start()
    {

        inputField = GetComponent<InputField>();

        InitInputField();

        obj = GameObject.Find("Csvreader");//Csvreaderを探してくる
        csvreader = obj.GetComponent<CsvReader>();//CsvReaderのスクリプト読み込む
    }



    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>


    public void InputLogger()
    {

        inputValue = inputField.text;

        Debug.Log(inputValue);
        InputCSVread();
        InitInputField();
        
    }



    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>


    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }

    /// <summary>
    /// CSV読み込みメソッド
    /// Csvの格納
    /// </summary>
    
    void InputCSVread()
    {
        csvreader.CsvRead1("latin");
        latincheck();
        csvreader.CsvRead(inputchangeValue);
        //予備検討シーン呼び出し
        //Bの値に応じて移動するシーンを変える
        if (success)
        {
            switch (csvreader.csvDatas[0][4])
            {
                case "1":
                    SceneManager.LoadScene(1);
                    break;
                case "2":
                    SceneManager.LoadScene(10);
                    break;
            }
        }
    }
    
    void latincheck()
    {
        if (int.TryParse(inputValue, out N))
        {
            //1の位
            //ゲームを始めていく時は10~15とやる
            i = N % 10;
            //10の位
            j = (N / 10) % 10;
            //100の位
            k = (N / 100) % 10;
            l = k * 10 + j;
            success = true;
        }
        else
        {
            Debug.Log("string could not be parsed");
            success = false;
        }
        inputchangeValue = csvreader.csvDatas1[l-1][i];
        csvreader.participantsnumber = j.ToString();
    }
    
}
