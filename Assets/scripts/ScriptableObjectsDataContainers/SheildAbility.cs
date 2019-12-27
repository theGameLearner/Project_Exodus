using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SheildAbility : PlayerAbilityBase
{
    
    GameObject sheild;

    public GameObject Meter;

    public float MaxsheildCharge;

    [SerializeField] FloatData sheildCharge;

    public float dischargeRate;

    bool isActive = false;


    public override void abilityInitialize(){
        sheild = Instantiate(AbilityPrefab,PlayerTransform.position,Quaternion.identity,PlayerTransform);
        sheild.SetActive(false);
        sheildCharge.Data = MaxsheildCharge;

    }

    public override void abilityUpdate(){
        if(isActive&&sheildCharge.Data>0){
            sheildCharge.Data = Mathf.Clamp(sheildCharge.Data-dischargeRate,0,sheildCharge.Data);
        }

    }

    public void Activate(){
        sheild.SetActive(true);
        //Meter.SetActive(true);
    }

    public void Deactivate(){
        sheild.SetActive(false);
        //Meter.SetActive(true);
    }

    public override void OnAbilityKeyDown(){

        Debug.Log("key down");
        isActive = true;
        Activate();
    }

    public override void OnAbilityKeyUp(){
        isActive = false;
        Deactivate();
    }







}
