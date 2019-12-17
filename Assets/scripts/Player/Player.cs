using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{



    public float MaxFuel = 100f;

    public float fuelConsumptionRate = 0.1f;

    public float turnRadius = 1f;

    public float damageTurnMultiplier = 2f;
    public float speed = 10f;

    public float screenCenterX;

    private bool mouseDown;

    private float mouseX;

    public Wing leftWing;
    public Wing rightWing;

    private bool canMove = true;
    private bool PerformingTrick = false;

    private bool turning = false;
    private bool turnLeft = false;

    private Animator animator;

    [SerializeField] FloatData fuel;

    private delegate void AnimEndCallback();


    // Start is called before the first frame update
    void Start()
    {
        screenCenterX = Screen.width/2;
        fuel.Data = MaxFuel;

        animator = GetComponent<Animator>();
    }

    IEnumerator AnimationCallback(string state,AnimEndCallback callback){
        
        while(!animator.GetCurrentAnimatorStateInfo(0).IsName(state))
        yield return null;

        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        yield return null;
        callback();
    }

    void endflip(){
        PerformingTrick = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        

        #if UNITY_ANDROID && !UNITY_EDITOR
        mouseDown = Input.GetMouseButton(0);
        if(mouseDown){
            turning = true;
            mouseX = Input.mousePosition.x;
            if(mouseX<=screenCenterX){
                turnLeft = true;
            }
            else{
                turnLeft = false;
            }
        }
        else{
            turning = false;
        }
        #else

        if(Input.GetKeyDown(KeyCode.Z)){
            animator.SetTrigger("Flip");
            PerformingTrick = true;
            StartCoroutine(AnimationCallback("flip",endflip));
        }

        if(Input.GetKey(KeyCode.LeftArrow)){
            turning = true;
            turnLeft = true;
           
        }
        else if(Input.GetKey(KeyCode.RightArrow)){
            turning = true;
            turnLeft = false;
        }
        else{
            turning = false;
        }

        if(Input.GetKey(KeyCode.Escape)){
            GameManager.instance.PauseGame();
        }

        #endif

       
        if(canMove){
            if(fuel.Data<=0){
                canMove = false;
            }
            else{
                fuel.Data -= fuelConsumptionRate*Time.deltaTime;
            }
        }
        
    }

    public void RefillFuel(){
        fuel.Data = MaxFuel;
    }

    public void fixWings(){
        if(leftWing.damaged){
            leftWing.Repair();
        }
         if(rightWing.damaged){
            rightWing.Repair();
        }
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        
       if(!PerformingTrick){
            //turning
            if(turning){
                float angle = speed*Time.fixedDeltaTime/(2*turnRadius);
                if(turnLeft){
                    if(leftWing.damaged){
                        angle/=damageTurnMultiplier;
                    }
                    transform.Rotate(Vector3.forward,angle,Space.World);
                }
                else{
                    if(rightWing.damaged){
                        angle/=damageTurnMultiplier;
                    }
                transform.Rotate(-Vector3.forward,angle,Space.World); 
                }
            }
            
            
            if(canMove){
                //forward movement
                float distance = speed*Time.fixedDeltaTime;
                transform.Translate(new Vector3(0,distance,0),Space.Self);

            }
       }

           
    }

   
}
