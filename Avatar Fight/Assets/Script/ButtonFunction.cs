using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour {

public GameObject BGM;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void functions(string function)
    {
        if (function == "Back") f_Back();
        if (function == "ChangeBGM") f_BGMChange();
        if (function == "ReplayCinematic") f_ReplayCinematic();
    }

    public void f_Back()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void f_BGMChange()
    {
		int temp = BGM.GetComponent<BackGroundMusic>().BGMnum;
        temp++;
        if (temp == 3) temp = 0;
		BGM.GetComponent<BackGroundMusic>().BGMnum = temp;
		BGM.SendMessage("ChangeBGM", BGM.GetComponent<BackGroundMusic>().BGMnum);
    }
    public void f_ReplayCinematic()
    {

    }
}
