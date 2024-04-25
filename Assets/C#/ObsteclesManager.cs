using System;
using TMPro;
using UnityEditorInternal;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class ObsteclesManager : MonoBehaviour
{
    //Managers
    private PlayerManager pm;

    private ObstacleStandalone[] obs;
    //vars
    private Vector3[] SpawnList;
    Random rand = new Random();
    Stack Queue = new Stack();
    //static vars
    public bool Gameover = false;
    public float MovmentSpeed;
    public int MaxObsticlesAlive = 1;
    public int AliveObsticles = 0;
    public int ObstacleCount=0;
    //functions
    private void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if(!Gameover) ReviveObsticle();
    }

    //my functions
    public int addMeTolist(ObstacleStandalone lobs,Vector3 v)
    {
        //SpawnList[ObstacleCount]=v;
        ObstacleStandalone[] obsTemp = new ObstacleStandalone[ObstacleCount + 1];
        Vector3[] SpawnListTemp = new Vector3[ObstacleCount + 1];
        for (int i = 0; i < ObstacleCount; i++)
        {
            obsTemp[i] = obs[i];
            SpawnListTemp[i] = SpawnList[i];
        }

        obsTemp[ObstacleCount] = lobs;
        SpawnListTemp[ObstacleCount] = v;

        obs = obsTemp;
        SpawnList = SpawnListTemp;
        //AddToQueue(ObstacleCount);
        return ObstacleCount++;
    }

    public void PlayerHit(int id)
    {
        Debug.Log("Obscticle id = "+id+" Reported a collision with The player");
        ClearGame();
        pm.Lost();
    }

    public void ObsticlesHit(int id, int otherid)
    {
        Debug.Log("Obscticle id = " + id + " Reported a collision with obscticle"+otherid);
        kill(id);
    }

    public void ObsticlesHitGround(int id)
    {
        Debug.Log("Obscticle id = " + id);
        pm.addScore(10);
        kill(id);
    }

    private void kill(int id)
    {
        Debug.Log("Killing OBS.id = "+id);
        //AddToQueue(id);
        obs[id].die();
    }

    public Vector3 FindSpawn()
    {
        int i = 0;
        i = rand.Next(0, ObstacleCount);
        return SpawnList[i];
    }

    private void ReviveObsticle()
    {
        int i;
        if (ObstacleCount != 0)
        {
            if (AliveObsticles < MaxObsticlesAlive)
            {
                i = getNextInQueue();
                if(i>0)
                {
                    obs[i].revive();
                    AliveObsticles++;
                } else Debug.Log("Stock return an null value");
            }
        } else Debug.Log("There is no Obsticles registered..");
        
    }

    private int getNextInQueue()
    {
        int id = -1;
        if (Queue.Count != 0 && !Gameover)
        {
            id = (int)Queue.Pop();
            obs[id].inQueue = false;
        }
        return id;
    }

    private void AddToQueue(int id)
    {
        if (obs[id].inQueue == false)
        {
            Queue.Push(id);
            obs[id].inQueue = true;
        } else Debug.Log("Tryed to add existing pice to queue " +id);
    }

    public void askAddToQueue(int id)
    {
        if(!Gameover) AddToQueue(id);
        //if(Queue)
    }

    public void retry()
    {
        Queue.Clear();
        AliveObsticles = 0;
        Gameover = false;
    }

    private void ClearGame()
    {
        Gameover = true;
        AliveObsticles = 0;
        Queue.Clear();
    }
    
}
