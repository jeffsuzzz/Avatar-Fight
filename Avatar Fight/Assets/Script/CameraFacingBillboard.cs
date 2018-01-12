using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {

    public Camera facingcamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + facingcamera.transform.rotation * Vector3.forward,
            facingcamera.transform.rotation * Vector3.up);
    }

	public void SetCamera(Camera C)
	{
		facingcamera = C;
	}
}
