using UnityEngine;
using System.Collections;

public class EarthSpurt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("EarthSpurtReappear");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator EarthSpurtReappear()
	{
		Collider c = transform.GetComponent<Collider>();
		yield return new WaitForSeconds(1);
		c.enabled = false;
		yield return new WaitForSeconds(1);
		c.enabled = true;
		yield return new WaitForSeconds(1);
		c.enabled = false;
		yield return new WaitForSeconds(1);
		c.enabled = true;
	}
}
