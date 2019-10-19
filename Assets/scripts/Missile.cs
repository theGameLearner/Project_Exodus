using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public GameObject explosionParticleSystem;
    public bool isArmed = false;

    public float armingTime = 2f;
    public GameObject PointerPrefab;
    private GameObject pointer;
    public Transform target;

    public float turnResponse = 1f;

    public float speed = 10;

    Rigidbody2D rigidbody2D;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //instantiate OffScreen pointer
        pointer = (GameObject)Instantiate(PointerPrefab,transform.position,Quaternion.identity);
        ArrowPointer pointerScript = pointer.GetComponent<ArrowPointer>();
        pointerScript.targetTransform = transform;
        pointer.transform.parent = GameObject.FindGameObjectWithTag("Pointers").transform;
        pointer.transform.localScale = new Vector3(1,1,1);
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(armMissile());


    }

    IEnumerator armMissile(){
        yield return new WaitForSecondsRealtime(armingTime);
        isArmed = true;
    }
    

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
         //turn to point at target
            float rotamt = -Vector3.Cross((target.position-transform.position).normalized,transform.up).z;
            rigidbody2D.angularVelocity = rotamt*turnResponse;
        
        if(target!=null){
            //move forward;
            // transform.Translate(new Vector3(0,speed*Time.fixedDeltaTime,0),Space.Self);
            rigidbody2D.velocity = transform.up*speed;

           
        }
       
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "missile" || other.collider.tag == "Player"){
           
                //particle effect
                Explode();
                //increment score
                Destroy(pointer);
                Destroy(gameObject);


            
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemy" ){
            if(isArmed){
                //particle effect
                Explode();
                Destroy(pointer);
                Destroy(gameObject);

            }
            
        }
        
    }

    void Explode(){
        GameObject ps = Instantiate(explosionParticleSystem,transform.position,Quaternion.identity);
        Destroy(ps,ps.GetComponent<ParticleSystem>().main.duration);
    }
}
