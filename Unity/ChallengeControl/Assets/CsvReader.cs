using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsvReader : MonoBehaviour
{

    public string fileName; // 読み込むcsvの名前
    private TextAsset csvFile; // CSVファイル
    public List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト
    private int height = 0; // CSVの行数
    private bool checker=true;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void CsvRead(string username)
    {
        if (checker==true)
        {
            fileName = username; // filename
            csvFile = Resources.Load("CSV/" + fileName) as TextAsset; /* Resouces/CSV下のCSV読み込み */
            if (csvFile != null)
            {
                StringReader reader = new StringReader(csvFile.text);
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    csvDatas.Add(line.Split(',')); // リストに入れる
                    height++; // 行数加算
                }
                //中身確認
                for (int i = 0; i < csvDatas.Count; i++)
                {
                    for (int j = 0; j < csvDatas[i].Length; j++)
                    {
                        Debug.Log("csvDatas[" + i + "][" + j + "] = " + csvDatas[i][j]);
                    }
                }
                Debug.Log(csvDatas.Count);
                //再読み込み禁止
                checker = false;
                //予備検討シーン呼び出し
                if (csvDatas[0].Length >= 5) {
                    switch (csvDatas[0][4])
                    {
                        case "1":
                            SceneManager.LoadScene(1);
                            break;
                        case "2":
                            SceneManager.LoadScene(5);
                            break;
                        case "3":
                            SceneManager.LoadScene(6);
                            break;
                    }
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

}
