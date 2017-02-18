using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public GameManager gameManager;

	void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == "Obstacle"){
			Debug.Log(c.transform.tag);
			c.gameObject.SetActive(false);
			gameManager.HealthLoss(c.gameObject);
		}
	}
}
