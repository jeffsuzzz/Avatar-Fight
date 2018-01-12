using UnityEngine;
using System.Collections;

public class LeftHand : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	public bool gripButtonPressed = false;
	public bool gripButtonDown = false;

	private Valve.VR.EVRButtonId triggrtButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public bool triggerButtonDown = false;
	public bool triggerButtonUp = false;
	public bool triggerButtonPressed = false;

	private Valve.VR.EVRButtonId padButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	public bool padButtonPressed = false;
	public Vector2 padXY;

	private Valve.VR.EVRButtonId appMenuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
	public bool appMenuButtonPressed = false;

	public GameObject normal;
	public GameObject together;
	public GameObject fist;

	private SteamVR_Controller.Device controller {get { return SteamVR_Controller.Input ((int)trackObj.index);}}
	private SteamVR_TrackedObject trackObj;

    private bool changeflag;

	// Use this for initialization
	void Start () {
		trackObj = GetComponent<SteamVR_TrackedObject> ();

		normal.SetActive (false);
		together.SetActive (false);
		fist.SetActive (false);

        changeflag = true;
		change (0);
	}

	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log ("Controller faillllllll");
			return;
		}

		gripButtonPressed = controller.GetPress (gripButton);
		gripButtonDown = controller.GetPressDown (gripButton);

		triggerButtonDown = controller.GetPressDown (triggrtButton);
		triggerButtonUp = controller.GetPressUp (triggrtButton);
		triggerButtonPressed = controller.GetPress (triggrtButton);

		padButtonPressed = controller.GetPressDown (padButton);
		padXY = controller.GetAxis (padButton);

		appMenuButtonPressed = controller.GetPressDown (appMenuButton);
	}

	public void change(int num){
        if (changeflag) {
            switch (num)
            {
                case 0:
                    normal.SetActive(true);
                    together.SetActive(false);
                    fist.SetActive(false);
                    break;
                case 1:
                    normal.SetActive(false);
                    together.SetActive(true);
                    fist.SetActive(false);
                    break;
                case 2:
                    normal.SetActive(false);
                    together.SetActive(false);
                    fist.SetActive(true);
                    break;
                case 3:
                    normal.SetActive(true);
                    together.SetActive(false);
                    fist.SetActive(false);
                    break;
            }
        }
		
	}
    public void flagDelay() {
        changeflag = false;
        StartCoroutine(delay());                                                                                                                                                                                                                                                                                                 
    }
    IEnumerator delay() {
        yield return new WaitForSeconds(2F);
        changeflag = true;
    }

	public void Vibrate( float length)
	{
		StartCoroutine (LongVibration(length,1));
	}
	IEnumerator LongVibration(float length, float strength) {
		for(float i = 0; i < length; i += Time.deltaTime) {
			SteamVR_Controller.Input ((int)trackObj.index).TriggerHapticPulse ((ushort)Mathf.Lerp(0, 3999, strength));
			yield return null;
		}
	}

}
