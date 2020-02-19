using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controll : MonoBehaviour {
    public GameObject obj1,obj2;
    public Transition script1;
    public Csvreader script2;
    float fall = 0;
    public static float fallspeed = 0.15f;
    public int f = 0;
    public static int c = 0;
    public int gameover = 0;

    // Use this for initialization
    void Start()
    {
        obj1 = GameObject.Find("Transition");
        obj2 = GameObject.Find("Csvreader");
        script1 = obj1.GetComponent<Transition>();
        script2 = obj2.GetComponent<Csvreader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (f == 0)
        {
            CheckUserInput();

        }

        else if (f == 1)
        {
            enabled = false;
            //throw new System.Exception("bfr loop");
            StartCoroutine(Coroutine());
          
            //if (gameover==1 &&FindObjectOfType<Game>().CheckIsAboveGrid(this))
            //{
            //    script1.timestageMesure();
            //    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

            //}
        }

        FindObjectOfType<Transition>().StageClear();
        
    }
    //void lateupdate()
    //{
    //    if (f == 1)
    //    {
    //        startcoroutine(coroutine());
    //    }
    //    debug.log(f);
    //    findobjectoftype<transition>().stageclear();
    //}

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().Updategrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().Updategrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallspeed)
        {
            transform.position += new Vector3(0, -1, 0);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().Updategrid(this);
            }
            else
            {

                f = 1;
                transform.position += new Vector3(0, 1, 0);
                //enabled = false;          
                // FindObjectOfType<Scenescript>().detatch();
                FindObjectOfType<Scenescript>().ChildrenCheck();
             
                //  StartCoroutine(Coroutine());
                //FindObjectOfType<Scenescript>().debug();
                //FindObjectOfType<Scenescript>().Label();
                // Debug.Log(Scenescript.z);
                // Debug.Log(f);
                //if (Scenescript.z == 1)
                // {
                //     throw new System.Exception("bfr loop");
                // }
                // // FindObjectOfType<Scenescript>().destroy();
                // while (Scenescript.z == 1)
                // {
                //     Debug.Log("c");
                //     FindObjectOfType<Scenescript>().ChildrenCheck();
                //     FindObjectOfType<Scenescript>().debug();
                //     FindObjectOfType<Scenescript>().Label();
                //     Debug.Log("b");
                //     // yield return null;
                // }

                //FindObjectOfType<Game>().Nextmino();

                //i++;
            }

            fall = Time.time;
        }
        else if (Input.GetKeyDown("space"))
        {
            transform.Rotate(0, 0, 90);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().Updategrid(this);
            }
            else
            {
                if (transform.position.x == 0)
                {
                    transform.position += new Vector3(1, 0, 0);
              
                }
                else if (transform.position.x == 5)
                {
                    transform.position += new Vector3(-1, 0, 0);

                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
                FindObjectOfType<Game>().Updategrid(this);
            }
        }

        //Debug.Log(Game.grid[0, 1]);
    }

    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            if (FindObjectOfType<Game>().CheckIsInsideGrid(pos) == false)
            {
                return false;
            }
            if (FindObjectOfType<Game>().Getgridpos(pos) != null && FindObjectOfType<Game>().Getgridpos(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator Coroutine()
    {
       
        enabled = false;
        do
        {
            gameover = 1;
            //if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
            //{

            //    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

            //}
            //FindObjectOfType<Scenescript>().detatch();
            //FindObjectOfType<Scenescript>().ChildrenCheck();
            yield return new WaitForSeconds(0.5f);
            FindObjectOfType<Scenescript>().debug();
            FindObjectOfType<Scenescript>().Label();
            if (c > 0 && Scenescript.z == 1)
            {
                script1.score = script1.score + (c + 1) * 10;
            }
            //Debug.Log(Scenescript.z);
            //yield return new WaitForSeconds(0.5f);
            //if (Scenescript.c > 1)
            //{
            //    yield return new WaitForSeconds(1.5f);
            //}
            
            yield return null;
            c++;
        } while (Scenescript.z == 1);
        if (Scenescript.z == 0 && FindObjectOfType<Game>().CheckIsAboveGrid(this))
        {
            Debug.Log("GAMEOVER");
            script1.timestageMesure();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            
        }
        c = 0;
        if(Game.num==4)
        {
            FindObjectOfType<Game>().Nextpuyo1();
        }
        else if(Game.num == 6)
        {
            FindObjectOfType<Game>().Nextpuyo2();
        }
        f = 0;
       
    }
}
