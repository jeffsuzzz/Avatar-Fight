using UnityEngine;
using System.Collections;

public class BackGroundMusic : MonoBehaviour {

	public AudioClip[] audioes;
    public int BGMnum;
	public bool fadeoutflag;

    private AudioSource audio;
    private int musicnum;    
	private float goalvolume;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        BGMnum = 0;
		Invoke ("ChangeBGM", 30);
		fadeoutflag = false;
    }
	
	// Update is called once per frame
	void Update () {  
		if(fadeoutflag)
		{
			if(audio.volume>0){
				audio.volume -= 0.5f * Time.deltaTime;
			}
			else{
				fadeoutflag = false;
				audio.clip = audioes [BGMnum];
				audio.Play ();
			}
		}
		else {
			if (BGMnum == 1) {
				goalvolume = 1f;
			} else {
				goalvolume = 0.6f;
			}
			if (audio.volume < goalvolume) {
				audio.volume += 0.5f * Time.deltaTime;
			}
		}
    }

    public void ChangeBGM()
    {
		fadeoutflag = true;
		if (BGMnum == 4)
			BGMnum = 0;
		else if (BGMnum == -1)
			BGMnum = 3;
		/*for(int i=0;i<6;i++){
			audio.volume -= 0.1;
		}
		
		BGMnum = BGMnum % 4;
		audio.clip = audioes[BGMnum];
		for(int i=0;i<6;i++){
			audio.volume += 0.1;
		}
        audio.Play();*/
    }
	/*IEnumerator PausePlay(){
		
	}*/

}
