using UnityEngine;
using System.Collections;

public class WaterWave : MonoBehaviour {

	private Vector3 direction;

	// Use this for initialization
	void Start () {
		direction = transform.forward;
		direction.y = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (direction * Time.deltaTime *35, Space.World);
	}
}
