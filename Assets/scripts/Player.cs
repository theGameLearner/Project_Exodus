using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float turnRadius = 1f;

    public float damageTurnMultiplier = 2f;
    public float speed = 10f;

    public float screenCenterX;

    private bool mouseDown;

    private float mouseX;

    public bool rightWingDamage = false;

    public bool leftWingDamage = false;


    // Start is called before the first frame update
    void Start()
    {
        screenCenterX = Screen.width/2;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        mouseDown = Input.GetMouseButton(0);
        if(mouseDown){
            mouseX = Input.mousePosition.x;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //turning
        if(mouseDown){
            float angle = speed*Time.fixedDeltaTime/(2*turnRadius);
            if(mouseX<screenCenterX){
                if(leftWingDamage){
                    angle/=damageTurnMultiplier;
                }
                transform.Rotate(Vector3.forward,angle,Space.World);
            }
            else{
                if(rightWingDamage){
                    angle/=damageTurnMultiplier;
                }
               transform.Rotate(-Vector3.forward,angle,Space.World); 
            }
        }
        
        


        //forward movement
        float distance = speed*Time.fixedDeltaTime;
        transform.Translate(new Vector3(0,distance,0),Space.Self);


       
        
        
    }
}
