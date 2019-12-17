using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    PolygonCollider2D wingCollider;
    public GameObject particleSystem;

    public bool damaged = false;
    public float repairTime = 20f;

    Coroutine r;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        wingCollider = this.GetComponent<PolygonCollider2D>();
        Debug.Log(wingCollider);
    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "missile" || other.gameObject.tag == "enemy"){
            damaged = true;
            toggleCollider();
            GameManager.instance.SpawnRepairKit();
            r = StartCoroutine(repairWing());

        }
    }

    public void toggleCollider(){
        wingCollider.isTrigger = !wingCollider.isTrigger;
        particleSystem.SetActive(!particleSystem.activeSelf);
    }

    IEnumerator repairWing()
    {
        yield return new WaitForSecondsRealtime(repairTime);
        Repair();

    }

    public void Repair()
    {
        damaged = false;
        toggleCollider();
        StopCoroutine(r);
    }
}
