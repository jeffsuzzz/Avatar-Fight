using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

	EnemyAI parentScript;
	private Animator animator;
	//public GameObject target;   

	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		parentScript  = transform.parent.GetComponent<EnemyAI>();

	}

	// Update is called once per frame
	void Update()
	{
		parentScript = transform.parent.GetComponent<EnemyAI>();
		Vector3 targetPos = parentScript.target.transform.position;
		targetPos.y = 0;
		float targetDistance = Vector3.Distance(targetPos, transform.position);

		animator.SetFloat("distance", targetDistance);
		animator.SetFloat("health", parentScript.cur_health);
		animator.SetBool ("stun", parentScript.stunflag);
	}
  
}
