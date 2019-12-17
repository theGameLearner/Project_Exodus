using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShip : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject explosionParticleSystem;
    public GameObject PointerPrefab;
    private GameObject pointer;

    public int MaxHealth = 2;

    public float movementSpeed = 5f;

    public float turnRate = 10f;

    public float tolerance = 5;

    public float destRadius = 20f;

    public GameObject healthBar;


    public Vector3 destPos;

    private bool canMove = false;

    private float health;

    [SerializeField] FloatData speedMultiplier;






    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        health = MaxHealth;

        turnRate*=speedMultiplier.Data;
        movementSpeed*=speedMultiplier.Data;

        //instantiate OffScreen pointer
        pointer = (GameObject)Instantiate(PointerPrefab,transform.position,Quaternion.identity);
        ArrowPointer pointerScript = pointer.GetComponent<ArrowPointer>();
        pointerScript.targetTransform = transform;
        pointer.transform.parent = GameObject.FindGameObjectWithTag("Pointers").transform;
        pointer.transform.localScale = new Vector3(1,1,1);

        
    }

    // Update is called once per frame
   
   

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(canMove){
            if((transform.position-destPos).magnitude>tolerance){
                transform.Translate(new Vector3(0,movementSpeed*Time.deltaTime));
            }
            else{
                canMove = false;
                Vector2 newDst = (Random.insideUnitCircle.normalized)*destRadius+new Vector2(PlayerTransform.position.x,PlayerTransform.position.y);
                destPos = new Vector3(newDst.x,newDst.y,0);

            }
        }
        else{
                Vector3 direction = (destPos-transform.position).normalized;
                Quaternion rot = Quaternion.FromToRotation(Vector3.up,direction);
                if(transform.up != direction){
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnRate*Time.deltaTime);
                }
                else{
                    canMove = true;
                }
        

        }
        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {   
       
            if(other.tag == "missile"){

                if(other.gameObject.GetComponent<Missile>().isArmed){
                    appydamage();
                }

               
            }
        
        
    }

    void appydamage(){
        health-=1;
        if(health<=0){
            Explode();
            //particle effect
            GameManager.instance.ShipDestroyed(this.gameObject);
            Destroy(this.pointer);
            Destroy(this.gameObject);
            return;
        }
        updateHealthBar();

    }

    void updateHealthBar()
    {
        float ratio = health/MaxHealth;
        healthBar.transform.localScale = new Vector3(ratio,1,1);
    }

    void Explode(){
        GameObject ps = Instantiate(explosionParticleSystem,transform.position,Quaternion.identity);
        Destroy(ps,ps.GetComponent<ParticleSystem>().main.duration);
    }

}
