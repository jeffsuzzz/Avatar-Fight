using UnityEngine;
using System.Collections;

public class MoviePlayer: MonoBehaviour{  

	public MovieTexture movTexture; //影片紋理 
	public GameObject plane;
	public GameObject StageControl;

	void Start() {
		GetComponent<Renderer>().material.mainTexture = movTexture;   //設置當前對象為主紋理為影片紋理(這邊我有做修正)  
		movTexture.loop = false;//設置循環撥放，如果要當開頭的話可以設計成不循環然後自動切場景下面會再提 
		movTexture.Play ();
		Invoke ("LoadTutorial", 30);
	}

	void Update() {
		if(!movTexture.isPlaying){
				movTexture.Play();
				plane.SetActive(false);
			}  
	}

	void LoadTutorial()
	{
		StageControl.GetComponent<StageControl> ().gameStartFlag = true;
	}

	/*void OnGUI() {  
		if(GUILayout.Button("Play"))   
			if(!movTexture.isPlaying){
				movTexture.Play();
			}  
		if(GUILayout.Button("Pause")){
			movTexture.Pause();//暫停播放
		}  
		if(GUILayout.Button("Stop")){
			movTexture.Stop();//停止播放
		}
	}*/
}
