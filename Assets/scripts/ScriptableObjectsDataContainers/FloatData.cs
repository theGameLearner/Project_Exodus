using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{

    [SerializeField] private float data;

    public float Data{
        get{
            return data;
        }
        set{
            data = value;
        }
    }
   
}
