using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuselage : MonoBehaviour
{
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
           GameManager.instance.gameOver();
           Destroy(transform.parent.gameObject);
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
                GameManager.instance.gameOver();
                Destroy(transform.parent.gameObject);

            }
        }
       
   }


}
