using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour 
{
	public float smoothness = 100, zOffset = 10, height = 10;
	public Transform target;
	Vector3 targetPosition;

	void LateUpdate()
	{
		Vector3 v = target.forward * (-1) * zOffset;
		v.y += height;
		transform.position = Vector3.Lerp(transform.position, v, Time.deltaTime * smoothness);
		transform.LookAt(target);
	}
}
