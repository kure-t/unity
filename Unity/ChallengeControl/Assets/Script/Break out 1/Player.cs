using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    private float direction;
    public GameObject obj;
    public CsvReader script;
    public int paddlewidthsetting;
    public float width;
    private float positionfield;
    public float publicspeed;

    //位置座標
    private Vector3 position;
    private Vector3 speed;
    private Vector3 latestPos;
    GameObject player;
    //スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

	void Start () {
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.Find("Player2");
        //publicspeed = 100;
    }

	void Update () { 
        //速度取得
        if (Input.GetKey(KeyCode.A))
        {
            speed = (player.transform.position - latestPos) / Time.deltaTime;
            latestPos = player.transform.position;
            Debug.Log("速さ＝ " + speed);

        }
        
        //キーボード操作によるplayerの大きさ変更(デバック用)
        /*
        if (this.gameObject.transform.localScale.x < 35.0f)
        {
            if (Input.GetButton("Fire1"))
            {
                this.gameObject.transform.localScale = new Vector3(
                    this.gameObject.transform.localScale.x + 0.5f,
                    this.gameObject.transform.localScale.y,
                    this.gameObject.transform.localScale.z);
               // Debug.Log(b.bounds.size.x);
                /*
                CapsuleCollider b = new Vector3(
                    this.gameObject.transform.size.x + 0.5f,
                    this.gameObject.transform.size.y,
                    this.gameObject.transform.size.z);
                    *//*
            }
        }
        if (this.gameObject.transform.localScale.x > 0.5f)
        {
            if (Input.GetButton("Fire2"))
            {
                this.gameObject.transform.localScale = new Vector3(
                   this.gameObject.transform.localScale.x - 0.5f,
                   this.gameObject.transform.localScale.y,
                   this.gameObject.transform.localScale.z);
            }
        }*/

        //キーボード操作によるplayerの移動
        
        positionfield = (39 - width) / 2;
        direction = Input.GetAxis("Horizontal");
        transform.Translate(direction * publicspeed * Time.deltaTime, 0, 0);//ブロック加速タイプ
        //transform.Translate(0,direction * publicspeed * Time.deltaTime, 0);//カプセル加速タイプ
        //transform.position += new Vector3(direction * 100, 0, 0) * Time.deltaTime;//移動タイプ

        //枠外補正:for blocks
        
        if (rb.position.x > positionfield)
        {
            Debug.Log(1);
            transform.position = new Vector3(positionfield, rb.position.y, rb.position.z);
        }
        else if (rb.position.x < -1 * positionfield)
        {
            Debug.Log(2);
            transform.position = new Vector3(-1*positionfield, rb.position.y,rb.position.z);
        }
        /*
        //Outside the frame:for capsules
                if (rb.position.x > positionfield)
        {
            Debug.Log(1);
            transform.position = new Vector3(rb.position.x, positionfield, rb.position.z);
        }
        else if (rb.position.x < -1 * positionfield)
        {
            Debug.Log(2);
            transform.position = new Vector3(rb.position.x, -1*positionfield,rb.position.z);
        }*/
        
        /*
        //マウスによるplayerの移動
        
        positionfield = (39 - width) / 2;
        //Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        //Z軸修正
        position.z = 50f;
        //マウスの一座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        //枠外補正
        if (screenToWorldPointPosition.x > positionfield)
        {
            screenToWorldPointPosition.x = positionfield;
        }
        else if (screenToWorldPointPosition.x < -1 * positionfield)
        {
            screenToWorldPointPosition.x = -1 * positionfield;
        }
        //ワールド座標に変換されたマウス座標を代入
        rb.transform.position = new Vector3(screenToWorldPointPosition.x, 0, -10f);
        */
    }
    public void widthset()
    {
        this.gameObject.transform.localScale = new Vector3(
                    width,
                    this.gameObject.transform.localScale.y,
                    this.gameObject.transform.localScale.z);
    }
}