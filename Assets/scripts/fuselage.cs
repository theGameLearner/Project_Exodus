using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuselage : MonoBehaviour
{
    public GameObject explosionParticleSystem;
   public leftwing leftWing;
   public leftwing rightWing;

   /// <summary>
   /// Sent when an incoming collider makes contact with this object's
   /// collider (2D physics only).
   /// </summary>
   /// <param name="other">The Collision2D data associated with this collision.</param>
   void OnCollisionEnter2D(Collision2D other)
   {
       if(other.collider.tag == "missile" ){

           //particle effect
            StartCoroutine( Explode());
       }
   }

   /// <summary>
   /// Sent when another object enters a trigger collider attached to this
   /// object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   void OnTriggerEnter2D(Collider2D other)
   {
       if(GameManager.instance.shipCollisions){
           if(other.tag == "enemy"){

                //particle effect
               StartCoroutine( Explode());
                

            }
        }
       
   }


     IEnumerator Explode(){
        GameObject ps = Instantiate(explosionParticleSystem,transform.position,Quaternion.identity);
        Destroy(ps,ps.GetComponent<ParticleSystem>().main.duration);
        transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        transform.parent.GetComponent<Player>().enabled = false;
        yield return new WaitForSecondsRealtime(ps.GetComponent<ParticleSystem>().main.duration);
        GameManager.instance.gameOver();
        
        

    }


}
