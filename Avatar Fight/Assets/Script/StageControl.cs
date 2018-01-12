using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageControl : MonoBehaviour {

	public int stagenum;
	public int enemynum;
	public int maxenemynum;
	public TextMesh stagetext;
	public bool gameStartFlag;
	public bool restartFlag;   
	public GameObject[] Tutorial;

	private GameObject[] childManagers;

	// Use this for initialization
	void Start () {
		stagenum = 0;
		enemynum = 0;
		maxenemynum = 5;
		stagetext.text = "";
		Invoke("CloseText", 5);

		childManagers = new GameObject[4];
		childManagers[0] = transform.GetChild(0).gameObject;
		childManagers[1] = transform.GetChild(1).gameObject;
		childManagers[2] = transform.GetChild(2).gameObject;

		gameStartFlag = false;
		restartFlag = false;

	}

	// Update is called once per frame
	void Update () {
		if (gameStartFlag) {
			stagetext.gameObject.SetActive(true);
			if (stagenum == 4) 
			{
				stagetext.text = "Congratulations, you've won!";
				restartFlag = true;
			}
			else {
				if(stagenum<3){
					Tutorial[stagenum].SetActive (true);
				}
				stagetext.text = "Press grip to start";
			}
		}

	}

	public void SpawnNumber()
	{
		enemynum++;
	}

	public void NextStage()
	{
		if(stagenum<3){
			Tutorial[stagenum].SetActive (false);
		}
		stagenum++;  
		Debug.Log("next stage"+stagenum);

		switch (stagenum)
		{
		case 1:
			stagetext.gameObject.SetActive(true);
			stagetext.text = "Stage 1";
			Invoke("CloseText", 5);
			StartCoroutine(Delay(2));
			childManagers[0] = transform.GetChild(0).gameObject;
			childManagers[0].SetActive(true);  
			childManagers[1] = transform.GetChild(1).gameObject;
			childManagers[1].SetActive(false);  
			childManagers[2] = transform.GetChild(2).gameObject;
			childManagers[2].SetActive(false);
			break;
		case 2:
			stagetext.gameObject.SetActive (true);
			stagetext.text = "Stage 2";
			Invoke ("CloseText", 5);
			StartCoroutine(Delay(2));
			childManagers[0] = transform.GetChild(0).gameObject;
			childManagers[0].SetActive(true);  
			childManagers[1] = transform.GetChild(1).gameObject;
			childManagers[1].SetActive(true);  
			childManagers[2] = transform.GetChild(2).gameObject;
			childManagers[2].SetActive(false);
			break;
		case 3:
			stagetext.gameObject.SetActive (true);
			stagetext.text = "Stage 3";
			Invoke ("CloseText", 5);
			StartCoroutine (Delay (2));
			childManagers [0] = transform.GetChild (0).gameObject;
			childManagers [0].SetActive (true);  
			childManagers [1] = transform.GetChild (1).gameObject;
			childManagers [1].SetActive (true); 
			childManagers [2] = transform.GetChild (2).gameObject;
			childManagers [2].SetActive (true);
			break;
		case 4:
			stagetext.gameObject.SetActive (true);
			stagetext.text = "Stage 4";
			Invoke ("CloseText", 5);
			StartCoroutine(Delay(2));
			break;

		}
	}

	public void PlayerDead()
	{
		stagetext.gameObject.SetActive(true);
		stagetext.text = "You've failed";
		for (int i=0; i<3; i++)
		{
			childManagers[i].SetActive(false);
		}
		restartFlag = true;
	}

	public void Restart()
	{
		stagenum = 1;
		enemynum = 0;
		maxenemynum = 20 * stagenum;
		childManagers[0].SetActive(true);
		stagetext.text = "Stage 1";
		Invoke ("CloseText", 5);
	}

	void CloseText()
	{
		stagetext.gameObject.SetActive(false);
	}

	IEnumerator Delay(float time)
	{
		yield return new WaitForSeconds(time);
		maxenemynum += 5 * (stagenum);
	}
}
