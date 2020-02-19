using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    InputField inputField;
    private string inputValue;
    private GameObject obj;
    private CsvReader csvreader;

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
        csvreader.CsvRead(inputValue);
    }
}
