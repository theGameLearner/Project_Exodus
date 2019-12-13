using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileLauncher : MonoBehaviour
{
    public GameObject missilePrefab;

    public GameObject progressBar;

    public Transform playerTransform;

    public float spawnRate = 2f;



    float time;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {

        StartCoroutine(PeriodicSpawn());
        time = 0f;
    } 

    IEnumerator PeriodicSpawn(){
        while(true){

            yield return new WaitForSeconds(spawnRate);
            SpawnMissile();
            
        }
        
    }

    public void SpawnMissile(){
        
        
        //instantiate missile
        GameObject missileObj; 
        missileObj = (GameObject)Instantiate(missilePrefab,new Vector3(transform.position.x,transform.position.y,0),Quaternion.identity);
        

        Missile mScript = missileObj.GetComponent<Missile>();
       if(mScript!=null)
       { mScript.enabled=false;
        mScript.target = playerTransform;


        //point it toward player
        Vector2 direction = (playerTransform.position-missileObj.transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
        mScript.enabled=true;}
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(time<=spawnRate){
            time+=Time.deltaTime;
            float ratio = time/spawnRate;
            progressBar.transform.localScale = new Vector3(ratio,1,1);
        }
        else{
            time = 0f;
        }
    }
}
