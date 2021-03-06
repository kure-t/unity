﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static int gridwidth = 6;
    public static int gridheight = 20;
    public static Transform[,] grid = new Transform[gridwidth, gridheight+1];
    float[] blockx = new float[100];
    float[] blocky = new float[100];
    GameObject[] blocks;
    public static int num = 4;
    public static int lack = 1;
    Dictionary<int, string> randompuyo;
    public static float[] puyodict;
    public float[] v= new float[6];
    public GameObject obj1, obj2;
    public Transition script1;
    public Csvreader script2;
    // Use this for initialization
    void Start()
    {
        obj1 = GameObject.Find("Transition");
        obj2 = GameObject.Find("Csvreader");
        script1 = obj1.GetComponent<Transition>();
        script2 = obj2.GetComponent<Csvreader>();
        if (script2.csvDatas[0][2] == "1")
        {
            lack = 1;
        }
        else if (script2.csvDatas[0][2] == "2")
        {
            lack = 2;
        }
        else if (script2.csvDatas[0][2] == "3")
        {
            lack = 3;
        }
        if (script2.csvDatas[0][1] == "4")
        {
            num = 4;
        }
        else if (script2.csvDatas[0][1] == "6")
        {
            num = 6;
        }
        Nextmino();
        Debug.Log(lack);
    }



    //積んでいく　グリッドアップデート
    public void Updategrid(Controll tetromino)
    {
        for (int y = 0; y < gridheight; ++y)
        {
            for (int x = 0; x < gridwidth; ++x)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform mino in tetromino.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < gridheight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
        // FindObjectOfType<field>().firstfield();
    }

    //座標取得
    public Transform Getgridpos(Vector2 pos)
    {
        if (pos.y > gridheight-1 )
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public bool CheckIsAboveGrid(Controll tetromino)
    {
        for (int x = 0; x < gridwidth; ++x)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if (pos.y > 8)
                {
                    return true;
                    
                }

            }
        }
        return false;

    }

    //ランダムに新しいブロックを出す
    public void Nextmino()
    {
        GameObject nextmino = (GameObject)Instantiate(Resources.Load(GetRandom(), typeof(GameObject)), new Vector2(3.0f, 15.0f), Quaternion.identity);
        
    }
    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridwidth && (int)pos.y >= 0);
    }

    public bool IsFullRowAt(int y)
    {
        for (int x = 0; x < gridwidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void Delete(int y)
    {
        for (int x = 0; x < gridwidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandom()
    {
        int randommino = Random.Range(1, 11);
        string randomminonName = "Prefab/block1";
        switch (randommino)
        {
            case 1:
                randomminonName = "Prefab/block1";
                break;
            case 2:
                randomminonName = "Prefab/block2";
                break;
            case 3:
                randomminonName = "Prefab/block3";
                break;
            case 4:
                randomminonName = "Prefab/block4";
                break;
            case 5:
                randomminonName = "Prefab/block5";
                break;
            case 6:
                randomminonName = "Prefab/yellow";
                break;
            case 7:
                randomminonName = "Prefab/block6";
                break;
            case 8:
                randomminonName = "Prefab/block7";
                break;
            case 9:
                randomminonName = "Prefab/block8";
                break;
            case 10:
                randomminonName = "Prefab/block9";
                break;

        }
        return randomminonName;
    }
    public void Nextpuyo1()
    {
        Getdict1();
        string nextpuyo = randompuyo[Choose()];
        GameObject Nextpuyo = (GameObject)Instantiate(Resources.Load(nextpuyo, typeof(GameObject)), new Vector2(3.0f, 15.0f), Quaternion.identity);
    }
    public void Nextpuyo2()
    {
        
        Getdict2();
    
        string nextpuyo = randompuyo[Choose()];
        GameObject Nextpuyo = (GameObject)Instantiate(Resources.Load(nextpuyo, typeof(GameObject)), new Vector2(3.0f, 15.0f), Quaternion.identity);
    }


    void Getdict1()
    {
        randompuyo = new Dictionary<int, string>();
        randompuyo.Add(0, "Prefab/block4");
        randompuyo.Add(1, "Prefab/block1");
        randompuyo.Add(2, "Prefab/block3");
        randompuyo.Add(3, "Prefab/block6");
        randompuyo.Add(4, "Prefab/block9");
        randompuyo.Add(5, "Prefab/block2");
        randompuyo.Add(6, "Prefab/block5");
        randompuyo.Add(7, "Prefab/yellow");
        randompuyo.Add(8, "Prefab/block7");
        randompuyo.Add(9, "Prefab/block8");
       
        puyodict = new float[10];

       for(int i =0; i<4; i++)
        {
            puyodict[i] = Scenescript.prob1[i, 0];
        }
       for(int i=4; i < 7; i++)
        {
            puyodict[i] = Scenescript.prob1[i - 3, 1];
        }
       for(int i=7; i< 9; i++)
        {
            puyodict[i] = Scenescript.prob1[i - 5, 2];
        }
        puyodict[9] = Scenescript.prob1[3, 3];
    }

    void Getdict2()
    {
        randompuyo = new Dictionary<int, string>();
        randompuyo.Add(0, "Prefab/block4");
        randompuyo.Add(1, "Prefab/block1");
        randompuyo.Add(2, "Prefab/block3");
        randompuyo.Add(3, "Prefab/block6");
        randompuyo.Add(4, "Prefab/block11");
        randompuyo.Add(5, "Prefab/block16");
        randompuyo.Add(6, "Prefab/block9");
        randompuyo.Add(7, "Prefab/block2");
        randompuyo.Add(8, "Prefab/block5");
        randompuyo.Add(9, "Prefab/block10");
        randompuyo.Add(10, "Prefab/block15");
        randompuyo.Add(11, "Prefab/yellow");
        randompuyo.Add(12, "Prefab/block7");
        randompuyo.Add(13, "Prefab/block20");
        randompuyo.Add(14, "Prefab/block17");
        randompuyo.Add(15, "Prefab/block8");
        randompuyo.Add(16, "Prefab/block12");
        randompuyo.Add(17, "Prefab/block18");
        randompuyo.Add(18, "Prefab/block14");
        randompuyo.Add(19, "Prefab/block13");
        randompuyo.Add(20, "Prefab/block19");
     
        puyodict = new float[21];

        for (int i = 0; i < 6; i++)
        {
            puyodict[i] = Scenescript.prob1[i, 0];
        }
        for (int i = 6; i < 11; i++)
        {
            puyodict[i] = Scenescript.prob1[i - 5, 1];
        }
        for (int i = 11; i < 15; i++)
        {
            puyodict[i] = Scenescript.prob1[i - 9, 2];
        }
        for (int i = 15; i < 18; i++)
        {
            puyodict[i] = Scenescript.prob1[i - 12, 3];
        }
        for (int i = 18; i < 20; i++)
        {
            puyodict[i] = Scenescript.prob1[i - 14, 4];
        }
        puyodict[20] = Scenescript.prob1[5, 5];
    }

    int Choose()
    {
        float total = 0;
        foreach(float elem in puyodict)
        {
            total += elem;
        }
        float random = Random.value*total;
        for(int i=0; i<puyodict.Length;i++)
        {
            if(random< puyodict[i])
            {
                return i;
            }
            else
            {
                random -= puyodict[i];
            }
        }
        return 0;
    }
}


