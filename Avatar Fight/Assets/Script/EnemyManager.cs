using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject target;
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime;            // How long between each spawn.
	//public GameObject StageControl;

	private bool enableSpawn;
	private int totalenemynum;
	private int maxenemynum;

	// Use this for initialization
	void Start () {
		enableSpawn = true;
		InvokeRepeating("Spawn", spawnTime-2, spawnTime);
	}

	// Update is called once per frame
	void Update () {
		totalenemynum = transform.parent.GetComponent<StageControl>().enemynum;
		maxenemynum = transform.parent.GetComponent<StageControl>().maxenemynum;
		if (totalenemynum >= maxenemynum)
		{
			enableSpawn = false;
		}
		else
		{
			enableSpawn = true;
		}

	}

	void Spawn()
	{
		if (enableSpawn)
		{
			Vector3 spawnPos = new Vector3(Random.Range(-15.0F, 15.0F), 0, Random.Range(-15.0F, 15.0F));
			spawnPos += transform.position;
			spawnPos.y = 0;

			GameObject enemies;
			enemies = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
			enemies.SendMessage("setTarget", target);
			int stagenum = transform.parent.GetComponent<StageControl>().stagenum;
			enemies.SendMessage("SpeedUp", stagenum);
			enemies.SendMessage("IncreaseHP", stagenum);

			transform.parent.GetComponent<StageControl>().SendMessage("SpawnNumber");
		}

	}
}
