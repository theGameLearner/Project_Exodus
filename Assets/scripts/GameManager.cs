using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool shipCollisions = false;

    public float spawnRadius=10f;
    public GameObject shipPrefab;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    public Text ScoreText;
    public Text GameOverScore;

    public float ShipSpawnRate = 20f;

    private int ShipCount = 0;
    private int score;

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
        score = 0;
        updateScore();
        GameOverMenu.SetActive(false);
        PauseMenu.SetActive(false);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        shipSpawnCoroutine = StartCoroutine(ShipSpawn());
    }

    void updateScore(){
        ScoreText.text = "Score:"+score;
    }

    IEnumerator ShipSpawn(){
        while(true){
            SpawnEnemyShip();
            yield return new WaitForSecondsRealtime(ShipSpawnRate);
        }

        

    }


    public void ShipDestroyed(GameObject ship){

        score+=10;
        updateScore();
        if(ShipCount>0){
            ShipCount--;
        }
        if(ShipCount==0){
            StopCoroutine(shipSpawnCoroutine);
            shipSpawnCoroutine = StartCoroutine(ShipSpawn());
        }
    }

    public void gameOver(){
        GameOverMenu.SetActive(true);
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
    }

    public void PauseGame(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void UnpauseGame(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }



   
}
