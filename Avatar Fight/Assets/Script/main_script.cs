using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class main_script : MonoBehaviour {

	LeftHand leftHandScript;
	RightHand rightHandScript;

	public GameObject left, right, head;
	public GameObject rightHandEmitter;
	public GameObject leftHandEmitter;
	public float firespeed;
	public Text LDinfo_text;
	public Text RDinfo_text;
	public Text LAinfo_text;
	public Text RAinfo_text;
	public Text HeadInfo;
	public GameObject[] ElementPrefabs;   //has all the element prefabs
	
	private bool[] elementflag;		//water, earth, fire, air
	private bool earthchangeH;
	private bool earthwallflag;
	private bool waterWaveP1;
	private bool waterWaveP2;
	private bool airBladeP1;
	private bool airBladeP2;
	private bool meteorflag;
	private bool flamethrowerflag;
	private bool earthsunderingflag;
	private GameObject[] sunderingrocks;
	private Vector3 sunderingrockRpos, sunderingrockLpos;
	private bool watersplashflag;
	private bool watershieldflag;
	private bool airthunderflag;
	private bool earthspurtflag;
	private float headHeightconst;

	private GameObject temp_waterwave;

	private Vector3 righthand_gesturetemp;

	public GameObject[] Hands;
	private int[] handoutlinepara;
	public CanvasGroup bloodCanvas;
    public GameObject stageControl;
    public int health;
    private int killnum;
    private float goalAlpha;
	public GameObject BGM;

	/*public GameObject PauseCanvas;
	public bool pauseflag;*/
	
	// Use this for initialization
	void Start () {
		leftHandScript = gameObject.GetComponentInChildren<LeftHand> ();
		rightHandScript = gameObject.GetComponentInChildren<RightHand> ();
		LDinfo_text.text = "";
		RDinfo_text.text = "";
		LAinfo_text.text = "";
		RAinfo_text.text = "";
		HeadInfo.text = "";

		elementflag = new bool[] {false, false, false, false};
		handoutlinepara = new int[]{ 0, 0 };
		clearflag ();
		
		killnum = 0;
        bloodCanvas.alpha = 0.0f;
        goalAlpha = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		leftHandScript = this.gameObject.transform.GetChild (0).gameObject.GetComponent<LeftHand>();
		rightHandScript = this.gameObject.transform.GetChild (1).gameObject.GetComponent<RightHand>();

		Vector3 lpos, rpos, hpos, headtoleft, headtoright;
		lpos = left.transform.position;
		rpos = right.transform.position;
		hpos = head.transform.position;
		headtoleft = (lpos - hpos)*100;
		headtoright = (rpos - hpos)*100;
		//Debug.Log ("hpos forward: "+ (head.transform.forward*100));
		float headtoleftDist = Vector3.Distance (hpos, lpos) *100;
		float headtorightDist = Vector3.Distance (hpos, rpos) *100;
		float langle = Vector3.Angle (headtoleft, Vector3.up*100);
		float rangle = Vector3.Angle (headtoright, Vector3.up*100);

		/*LDinfo_text.text = "hLdist: " + headtoleftDist;
		LAinfo_text.text = "hLang: " + langle;
		RDinfo_text.text = "hRdist: " + headtorightDist;
		RAinfo_text.text = "hRang: " + rangle;
		HeadInfo.text = "hF: " + head.transform.forward;*/

		if (rightHandScript.appMenuButtonPressed || leftHandScript.appMenuButtonPressed) {
			health = 100;
			stageControl.SendMessage ("Restart");
			stageControl.GetComponent<StageControl> ().restartFlag = false;
		}


		if (rightHandScript.padButtonPressed || leftHandScript.padButtonPressed) {
			Debug.Log (rightHandScript.padXY.x+", " + rightHandScript.padXY.y);
			Debug.Log (leftHandScript.padXY.x+", " + leftHandScript.padXY.y);
			if (rightHandScript.padXY.x > 0 || leftHandScript.padXY.x > 0) {
				BGM.transform.GetComponent <BackGroundMusic> ().BGMnum++;
			}
			else if(rightHandScript.padXY.x < 0 || leftHandScript.padXY.x < 0) {
				BGM.transform.GetComponent <BackGroundMusic> ().BGMnum--;
			}
			BGM.SendMessage ("ChangeBGM");
		}

		if (stageControl.GetComponent<StageControl>().gameStartFlag) {
			if (rightHandScript.gripButtonDown || leftHandScript.gripButtonDown) {
				health = 100;
				stageControl.GetComponent<StageControl> ().gameStartFlag = false;
				stageControl.SendMessage("NextStage");
			}
		}
		if (stageControl.GetComponent<StageControl>().restartFlag) {
			if (rightHandScript.gripButtonDown || leftHandScript.gripButtonDown) {
				health = 100;
				stageControl.SendMessage ("Restart");
				stageControl.GetComponent<StageControl> ().restartFlag = false;
			}
		}
		


		if (leftHandScript.triggerButtonPressed && rightHandScript.triggerButtonPressed) {

			//if there is no elemental flag, check them
			if (elementflag [0] == false && elementflag [1] == false && elementflag [2] == false && elementflag [3] == false) {
				if (!earthchangeH) {
					headHeightconst = hpos.y;
				}
				earthchangeH = true;

				//water start position
				if (headtoleftDist > 80.0 && headtoleftDist < 100.0 && langle > 155.0 && langle < 170.0 && headtorightDist > 80.0 && headtorightDist < 100.0 && rangle > 155.0 && rangle < 170.0) {
					elementflag [0] = true;
					Debug.Log ("water start");
					rightHandScript.Vibrate (0.2f);
					leftHandScript.Vibrate (0.2f);
					left.SendMessage ("change", 1);
					right.SendMessage ("change", 1);
				}
				//earth starting position
				else if ((headHeightconst - hpos.y)>0.12 && headtoleftDist > 40.0 && headtoleftDist < 85.0 && langle > 155.0 && headtorightDist > 40.0 && headtorightDist < 85.0 && rangle > 155.0) {
					Debug.Log ("Earth start");
					elementflag [1] = true;
					rightHandScript.Vibrate (0.2f);
					leftHandScript.Vibrate (0.2f);
					left.SendMessage ("change", 2);
					right.SendMessage ("change", 2);
				}
				//fire starting position
				else if (headtoleftDist > 45.0 && headtoleftDist < 75.0 && langle > 100.0 && langle < 130.0 && headtorightDist > 20.0 && headtorightDist < 45.0 && rangle > 125.0 && rangle < 160.0) {
					Debug.Log ("Fire start");
					elementflag [2] = true;
					righthand_gesturetemp = right.transform.position;
					rightHandScript.Vibrate (0.2f);
					leftHandScript.Vibrate (0.2f);
					left.SendMessage ("change", 2);
					right.SendMessage ("change", 2);
				}
				// air start position
				else if (headtoleftDist > 20.0 && headtoleftDist < 50.0 && langle > 135.0 && langle < 175.0 && headtorightDist > 70.0 && headtorightDist < 100.0 && rangle > 135.0 && rangle < 170.0) {
					elementflag [3] = true;
					Debug.Log ("air start");
					rightHandScript.Vibrate (0.2f);
					leftHandScript.Vibrate (0.2f);
					left.SendMessage ("change", 1);
					right.SendMessage ("change", 1);
				}

			} 
			//one element flag is up
			else {
				//water
				if (elementflag [0]) {					
					if (!waterWaveP1 && !waterWaveP2 && !watersplashflag && !watershieldflag) {
						//water wave phase1
						if (headtoleftDist > 35.0 && headtoleftDist < 70.0 && langle > 110.0 && langle < 130.0 && headtorightDist > 35.0 && headtorightDist < 70.0 && rangle > 110.0 && rangle < 130.0) {
							waterWaveP1 = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("water wave phase1");
						}
						//water splash phase1
						else if (headtoleftDist > 40.0 && headtoleftDist < 80.0 && langle > 135.0 && langle < 165.0 && headtorightDist > 40.0 && headtorightDist < 70.0 && rangle > 140.0 && rangle < 160.0) {
							watersplashflag = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("water splash phase1");
						} 
						//water shield phase1
						else if (headtoleftDist > 60.0 && headtoleftDist < 85.0 && langle > 90.0 && langle < 120.0 && headtorightDist > 60.0 && headtorightDist < 90.0 && rangle > 82.0 && rangle < 120.0) {
							watershieldflag = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("water shield phase1");
						}
					} else {
						//water shield phase2
						if (watershieldflag && headtoleftDist > 15.0 && headtoleftDist < 50.0 && langle > 100.0 && headtorightDist > 15 && headtorightDist < 50.0 && rangle > 100.0 ) {
							Debug.Log ("water shield phase2");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
							left.SendMessage("flagDelay");
							right.SendMessage("flagDelay");

							GameObject temp_watershield;	
							temp_watershield = Instantiate (ElementPrefabs [0]) as GameObject;
							temp_watershield.transform.position = head.transform.position;
							health += 50;
							if (health > 100)
								health = 100;

							Destroy (temp_watershield, 3.0f);

							clearflag ();
						}
						//water splash phase2
						if (watersplashflag &&  headtorightDist > 45.0 && headtorightDist < 75.0 && rangle > 87.0 && rangle < 130.0) {
							Debug.Log ("water splash phase2");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
							left.SendMessage("flagDelay");
							right.SendMessage("flagDelay");

							GameObject temp_watersplash;	
							temp_watersplash = Instantiate (ElementPrefabs [1], right.transform.position, right.transform.rotation) as GameObject;
							temp_watersplash.transform.SetParent (right.transform);
							temp_watersplash.transform.up = head.transform.forward;					        

							Destroy (temp_watersplash, 3.0f);

							clearflag ();
						}					
						//water wave phase2
						if (waterWaveP1 && headtoleftDist > 35.0 && headtoleftDist < 70.0 && langle > 140.0 && langle < 175.0 && headtorightDist > 35.0 && headtorightDist < 70.0 && rangle > 140.0 && rangle < 175.0) {
							waterWaveP2 = true;
							waterWaveP1 = false;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("water wave phase2");
						}
						//water wave phase3
						if (waterWaveP2 && headtoleftDist > 35.0 && headtoleftDist < 70.0 && langle > 110.0 && langle < 130.0 && headtorightDist > 35.0 && headtorightDist < 70.0 && rangle > 110.0 && rangle < 130.0) {
							Debug.Log ("water wave phase3");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
                            left.SendMessage("flagDelay");
                            right.SendMessage("flagDelay");

							temp_waterwave = Instantiate (ElementPrefabs [2], right.transform.position, head.transform.rotation) as GameObject;
							temp_waterwave.transform.Translate (0, -3, 0);
							temp_waterwave.transform.forward = head.transform.forward;

							Destroy (temp_waterwave, 4.0f);

							clearflag ();
						}

					}
					Hands [1].SendMessage ("ChangeColor", 1);
					Hands [1].SendMessage ("ChangeSize", 3);
					Hands [4].SendMessage ("ChangeColor", 1);
					Hands [4].SendMessage ("ChangeSize", 3);
				}
				//earth gesture
				else if (elementflag [1]) {					
					if (!earthwallflag && !earthsunderingflag && !earthspurtflag) {
						//earth wall
						if (headtoleftDist > 65.0 && headtoleftDist < 80.0 && langle > 145.0 && langle < 165.0 && headtorightDist > 65.0 && headtorightDist < 80.0 && rangle > 145.0 && rangle < 165.0) {
							Debug.Log ("Earth wall phrase");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
							left.SendMessage("flagDelay");
							right.SendMessage("flagDelay");

							GameObject temp_earthwall;	
							Vector3 earthvec = head.transform.forward;
							earthvec.Normalize ();
							earthvec *= 10;
							Vector3 earthpos = earthvec + head.transform.position;
							earthpos.y = 0;
							temp_earthwall = Instantiate (ElementPrefabs[3], earthpos, head.transform.rotation) as GameObject;

							Destroy (temp_earthwall, 5.0f);

							clearflag ();
						}
						//earth sundering phase 1
						else if (headtoleftDist > 45.0 && headtoleftDist < 90.0 && langle > 140.0 && langle < 165.0 &&headtorightDist > 25.0 && headtorightDist < 60.0 && rangle > 10.0 && rangle < 50.0) {
							Debug.Log ("Earth sundering phase 1");
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							earthsunderingflag = true;
						}
						//earth spurt phase 1
						else if (headtoleftDist > 30.0 && headtoleftDist < 55.0 && langle > 115.0 && langle < 155.0 &&headtorightDist > 20.0 && headtorightDist < 55.0 && rangle > 115.0 && rangle < 155.0) {
							Debug.Log ("Earth spurt phase 1");
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							earthspurtflag = true;
						}
					} 
					else {
						//earth sundering
						if(earthsunderingflag){
							if (headtoleftDist > 35.0 && headtoleftDist < 100.0 && langle > 125.0 && langle < 165.0 && headtorightDist > 45.0 && headtorightDist < 95.0 && rangle > 150.0 && rangle < 185.0){
								Debug.Log ("Earth sundering phase 2");
								rightHandScript.Vibrate (0.5f);
								leftHandScript.Vibrate (0.5f);
								sunderingrocks = new GameObject[30];
								StartCoroutine(SunderingStart());
                                left.SendMessage("flagDelay");
                                right.SendMessage("flagDelay");

                                clearflag ();
							}
						}
                        //earth spurt
						else if (earthspurtflag) {
							if (headtoleftDist > 65.0 && headtoleftDist < 85.0 && langle > 85.0 && langle < 130.0 && headtorightDist > 70.0 && headtorightDist < 85.0 && rangle > 85.0 && rangle < 130.0) {
								Debug.Log ("Earth spurt phrase 2");
								rightHandScript.Vibrate (0.5f);
								leftHandScript.Vibrate (0.5f);
                                left.SendMessage("flagDelay");
                                right.SendMessage("flagDelay");

                                GameObject temp_earthspurt;	
								Vector3 earthspurtpos = head.transform.position;
								earthspurtpos.y = 0;
								temp_earthspurt = Instantiate (ElementPrefabs[5]) as GameObject;
								temp_earthspurt.transform.position = earthspurtpos;

								Destroy (temp_earthspurt, 6.0f);

								clearflag ();
							}
						}
					}						
					Hands [2].SendMessage ("ChangeColor", 2);
					Hands [2].SendMessage ("ChangeSize", 3);
					Hands [5].SendMessage ("ChangeColor", 2);
					Hands [5].SendMessage ("ChangeSize", 3);
				}
				//fire gesture
				else if (elementflag [2] == true) {					
					if (!meteorflag && !flamethrowerflag) {
						//fire punch
						if (headtoleftDist > 18.0 && headtoleftDist < 45.0 && langle > 110.0 && langle < 170.0 && headtorightDist > 43.0 && headtorightDist < 75.0 && rangle > 90.0 && rangle < 125.0) {
							Debug.Log ("fire fist!!");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
                            left.SendMessage("flagDelay");
                            right.SendMessage("flagDelay");

                            GameObject temp_fireball;	
							temp_fireball = Instantiate (ElementPrefabs [6], right.transform.position, right.transform.rotation) as GameObject;
							temp_fireball.transform.forward = head.transform.forward;

							Destroy (temp_fireball, 5.0f);
							clearflag ();
						}
						//meteor phase1
						else if (head.transform.forward.y > 0.5 && headtoleftDist > 25.0 && headtoleftDist < 60.0 && langle > 20.0 && langle < 70.0 && headtorightDist > 30.0 && headtorightDist < 60.0 && rangle > 20.0 && rangle < 70.0) {
							meteorflag = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("Meteor phase1");
						}
						//flamethrower phase1
						else if (headtoleftDist > 50.0 && headtoleftDist < 80.0 && langle > 130.0 && langle < 160.0 && headtorightDist > 50.0 && headtorightDist < 80.0 && rangle > 130.0 && rangle < 160.0) {
							flamethrowerflag = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("flamethrower phase1");
						}

					} else {					
						//flame thrower
						if (flamethrowerflag && headtoleftDist > 30.0 && headtoleftDist < 60.0 && langle > 90.0 && langle < 130.0 && headtorightDist > 30.0 && headtorightDist < 60.0 && rangle > 90.0 && rangle < 130.0) {
							Debug.Log ("flamethrower phase2");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
                            left.SendMessage("flagDelay");
                            right.SendMessage("flagDelay");
                            GameObject temp_flamethrower_right, capsule;
							temp_flamethrower_right = Instantiate (ElementPrefabs [7], right.transform.position, right.transform.rotation) as GameObject;
							temp_flamethrower_right.transform.position = right.transform.position+= head.transform.forward;
						    temp_flamethrower_right.transform.SetParent (right.transform);
							Vector3 temp = head.transform.forward*4 + head.transform.up;
							temp_flamethrower_right.transform.forward = temp;
							GameObject temp_flamethrower_left;
							temp_flamethrower_left = Instantiate (ElementPrefabs [7], left.transform.position, left.transform.rotation) as GameObject;
						    temp_flamethrower_left.transform.position = left.transform.position+= head.transform.forward;
						    temp_flamethrower_left.transform.SetParent (left.transform);
							temp_flamethrower_left.transform.forward = temp;

							Destroy (temp_flamethrower_right, 3.0f);
							Destroy (temp_flamethrower_left, 3.0f);
							clearflag ();
						}
						//meteor
						if (meteorflag && headtoleftDist > 40.0 && headtoleftDist < 80.0 && langle > 50.0 && langle < 140.0 && headtorightDist > 40.0 && headtorightDist < 80.0 && rangle > 50.0 && rangle < 140.0) {
							Debug.Log ("Meteor phase2");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
							left.SendMessage("flagDelay");
							right.SendMessage("flagDelay");
							GameObject temp_meteor;
							temp_meteor = Instantiate (ElementPrefabs [8], transform.position, transform.rotation) as GameObject;
							temp_meteor.transform.forward = head.transform.forward;
							Vector3 sourcePos = transform.position + head.transform.forward*100;
							sourcePos.y = 100.0f;
							temp_meteor.SendMessage ("SetSourcePosition", sourcePos);

							Destroy (temp_meteor, 5.0f);
							clearflag ();
						}
					}
					Hands [2].SendMessage ("ChangeColor", 3);
					Hands [2].SendMessage ("ChangeSize", 3);
					Hands [5].SendMessage ("ChangeColor", 3);
					Hands [5].SendMessage ("ChangeSize", 3);
				}
				//air
				else if (elementflag [3]) {
					if (!airBladeP1 && !airBladeP2 && !airthunderflag) {
						//tornato 
						if (headtoleftDist > 60.0 && headtoleftDist < 105.0 && langle > 135.0 && langle < 165.0 && headtorightDist > 25.0 && headtorightDist < 50.0 && rangle > 10.0 && rangle < 70.0) {
							Debug.Log ("tornado!!");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
                            left.SendMessage("flagDelay");
                            right.SendMessage("flagDelay");
                            GameObject temp_tornado;
							temp_tornado = Instantiate (ElementPrefabs [10], transform.position, transform.rotation) as GameObject;
							Rigidbody temp_bulllet_rigid;
							temp_bulllet_rigid = temp_tornado.GetComponent<Rigidbody> ();
							Vector3 forward_temp = head.transform.forward;
							forward_temp.y = 0;
							temp_bulllet_rigid.AddForce (forward_temp * 5.0f * firespeed);

							Destroy (temp_tornado, 7.0f);
							clearflag ();
						}
						//air blade phase1
						else if (headtoleftDist > 55.0 && headtoleftDist < 75.0 && langle > 45.0 && langle < 90.0 && headtorightDist > 90.0 && headtorightDist < 100.0 && rangle > 120.0 && rangle < 150.0) {
							airBladeP1 = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("air blade phase1");
						}
						//air thunder phase1
						else if(headtoleftDist > 85 && headtoleftDist < 98.0 && langle > 120.0 && langle < 165.0 && headtorightDist > 85.0 && headtorightDist < 100.0 && rangle > 130.0 && rangle < 165.0) {
							airthunderflag = true;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("thunder phase1");
						}
					}
					else {
						//air blade phase2
						if(airBladeP1 && headtoleftDist > 65.0 && headtoleftDist < 100.0 && langle > 120.0 && langle < 155.0 && headtorightDist > 40.0 && headtorightDist < 85.0 && rangle > 60.0 && rangle < 95.0){
							airBladeP2 = true;
							airBladeP1 = false;
							rightHandScript.Vibrate (0.2f);
							leftHandScript.Vibrate (0.2f);
							Debug.Log ("air blade phase2");
						}
						//air blade phase3
						if(airBladeP2 && headtoleftDist > 10.0 && headtoleftDist < 40.0 && langle > 130.0 && langle < 175.0 && headtorightDist > 65.0 && headtorightDist < 88.0 && rangle > 130.0){
							Debug.Log ("air blade phase3");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
                            left.SendMessage("flagDelay");
                            right.SendMessage("flagDelay");

                            GameObject temp_airblade;	
							Vector3 startpos = head.transform.position + head.transform.forward*1.5f;				
							temp_airblade = Instantiate (ElementPrefabs [9], startpos, head.transform.rotation) as GameObject;
							temp_airblade.transform.forward = head.transform.forward;
							temp_airblade.transform.Rotate (20, -180, -45);
							Rigidbody temp_bulllet_rigid;
							temp_bulllet_rigid = temp_airblade.GetComponent<Rigidbody> ();

							temp_bulllet_rigid.AddForce (head.transform.forward * 5.0f * firespeed);
							Destroy (temp_airblade, 5.0f);

							clearflag ();
						}
						//air thunder phase2
						if(airthunderflag && headtoleftDist > 25.0 && headtoleftDist < 65.0 && langle > 105.0 && langle < 145.0 && headtorightDist > 25.0 && headtorightDist < 70.0 && rangle > 90.0&& rangle < 140.0){
							Debug.Log ("thunder phase2");
							rightHandScript.Vibrate (0.5f);
							leftHandScript.Vibrate (0.5f);
                            left.SendMessage("flagDelay");
                            right.SendMessage("flagDelay");

                            GameObject temp_airthunder;			
							Vector3 thundervec = head.transform.forward;
							thundervec.Normalize ();
							thundervec *= 20;
							Vector3 thunderpos = thundervec + head.transform.position;
							thunderpos.y = 0;
							temp_airthunder = Instantiate (ElementPrefabs [11]) as GameObject;
							temp_airthunder.transform.position = thunderpos;

							Destroy (temp_airthunder, 5.0f);

							clearflag ();
						}
					}
					Hands [1].SendMessage ("ChangeColor", 4);
					Hands [1].SendMessage ("ChangeSize", 3);
					Hands [4].SendMessage ("ChangeColor", 4);
					Hands [4].SendMessage ("ChangeSize", 3);
				}
			}

			
		}
		//hand release
		else {
			clearflag ();
		}

		if (health > 85) {
			goalAlpha = 0.0f;
		}
		if (health < 85 && health > 70)
        {
            goalAlpha = 0.2f;
        }
        else if(health < 70 && health > 55)
        {
            goalAlpha = 0.4f;
        }
        else if (health < 55 && health > 40)
        {
            goalAlpha = 0.6f;
        }
        else if (health < 40 && health > 25)
        {
            goalAlpha = 0.8f;
        }
        else if(health < 25)
        {
            goalAlpha = 1.0f;
        }
        ChangeAlpha();

	}

	void clearflag(){
		elementflag[0] = false; elementflag[1] = false; elementflag[2] = false; elementflag[3] = false; 
		earthwallflag = false;
		waterWaveP1= false;
		waterWaveP2= false;
		airBladeP1 = false;
		airBladeP2 = false;
		earthchangeH = false;
		meteorflag = false;
		flamethrowerflag = false;
		earthsunderingflag = false;
		watersplashflag = false;
		watershieldflag = false;
		airthunderflag = false;
		earthspurtflag = false;
		leftHandScript.change (0);
		rightHandScript.change (0);
		/*Hands [0].SendMessage ("ChangeColor", 0);
		Hands [0].SendMessage ("ChangeSize", 0);
		Hands [3].SendMessage ("ChangeColor", 0);
		Hands [3].SendMessage ("ChangeSize", 0);*/

	}

	/*IEnumerator delayPause(){
		yield return new WaitForSeconds(1F);
		pauseflag = true;
	}*/

	IEnumerator SunderingStart()
	{
		sunderingrockRpos = head.transform.position;
		sunderingrockLpos = head.transform.position;
		for (int i = 0; i < 15; i++)
		{
			Sundering(i);
			yield return new WaitForSeconds(0.005F);
			sunderingrocks[i*2].transform.Translate(sunderingrocks[i*2].transform.up/ 2);
			sunderingrocks[i*2 + 1].transform.Translate(sunderingrocks[i*2+1].transform.up / 2);
		}

		for (int j = 0; j < 30; j++) {
			Destroy(sunderingrocks[j], 3);
		}
	}

	void Sundering(int i) {           
		sunderingrockRpos += head.transform.forward;
		sunderingrockLpos += head.transform.forward;
		sunderingrockRpos.y = -0.5f;
		sunderingrockLpos.y = -0.5f;
		sunderingrocks[i*2] = Instantiate(ElementPrefabs [4], sunderingrockRpos, transform.rotation) as GameObject;
		sunderingrocks[i*2].transform.Rotate(0, 0, -60);    
		sunderingrocks[i*2 + 1] = Instantiate(ElementPrefabs [4], sunderingrockLpos, transform.rotation) as GameObject;
		sunderingrocks[i*2 + 1].transform.Rotate(0, 0, 60);     
	}
	
	public void monsterKill(){
		killnum++;
        Debug.Log("killnum: "+killnum);
        int maxenemynum = stageControl.GetComponent<StageControl>().maxenemynum;
        if (killnum == maxenemynum)
        {
			stageControl.GetComponent<StageControl> ().gameStartFlag = true;
        }
	}
	public void beenHit()
    {
		health -= 2;
		if (health <= 0)
		{
			killnum = 0;
			stageControl.SendMessage("PlayerDead");
		}   
    }
	void ChangeAlpha()
    {
        if (bloodCanvas.alpha > goalAlpha)
        {
            bloodCanvas.alpha -= 0.1f * Time.deltaTime;
        }
        else if(bloodCanvas.alpha < goalAlpha)
        {
            bloodCanvas.alpha += 0.1f * Time.deltaTime;
        }       
    }
	
}
