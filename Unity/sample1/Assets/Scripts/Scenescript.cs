using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenescript : MonoBehaviour
{

    public GameObject obj,obj2;
    public Transition script;
    public Csvreader script2;
    GameObject[] blocks;
    float[] blockx = new float[100];
    float[] blocky = new float[100];
    public static int d = 4;
    public static float[,] prob1 = new float[d, d];
    int[] checks = new int[100];
    int[] samecolornums = new int[100];
    public int[] colorcheck1 = new int[10];
    public int[] colorcheck2 = new int[10];
    List<int> label = new List<int>();
    List<int> samecolorset = new List<int>();
    public static int gridwidth = 6;
    public static int gridheight = 20;
    List<int> positionx = new List<int>();
    public static int z = 0;
    
    public int m = 0;

    // Use this for initialization
 
    void Start()
    {
        obj = GameObject.Find("Transition");
        script = obj.GetComponent<Transition>();
        obj2= GameObject.Find("Csvreader");
        script2 = obj2.GetComponent<Csvreader>();
        d = int.Parse(script2.csvDatas[0][1]);
    }
    
    public void debug()
    {
        
        z = 0;
        this.blocks = GameObject.FindGameObjectsWithTag("block");
        int i = 0;
        this.label.Clear();
        foreach (GameObject block in this.blocks)
        { 
            //checks[i]：i番ぷよの確認作業終了フラグ…0：未完了、1：完了
            this.checks[i] = 0;
            //samecolornums[i]：i番ぷよと隣り合っている同色ぷよの数、基本は1（自分自身）
            this.samecolornums[i] = 1;
            //puyox[i],puyoy[i]：i番ぷよの位置座標（丸め誤差対策済）
            this.blockx[i] = Mathf.RoundToInt(block.transform.position.x * 10.0f) / 10.0f;
            this.blocky[i] = Mathf.RoundToInt(block.transform.position.y * 10.0f) / 10.0f;
            i++;
        }

        i = 0;
        foreach (GameObject block in this.blocks)
        {
            if (this.checks[i] == 0)
            {
                this.samecolorset.Clear();
                Check(i);
                for (int k = 0; k < this.samecolorset.Count; k++)
                {
                    //i番ぷよと隣接同色ぷよのsamecolornumsにカウント結果を代入
                    this.samecolornums[this.samecolorset[k]] = this.samecolorset.Count;
                }
            }
    
            else
            {
                for (int k = 0; k < this.samecolorset.Count; k++)
                {
                    //i番ぷよと隣接同色ぷよのsamecolornumsにカウント結果を代入
                    this.samecolornums[this.samecolorset[k]] = this.samecolorset.Count;
                }
            }

           
            i++;
        }

        this.label.Clear();
        this.positionx.Clear();
        m = 0;
        i = 0;
        foreach (GameObject block in this.blocks)
        {

            if (this.samecolornums[i] >= 4)
            {

                for (int j = 0; j < this.blocks.Length; j++)
                {
                    //上のぷよjを取得
                    if (this.blockx[i] == this.blockx[j] && this.blocky[i] == this.blocky[j] - 1.0f && this.samecolornums[j] < 4)
                    {
                        
                        this.label.Add(j);
                        
                        //Debug.Log(j);
                    }

                }
                Destroy(block);
                Game.grid[(int)this.blockx[i], (int)this.blocky[i]] = null;
                z = 1;
                m++;
    

            }
            if (this.samecolornums[i] == 3)
            {
                if (Game.num == 4)
                {
                    if (Upcheck(i) || Sidecheck(i))
                    {
                        //   Debug.Log(i);
                        switch (this.blocks[i].transform.name)
                        {
                            case "violet":
                                colorcheck1[0] = colorcheck1[0] + 1;
                                // Debug.Log(colorcheck[0]);
                                break;

                            case "green":
                                colorcheck1[1] = colorcheck1[1] + 1;

                                break;

                            case "yellow":
                                colorcheck1[2] = colorcheck1[2] + 1;
                                // Debug.Log(colorcheck[2]);
                                break;

                            case "red":
                                colorcheck1[3] = colorcheck1[3] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                        }
                    }
                }
                else if (Game.num == 6)
                {
                    if (Upcheck(i) || Sidecheck(i))
                    {
                        //   Debug.Log(i);
                        switch (this.blocks[i].transform.name)
                        {
                            case "violet":
                                colorcheck1[0] = colorcheck1[0] + 1;
                                // Debug.Log(colorcheck[0]);
                                break;

                            case "green":
                                colorcheck1[1] = colorcheck1[1] + 1;

                                break;

                            case "yellow":
                                colorcheck1[2] = colorcheck1[2] + 1;
                                // Debug.Log(colorcheck[2]);
                                break;

                            case "red":
                                colorcheck1[3] = colorcheck1[3] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                            case "white":
                                colorcheck1[4] = colorcheck1[4] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                            case "blue":
                                colorcheck1[5] = colorcheck1[5] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                        }
                    }
                }
               
            }
            if (this.samecolornums[i] == 2)
            {
                if (Game.num == 4)
                {
                    if (Upcheck(i) || Sidecheck(i))
                    {
                        switch (this.blocks[i].transform.name)
                        {
                            case "violet":
                                colorcheck2[0] = colorcheck2[0] + 1;
                                // Debug.Log(colorcheck[0]);
                                break;

                            case "green":
                                colorcheck2[1] = colorcheck2[1] + 1;

                                break;

                            case "yellow":
                                colorcheck2[2] = colorcheck2[2] + 1;
                                // Debug.Log(colorcheck[2]);
                                break;

                            case "red":
                                colorcheck2[3] = colorcheck2[3] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                        }
                    }
                }
                else if (Game.num == 6)
                {
                    if (Upcheck(i) || Sidecheck(i))
                    {
                        switch (this.blocks[i].transform.name)
                        {
                            case "violet":
                                colorcheck2[0] = colorcheck2[0] + 1;
                                // Debug.Log(colorcheck[0]);
                                break;

                            case "green":
                                colorcheck2[1] = colorcheck2[1] + 1;

                                break;

                            case "yellow":
                                colorcheck2[2] = colorcheck2[2] + 1;
                                // Debug.Log(colorcheck[2]);
                                break;

                            case "red":
                                colorcheck2[3] = colorcheck2[3] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                            case "white":
                                colorcheck2[4] = colorcheck2[4] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                            case "blue":
                                colorcheck2[5] = colorcheck2[5] + 1;
                                // Debug.Log(colorcheck[3]);
                                break;
                        }
                    }
                }
                
            }
            i++;
        }
       
        script.number = script.number + m;
        script.score = script.score + m * 10;
        if (Game.num==4)
        {
            if (Game.lack==1)
            {
                probability1();
            }
            else if (Game.lack == 2)
            {
                probability2();
            }
            else if (Game.lack == 3)
            {
                probability3();
            }
        }
        else if (Game.num == 6)
        {
            if (Game.lack == 1)
            {
                probability4();
            }
            else if (Game.lack == 2)
            {
                probability5();
            }
            else if (Game.lack == 3)
            {
                probability6();
            }
        }
        //if (c > 1)
        //{
        //    yield return new WaitForSeconds(1.5f);
        //}
        //Debug.Log(z);
        //for (int j = 0; j < this.label.Count; j++)
        //{
        //    Getnullnum(this.label[j]);
        //}

            //  ChildrenCheck();


            //for (int k = 0; k < blocks.Length; k++)
            //{
            //    Debug.Log(blocks.Length);
            //}

    }
    public void Label()
    {
        for (int j = 0; j < this.label.Count; j++)
        {
            Getnullnum(this.label[j]);
        }
    }
    public void ChildrenCheck()
    {
        this.blocks = GameObject.FindGameObjectsWithTag("block");
        int i = 0;
        foreach (GameObject block in this.blocks)
        {
            this.blockx[i] = Mathf.RoundToInt(block.transform.position.x * 10.0f) / 10.0f;
            this.blocky[i] = Mathf.RoundToInt(block.transform.position.y * 10.0f) / 10.0f;

            float v = 0;
            float w = 0;
            v = this.blockx[i];
            w = this.blocky[i];
            //int ObjCount = gameObject.transform.childCount;
            int n = 0;
            // if (blocks[i].transform.parent == null)
            // {

            for (int k = 0; w - k > 0; ++k)
            {
                // j = (int)w - k - 1;
                if (Game.grid[(int)v, (int)w - k - 1] != null)
                {
                    break;
                }
                n = k + 1;
            }
            //Debug.Log(n);

            for (int y = (int)w; y < gridheight; ++y)
            {
                if (Game.grid[(int)v, y] != null && n != 0)
                {
                    Game.grid[(int)v, y - n] = Game.grid[(int)v, y];
                    Game.grid[(int)v, y] = null;
                    Game.grid[(int)v, y - n].position += new Vector3(0, -n, 0);

                }
            }
            //}
            i++;
        }
    }

    public void Check(int i)
    {
        //this.blocks = GameObject.FindGameObjectsWithTag("block");
        //int m = 0;
        //foreach (GameObject block in this.blocks)
        //{
        //    //丸め誤差解消
        //    this.blockx[m] = Mathf.RoundToInt(block.transform.position.x * 10.0f) / 10.0f;
        //    this.blocky[m] = Mathf.RoundToInt(block.transform.position.y * 10.0f) / 10.0f;
        //    m++;
        // }

        this.samecolorset.Add(i);
        //元からchecks[i]=1ならi番ぷよは調査済なので確認しない
        if (this.checks[i] == 1) return;
        this.checks[i] = 1;
        for (int j = 0; j < this.blocks.Length; j++)
        {
            if (this.blockx[i] == this.blockx[j] && this.blocky[i] == this.blocky[j] + 1.0f && this.blocks[i].transform.name == this.blocks[j].transform.name && this.checks[j] == 0)
            {
                // 下と自分自身が同色
                Check(j);
            }
            if (this.blockx[i] == this.blockx[j] && this.blocky[i] == this.blocky[j] - 1.0f && this.blocks[i].transform.name == this.blocks[j].transform.name && this.checks[j] == 0)
            {
                // 上と自分自身が同色
                Check(j);
            }
            if (this.blockx[i] == this.blockx[j] + 1.0f && this.blocky[i] == this.blocky[j] && this.blocks[i].transform.name == this.blocks[j].transform.name && this.checks[j] == 0)
            {
                // 左と自分自身が同色
                Check(j);
            }
            if (this.blockx[i] == this.blockx[j] - 1.0f && this.blocky[i] == this.blocky[j] && this.blocks[i].transform.name == this.blocks[j].transform.name && this.checks[j] == 0)
            {
                // 右と自分自身が同色
                Check(j);
            }
        }
        return;
    }

    //public void destroy()
    //{
    //    int i = 0;
    //   
    //    foreach (GameObject block in this.blocks)
    //    {
    //        if (this.samecolornums[i] >= 4)
    //        {
    //           Destroy(block);
    //     }
    //    Check(i);
    //   Debug.Log(this.samecolornums[i]);
    // }
    // i++;
    // }



    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void detatch()
    {
        gameObject.transform.DetachChildren();
        Destroy(gameObject);
    }

    public void Getnullnum(int i)
    {
        int n = 0;
        int num = 0;
        float v = 0;
        float w = 0;
        v = this.blockx[i];
        w = this.blocky[i];
        // Debug.Log(v);
        for (int y = (int)w; y < gridheight; y++) {
            if (Game.grid[(int)v, y-num] == null)
            {
                continue;
            }
            int t = y - num;
            num = 0;
            for (int k = 0; t - k > 0; k++)
            {
                if (Game.grid[(int)v, (int)t - k - 1] == null)
                {
                    num++;
                }
                else
                {
                    break;
                }
                n = num;
            }
            Debug.Log(num);
            Debug.Log(n);
            if (num != 0)
            {
                for (int q = (int)t; q < gridheight-1; ++q)
                {
                    if (Game.grid[(int)v, q] != null)
                    {
                        Game.grid[(int)v, q - n] = Game.grid[(int)v, q];
                        Game.grid[(int)v, q] = null;
                        if (Game.grid[(int)v, q - n] != null)
                        {
                            Game.grid[(int)v, q - n].position += new Vector3(0, -n, 0);
                        }

                        Debug.Log(Game.grid[(int)v, q - n]);
                    }
                }
            }

        }
    }

    public void firstfield()
    {
        foreach (Transform block in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(block.position);
            if (pos.y < gridheight)
            {
                Game.grid[(int)pos.x, (int)pos.y] = block;
            }
        }
        // Debug.Log(Game.grid[0, 1]);
    }

    public bool Upcheck(int i)
    {
        float v = 0;
        float w = 0;
        v = this.blockx[i];
        w = this.blocky[i];
        if (Game.grid[(int)v, (int)w + 1] == null)
        {
           return true;
        }
        
       
        return false;
    }
    public bool Sidecheck(int i)
    {
        float v = 0;
        float w = 0;
        int n = 0;
        v = this.blockx[i];
        w = this.blocky[i];
    
        if ((int)v == 0)
        {
            if (Game.grid[(int)v + 1, (int)w] == null)
            {
                for (int k = 0; w - k > -1; ++k)
                {
                    if (Game.grid[(int)v + 1, (int)w - k] != null)
                    {
                        break;
                    }
                    n = k;
                }
                if (n < 2)
                {
                    return true;
                }
            }
          
        }
        else if ((int)v == gridwidth-1)
        {
            if (Game.grid[(int)v - 1, (int)w] == null)
            {
                for (int k = 0; w - k > -1; ++k)
                {
                    if (Game.grid[(int)v - 1, (int)w - k] != null)
                    {
                        break;
                    }
                    n = k;
                }
                if (n < 2)
                {
                    return true;
                }
            }
            return false;

        }
        else 
        {
            if (Game.grid[(int)v + 1, (int)w] == null)
            {
                for (int k = 0; w - k > -1; ++k)
                {
                    if (Game.grid[(int)v + 1, (int)w - k] != null)
                    {
                        break;
                    }
                    n = k;
                }
                if (n < 2)
                {
                    return true;
                }
            }
            if (Game.grid[(int)v - 1, (int)w] == null)
            {
                for (int k = 0; w - k > -1; ++k)
                {
                    if (Game.grid[(int)v - 1, (int)w - k] != null)
                    {
                        break;
                    }
                    n = k;
                }
                if (n < 2)
                {
                    return true;
                }
            }
        }
        return false;

    }
   

    //運悪い
    public void probability1()
    {
        List<int> thr = new List<int>();
        List<int> two = new List<int>();
        int a1 = 0;
        int a2 = 0;
        int sum = 0;

        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for(int m=0; m< prob1.GetLength(1); m++)
            {
                prob1[n, m] = 0;
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck1[k] != 0)
            {
                a1++;
                thr.Add(k);
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck2[k] != 0)
            {
                a2++;
                two.Add(k);
            }
        }
        switch (a1)
        {
            case 0:
                
                break;
            case 1:
                for (int n = 0; n < prob1.GetLength(1); n++)
                {
                    prob1[thr[0],n]=1;
                }
                break;

            case 2:
                for (int m = 0;m <thr.Count;m++)
                {
                    for (int n =0;n< prob1.GetLength(1); n++)
                    {
                        prob1[thr[m],n] = 1;
                       
                    }
                }
                sum = sum - 1;
                break;
           
            case 3:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
                sum = sum - 3;
                break;
        }
        switch (a2)
        {
         
            case 1:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
            case 2:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;  
                }
                break;
            case 3:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
        }
     
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                sum=sum+(int)prob1[n,m];
            }
        }
        
        Debug.Log("sum="+sum);
        //Debug.Log(prob1[1, 0]);


        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (prob1[n, m] > prob1[m, n])
                {
                    prob1[m, n] = prob1[n, m];
                }
            }
        }

        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (sum == 0||sum==10)
                {
                    prob1[n, m] = 100f/10f;
                }
                else
                {
                    if (sum <3)
                    {
                        if (prob1[n, m] == 1)
                        {
                            prob1[n, m] = 0f / sum;
                        }
                        else
                        {
                            prob1[n, m] = 100f / (10f - sum);
                        }
                    }
                    else
                    if (prob1[n, m] == 1)
                    {
                        prob1[n, m] = 10f / sum;
                    }
                    else
                    {
                        prob1[n, m] = 90f / (10f - sum);
                    }
                   
                }
               
            }
        }
        //for(int n = 0; n < 4; n++)
        //{
        //    Debug.Log(prob1[0, n]);
        //}
       
    }

    //運良い
    public void probability3()
    {
        List<int> thr = new List<int>();
        List<int> two = new List<int>();
        int a1 = 0;
        int a2 = 0;
        int sum = 0;

        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                prob1[n, m] = 0;
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck1[k] != 0)
            {
                a1++;
                thr.Add(k);
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck2[k] != 0)
            {
                a2++;
                two.Add(k);
            }
        }
        switch (a1)
        {
            case 0:

                break;
            case 1:
                for (int n = 0; n < prob1.GetLength(0); n++)
                {
                    prob1[n, thr[0]] = 1;
                }
                break;

            case 2:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(0); n++)
                    {
                        prob1[n, thr[m]] = 1;

                    }
                }
             //   sum = sum - 1;
                break;

            case 3:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(0); n++)
                    {
                        prob1[n, thr[m]] = 1;
                    }
                }
             //   sum = sum - 3;
                break;
        }
        if (a2 != 0)
        {
            for (int m = 0; m < two.Count; m++)
            {
                for (int n = 0; n < prob1.GetLength(0); n++)
                {
                    prob1[n, two[m]] = 1;
                }

            }
        }


        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (prob1[n, m] > prob1[m, n])
                {
                    prob1[m, n] = prob1[n, m];
                }
            }
        }

        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < n+1; m++)
            {
                sum = sum + (int)prob1[n, m];
            }
        }

        Debug.Log("sum=" + sum);
        //Debug.Log(prob1[1, 0]);
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (sum == 0)
                {
                    prob1[n, m] = 100f / 10f;
                }
                else
                {
                    if (prob1[n, m] == 1)
                    {
                        prob1[n, m] = 75f / sum;
                    }
                    else
                    {
                        prob1[n, m] = 25f / (10f - sum);
                    }
                  
                }
         
            }
        }
    }

    //運ふつう
    public void probability2()
    {
        int sum = 0;
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                prob1[n, m] = 1;
                sum = sum + (int)prob1[n, m];
            }
        }
        Debug.Log("sum=" + sum);

    }
    
    //運悪い
    public void probability4()
    {
        List<int> thr = new List<int>();
        List<int> two = new List<int>();
        int a1 = 0;
        int a2 = 0;
        int sum = 0;
        prob1 = new float[d, d];
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                prob1[n, m] = 0;
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck1[k] != 0)
            {
                a1++;
                thr.Add(k);
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck2[k] != 0)
            {
                a2++;
                two.Add(k);
            }
        }
        switch (a1)
        {
            case 0:

                break;
            case 1:
                for (int n = 0; n < prob1.GetLength(1); n++)
                {
                    prob1[thr[0], n] = 1;
                }
                break;

            case 2:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
                sum = sum - 1;
                break;

            case 3:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
                sum = sum - 3;
                break;
            case 4:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
                sum = sum - 6;
                break;
            case 5:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
                sum = sum - 10;
                break;
        }
        switch (a2)
        {

            case 1:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
            case 2:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
            case 3:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
            case 4:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
            case 5:
                for (int n = 0; n < two.Count; n++)
                {
                    prob1[two[n], two[n]] = 1;
                }
                break;
        }

        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                sum = sum + (int)prob1[n, m];
                Debug.Log(prob1[n,m]);
            }
        }

        Debug.Log("sum=" + sum);
        //Debug.Log(prob1[1, 0]);


        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (prob1[n, m] > prob1[m, n])
                {
                    prob1[m, n] = prob1[n, m];
                }
            }
        }

        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (sum == 0 || sum == 21)
                {
                    prob1[n, m] = 100f / 10f;
                }
                else
                {
                    if (sum < 6)
                    {
                        if (prob1[n, m] == 1)
                        {
                            prob1[n, m] = 0f / sum;
                        }
                        else
                        {
                            prob1[n, m] = 100f / (21f - sum);
                        }

                    }
                    else
                    {
                        if (prob1[n, m] == 1)
                        {
                            prob1[n, m] = 10f / sum;
                        }
                        else
                        {
                            prob1[n, m] = 90f / (21f - sum);
                        }

                    }
                }
            }
        }
        Debug.Log(prob1[0, 0]);

    }

    //運良い
    public void probability6()
    {
        List<int> thr = new List<int>();
        List<int> two = new List<int>();
        int a1 = 0;
        int a2 = 0;
        int sum = 0;
        prob1 = new float[d, d];
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                prob1[n, m] = 0;
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck1[k] != 0)
            {
                a1++;
                thr.Add(k);
            }
        }
        for (int k = 0; k < d; k++)
        {
            if (colorcheck2[k] != 0)
            {
                a2++;
                two.Add(k);
            }
        }
        switch (a1)
        {
            case 0:

                break;
            case 1:
                for (int n = 0; n < prob1.GetLength(1); n++)
                {
                    prob1[thr[0], n] = 1;
                }
                break;

            case 2:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
              //  sum = sum - 1;
                break;

            case 3:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
              //  sum = sum - 3;
                break;
            case 4:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
             //   sum = sum - 6;
                break;
            case 5:
                for (int m = 0; m < thr.Count; m++)
                {
                    for (int n = 0; n < prob1.GetLength(1); n++)
                    {
                        prob1[thr[m], n] = 1;

                    }
                }
             //   sum = sum - 10;
                break;
        }
        if (a2 != 0)
        {
            for (int m = 0; m < two.Count; m++)
            {
                for (int n = 0; n < prob1.GetLength(0); n++)
                {
                    prob1[n, two[m]] = 1;
                }

            }
        }


        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (prob1[n, m] > prob1[m, n])
                {
                    prob1[m, n] = prob1[n, m];
                  
                }
            }
        }
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < n + 1; m++)
            {
                sum = sum + (int)prob1[n, m];
            }
        }

        Debug.Log("sum=" + sum);

        //Debug.Log(prob1[1, 0]);
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                if (sum == 0 || sum == 21)
                {
                    prob1[n, m] = 100f / 10f;
                }
              
                
                else
                {
                    if (prob1[n, m] == 1)
                    {
                        
                        prob1[n, m] = 75f / sum;
                    }
                    else
                    {
                        prob1[n, m] = 25f / (21f - sum);
                    }

                }
            }
           
        }
    }
    //運ふつう
    public void probability5()
    {
        prob1 = new float[d, d];
        int sum = 0;
        for (int n = 0; n < prob1.GetLength(0); n++)
        {
            for (int m = 0; m < prob1.GetLength(1); m++)
            {
                prob1[n, m] = 1;
                sum = sum + (int)prob1[n, m];
                Debug.Log(prob1[n, m]);
            }
        }

    }
}
