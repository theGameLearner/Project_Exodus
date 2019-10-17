using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;

    public float offsetx=0;
    public float offsety=0;

    // Update is called once per frame
    void Update()
    {
        if(target!=null){
            transform.position = target.transform.position + new Vector3(offsetx,offsety,-10);
        }
    }
}
