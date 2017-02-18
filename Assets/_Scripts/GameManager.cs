using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Transform destinationPoints, player, obstacleParent;
	public GameObject obstacle, victoryPanel, defeatPanel;
	public float playerSpeed = 10;
	public int obstacleCount = 10, health = 3;
	public float dx = 5, y = 1, dz = 5;
	public Vector3 playerSpawnPoint;
	Transform currentDestinationPoint;
	List<Transform> _destinations = new List<Transform>();
	int currentObstacleCount = 0;

	void Start()
	{
		FindDestinationPoint();
		SpawnObstacles();
		playerSpawnPoint = player.position;
		Time.timeScale = 1;
	}

	void Update()
	{
		KeepWalking();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, new Vector3(dx, 1, dz));
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(playerSpawnPoint, 0.5f);
		Gizmos.color = Color.blue;
		if(_destinations.Count > 0){
			Gizmos.DrawLine(playerSpawnPoint, _destinations[0].position);
			for(int i=1; i<_destinations.Count; i++)
				Gizmos.DrawLine(_destinations[i-1].position, _destinations[i].position);
		}
	}

	void KeepWalking()
	{
		player.LookAt(currentDestinationPoint);
		player.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
		if(Vector3.Distance(player.position, currentDestinationPoint.position) < 0.1f)
			FindDestinationPoint();
	}

	void Victory()
	{
		Debug.Log("Victory!!");
		Time.timeScale = 0;
		victoryPanel.SetActive(true);
	}

	void GameOver()
	{
		Debug.Log("Defeat!!");
		Time.timeScale = 0;
		defeatPanel.SetActive(true);
	}

	public void HealthLoss(GameObject g)
	{
		health--;
		if(health <= 0)
			GameOver();
		else{
			currentDestinationPoint = null;
			FindDestinationPoint();
			player.position = playerSpawnPoint;
			float lx = -dx/2, hx = dx/2, lz = -dz/2, hz = dz/2;
			Vector3 p = new Vector3(Random.Range(lx, hx), y, Random.Range(lz, hz));
			g.transform.position = p;
			g.SetActive(true);
		}
	}

	void FindDestinationPoint()
	{
		_destinations.Clear();
		for(int i=0; i<destinationPoints.childCount; i++)
			_destinations.Add(destinationPoints.GetChild(i));
		if(currentDestinationPoint == null){
			currentDestinationPoint = destinationPoints.GetChild(0);
			return;
		}

		int currentIndex;
		for(currentIndex = 0; currentIndex<destinationPoints.childCount; currentIndex++)
			if(currentDestinationPoint == destinationPoints.GetChild(currentIndex))
				break;
		if(currentIndex >= destinationPoints.childCount){	// Current destination point was removed from list
			float d = Vector3.Distance(player.position, destinationPoints.GetChild(0).position);
			int c = 0;

			for(int i=1; i < destinationPoints.childCount; i++){
				float t = Vector3.Distance(player.position, destinationPoints.GetChild(i).position);
				if(d > t){
					c = i;
					d = t;
				}
			}
			currentDestinationPoint = destinationPoints.GetChild(c);
		}
		else if(currentIndex == (destinationPoints.childCount - 1)){ // Current destination point is last one in list
			Victory();
		}
		else{									// Set next entity in list as current destination point
			currentIndex++;
			currentDestinationPoint = destinationPoints.GetChild(currentIndex);
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void SpawnObstacles()
	{
		float lx = -dx/2, hx = dx/2, lz = -dz/2, hz = dz/2;
		for(int i=0; i < obstacleCount; i++){
			Vector3 p = new Vector3(Random.Range(lx, hx), y, Random.Range(lz, hz));
			GameObject g = Instantiate(obstacle, p, Quaternion.identity, obstacleParent);
		}
	}
}
