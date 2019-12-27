using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : ScriptableObject
{

    public GameObject AbilityPrefab;

    protected Transform PlayerTransform;


    public void OnPickup(Transform playerTransform){
        PlayerTransform = playerTransform;
        abilityInitialize();
    }

    public virtual void abilityInitialize(){}


    public virtual void OnAbilityKeyDown(){}

    public virtual void OnAbilityKey(){}

    public virtual void abilityUpdate(){}


    public virtual void OnAbilityKeyUp(){}

    public virtual void abilityDeinitialize(){}

    
}
