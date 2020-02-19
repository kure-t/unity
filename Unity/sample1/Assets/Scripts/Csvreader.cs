using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Csvreader : MonoBehaviour {

    public string fileName; // 読み込むcsvの名前
    public string fileName1; // 読み込むcsvの名前
    private TextAsset csvFile; // CSVファイル
    private TextAsset csvFile1; // CSVファイル
    public List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト
    public List<string[]> csvDatas1 = new List<string[]>(); // CSVの中身を入れるリスト
    private int height = 0; // CSVの行数
    private int height1 = 0; // CSVの行数
    private bool checker = true;
    private bool checker1 = true;
    public string participantsnumber;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void CsvRead(string username)
    {
        if (checker == true)
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
            }
        }
    }
    public void CsvRead1(string username)
    {
        if (checker1 == true)
        {
            fileName1 = username; // filename
            csvFile1 = Resources.Load("CSV/" + fileName1) as TextAsset; /* Resouces/CSV下のCSV読み込み */
            if (csvFile1 != null)
            {
                StringReader reader1 = new StringReader(csvFile1.text);
                while (reader1.Peek() > -1)
                {
                    string line1 = reader1.ReadLine();
                    csvDatas1.Add(line1.Split(',')); // リストに入れる
                    height1++; // 行数加算
                }
                //中身確認
                for (int i = 0; i < csvDatas1.Count; i++)
                {
                    for (int j = 0; j < csvDatas1[i].Length; j++)
                    {
                        Debug.Log("csvDatas[" + i + "][" + j + "] = " + csvDatas1[i][j]);
                    }
                }
                Debug.Log(csvDatas1.Count);
                //再読み込み禁止
                checker1 = false;
            }
        }
    }

}