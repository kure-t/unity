using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controll : MonoBehaviour {
    public GameObject obj;
    public Score script;
    float fall = 0;
    public static float fallspeed = 0.1f;
    public int f = 0;
    public int c = 0;


    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("Main Camera");
        script = obj.GetComponent<Score>();
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
           // Debug.Log(f);
            //throw new System.Exception("bfr loop");
            StartCoroutine(Coroutine());
            if (Scenescript.z == 0&&FindObjectOfType<Game>().CheckIsAboveGrid(this))
            {

                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

            }
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
                transform.Rotate(0, 0, -90);
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
        do
        {
            enabled = false;
            //if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
            //{

            //    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

            //}
            //FindObjectOfType<Scenescript>().detatch();
            //FindObjectOfType<Scenescript>().ChildrenCheck();
            FindObjectOfType<Scenescript>().debug();
            FindObjectOfType<Scenescript>().Label();
         
            //Debug.Log(Scenescript.z);
            yield return new WaitForSeconds(0.5f);
            //if (Scenescript.c > 1)
            //{
            //    yield return new WaitForSeconds(1.5f);
            //}
            yield return null;

        } while (Scenescript.z == 1);

        c = 0;
        Scenescript.z = 0;
        FindObjectOfType<Game>().Nextpuyo2();
        f = 0;
       
    }
}
