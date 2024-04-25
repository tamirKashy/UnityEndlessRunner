using System;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class ObstacleStandalone : MonoBehaviour
{
    //Managers
    private PlayerManager pm;
    private ObsteclesManager Manager;
    private Rigidbody rb;
    private MeshRenderer mrend;
    private BoxCollider boxC;

    //vars

    //static vars;
    private float MovmentSpeed = 0;
    public int id;
    public bool Alive;
    public bool inQueue = false;

    // Start is called before the first frame update
    void Start()
    {
        //find my managers
        pm = FindObjectOfType<PlayerManager>();
        Manager = FindObjectOfType<ObsteclesManager>();
        rb = GetComponent<Rigidbody>();
        mrend = GetComponent<MeshRenderer>();
        boxC = GetComponent<BoxCollider>();
        //
        id = Manager.addMeTolist(this, rb.position);
        rb.position = DefaultSpawn();
        mrend.enabled = false;
        die();
        Manager.AliveObsticles++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Alive && !Manager.Gameover)
        {
                if (MovmentSpeed != Manager.MovmentSpeed)
                {
                    rb.velocity = Vector3.zero;
                    MovmentSpeed = Manager.MovmentSpeed;
                }

                if (rb.transform.position.z < -(pm.transform.position.z+10) && Alive)
                {
                    rb.position = DefaultSpawn();
                    rb.velocity = Vector3.zero;
                    Manager.ObsticlesHitGround(id);
                }
                
        }
        else
        {
            if(Manager.Gameover) die();
            else if(!inQueue) Manager.askAddToQueue(id);
        }


    }

    private void FixedUpdate()
    {
        if (Alive)
        {
         rb.AddForce(0,0,-(MovmentSpeed*Time.deltaTime),ForceMode.VelocityChange);   
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        ObstacleStandalone Myother;
        if (other.gameObject.tag == "Player" && mrend.enabled && Alive)
        {
            Manager.PlayerHit(id);
        }
        else if (other.gameObject.tag == "Obstacle" && Alive)
        {
            Myother = other.gameObject.GetComponent<ObstacleStandalone>();
            if(Myother.Alive) Manager.ObsticlesHit(id, Myother.id);
        }
        
    }
    
    //functions
    private Vector3 DefaultSpawn()
    {
        Vector3 v = Vector3.zero;
        v.y = 1;
        v.z = 150;
        return v;
    }

    public void die()
    {
        Debug.Log(id+":Im dying ");
        Alive = false;
        MovmentSpeed = 0;
        mrend.enabled = false;
        boxC.enabled = false;
        rb.position = DefaultSpawn();
        rb.velocity = Vector3.zero;
        Manager.AliveObsticles -=1;
        Manager.askAddToQueue(id);
    }
    
    
    public void revive()
    {
        rb.position = Manager.FindSpawn();
        rb.velocity = Vector3.zero;
        mrend.enabled = true;
        boxC.enabled = true;
        Alive = true;
        inQueue = false;
    }
    
}
