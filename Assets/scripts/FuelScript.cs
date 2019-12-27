using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    public GameObject PointerPrefab;
    private GameObject pointer;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        pointer = (GameObject)Instantiate(PointerPrefab,transform.position,Quaternion.identity);
        ArrowPointer pointerScript = pointer.GetComponent<ArrowPointer>();
        pointerScript.targetTransform = transform;
        pointer.transform.parent = GameObject.FindGameObjectWithTag("Pointers").transform;
        pointer.transform.localScale = new Vector3(1,1,1);
    }
   /// <summary>
   /// Sent when another object enters a trigger collider attached to this
   /// object (2D physics only).
   /// </summary>
   /// <param name="other">The other Collider2D involved in this collision.</param>
   void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "Player"){
           Player playerScript = other.transform.parent.GetComponentInChildren<Player>();
           if(playerScript!=null){
               playerScript.RefillFuel();
               Destroy(pointer);
               Destroy(gameObject);
           }
       }
   }
}
