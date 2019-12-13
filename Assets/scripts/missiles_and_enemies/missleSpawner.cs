using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missleSpawner : MonoBehaviour
{
    public GameObject[] missilePrefabs;

    public Transform playerTransform;

    public float spawnRadius=10f;

    public float spawnRate = 2f;

    private bool spawnMissiles = false;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        spawnMissiles = true;
        StartCoroutine(PeriodicSpawn());
    } 

    IEnumerator PeriodicSpawn(){
        while(true){
            while(!spawnMissiles);
            SpawnMissile(0);
            yield return new WaitForSecondsRealtime(spawnRate);
        }
        
    }

    public void SpawnMissile(int type){
        
        //get random position on a circle of spawn radius centred on player
        Vector2 spawnPos = Random.insideUnitCircle.normalized*spawnRadius;
        spawnPos += new Vector2(playerTransform.position.x,playerTransform.position.y);


        
        //instantiate missile
        GameObject missileObj; 
        missileObj = (GameObject)Instantiate(missilePrefabs[type],new Vector3(spawnPos.x,spawnPos.y,0),Quaternion.identity);
        

        Missile mScript = missileObj.GetComponent<Missile>();
       if(mScript!=null)
       { mScript.enabled=false;
        mScript.target = playerTransform;


        //point it toward player
        Vector2 direction = (playerTransform.position-missileObj.transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
        mScript.enabled=true;}
    }

}
