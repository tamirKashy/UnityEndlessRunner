using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{
    static public PlayerManager Instance { get; private set;}
    private ObsteclesManager ObsManager;
    
    public Rigidbody rb;
    // Start is called before the first frame update
    private bool moveleft, moveright, isCollision = false;
    public float Forwardforce = 0;
    public float Sideforce = 100f;
    public GameObject gameOverly,gameoverOverly;
    private Vector3 spawnPoint;
    private overlymanager overlyManager;
    public int CurrentScore { get; set; }
    public TextMeshProUGUI ScoreText, Timer;
    void Start()
    {
        ObsManager = FindObjectOfType<ObsteclesManager>();
        Instance = this;
        rb = GetComponent<Rigidbody>();
        ScoreText = gameOverly.GetComponentInChildren<TextMeshProUGUI>();
        spawnPoint = rb.transform.position;
        CurrentScore = 0;
        overlyManager = FindObjectOfType<overlymanager>();
        overlyManager.TimerReset();
    }

   /* private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle") gameOver(1);
            
    }*/
    // Update is called once per frame
    private void Update()
    {
        if(rb.transform.position.y > 10 || rb.transform.position.y <= 0.5) gameOver(1);
        if (Input.GetKey("d")) moveright = true;
        else moveright = false;
        if (Input.GetKey("a")) moveleft = true;
        else moveleft = false;
        //updateScore();
    }

    void FixedUpdate()
    {
        if (!isCollision)
        {
            //rb.AddForce(0, 0, Forwardforce * Time.deltaTime,ForceMode.VelocityChange);
            if (moveright)
            {
                rb.AddForce(Sideforce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
            }
            else if (moveleft)
            {
                rb.AddForce(-Sideforce * Time.deltaTime, 0, -Sideforce*0.1f* Time.deltaTime,ForceMode.VelocityChange);
            }
        }
    }
    //functions
    public void retry()
    {
        ObsManager.retry();//ObstacleManager.Instance.myRetry = true;
        rb.position = spawnPoint; 
        rb.transform.Rotate(0,0,0);
        rb.velocity = Vector3.zero;
        isCollision = false; 
        Debug.Log("Player is retring level");
        gameoverOverly.gameObject.SetActive(false);
        gameOverly.gameObject.SetActive(true);
        CurrentScore = 0;
        overlyManager.TimerReset();
        //updateScore();
    }

    private void setVector(Vector3 V,float x, float y, float z)
    {
        V.x = x;
        V.y = y;
        V.z = z;
    }
    private Vector3 getVector(float x, float y, float z)
    {
        Vector3 V;
        V.x = x;
        V.y = y;
        V.z = z;
        return V;
    }

    private void gameOver(int type) // 1 == Obstacle
    {
        if (type == 1)
        {
            isCollision = true; 
            //Debug.Log("Collision entered with Obstacle");
            gameoverOverly.gameObject.SetActive(true);
        }
        gameOverly.gameObject.SetActive(false);
    }

    public void addScore(int i)
    {
        CurrentScore += i;
    }

    public void Lost()
    {
        gameOver(1);
    }
    
}
