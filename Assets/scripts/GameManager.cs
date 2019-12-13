using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameStates{
    running,GameOver,paused
}

public class GameManager : MonoBehaviour
{

    public GameObject fuelPrefab;
    public bool shipCollisions = false;

    public GameStates gameState = GameStates.running;

    public float spawnRadius=10f;
    public GameObject shipPrefab;

    public float ShipSpawnRate = 20f;

    private int ShipCount = 0;
    [SerializeField] FloatData scoreData;

    // singleTon reference
    public static GameManager instance;
    private Transform playerTransform;

    Coroutine shipSpawnCoroutine;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

     public void SpawnEnemyShip(){
        
        //get random position on a circle of spawn radius centred on player
        Vector2 spawnPos = Random.insideUnitCircle.normalized*spawnRadius;
        spawnPos += new Vector2(playerTransform.position.x,playerTransform.position.y);


        
        //instantiate ship
        GameObject shipObj; 
        shipObj = (GameObject)Instantiate(shipPrefab,new Vector3(spawnPos.x,spawnPos.y,0),Quaternion.identity);
        missileLauncher mlScript = shipObj.GetComponentInChildren<missileLauncher>();
        if(mlScript!=null){
            mlScript.playerTransform = playerTransform;
        }

        enemyShip enemyScript = shipObj.GetComponent<enemyShip>();
       if(enemyScript!=null)
       {
           enemyScript.destPos = playerTransform.position; 
        }
        ShipCount++;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        ShipCount = 0;
        scoreData.Data = 0;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        shipSpawnCoroutine = StartCoroutine(ShipSpawn());
        gameState = GameStates.running;
    }


    IEnumerator ShipSpawn(){
        while(true){
            SpawnEnemyShip();
            yield return new WaitForSeconds(ShipSpawnRate);
        }

        

    }


    public void ShipDestroyed(GameObject ship){

        scoreData.Data+=10;
        if(ShipCount>0){
            ShipCount--;
        }
        if(ShipCount==0){
            StopCoroutine(shipSpawnCoroutine);
            shipSpawnCoroutine = StartCoroutine(ShipSpawn());
        }
        Instantiate(fuelPrefab,ship.transform.position,Quaternion.identity);
    }

    public void gameOver(){
        gameState = GameStates.GameOver;
        StopAllCoroutines();
        Time.timeScale = 0;
    }

    /// <summary>
    /// Callback sent to all game objects when the player pauses.
    /// </summary>
    /// <param name="pauseStatus">The pause state of the application.</param>
    void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus){
            PauseGame();
        }
        else{
            UnpauseGame();
        }
    }

    public void PauseGame(){
        gameState = GameStates.paused;
        Time.timeScale = 0;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void UnpauseGame(){
        gameState = GameStates.running;
        Time.timeScale = 1;
    }



   
}
