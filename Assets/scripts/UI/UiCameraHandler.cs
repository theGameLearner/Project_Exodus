/*
 * Copyright (c) The Game Learner
 * https://connect.unity.com/u/rishabh-jain-1-1-1
 * https://www.linkedin.com/in/rishabh-jain-266081b7/
 * 
 * created on - #CREATIONDATE#
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UiCameraHandler : MonoBehaviour
{
    public Camera UiCam;

	private void Start()
	{
		UiCam = GetComponent<Camera>();
		if(UiCam == null || !UiCam.orthographic)
		{
			Debug.LogError("the script " + this.GetType().Name + " is only allowed to be attached when there is a orthographic camera attached to it, not perspective ");
			Destroy(this);
		}
	}

	public Vector3 GetUiWorldPosition(Vector3 worldPosition)
	{
		return UiCam.ScreenToWorldPoint(worldPosition);
	}	
}
