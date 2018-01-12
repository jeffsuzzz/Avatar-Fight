using UnityEngine;
using System.Collections;

public class HandColorChange : MonoBehaviour {

	private Renderer rend;
	private Color colorwhite, colorwater, colorearth, colorfire, colorair;
	private float outline0, outline1, outline2, outline3;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		colorwhite = new Color(1.0F, 1.0F, 1.0F, 1.0F);
		colorwater = new Color(0.11F, 0.56F, 1.0F, 1.0F);
		colorearth = new Color(245F/255F, 184F/255F, 0.0F, 1.0F);
		colorfire = new Color(1.0F, 0.0F, 0.0F, 1.0F);
		colorair = new Color(0.0F, 1.0F, 0.5F, 1.0F);

		outline0 = 0.0f;
		outline1 = 0.15f;
		outline2 = 0.25f;
		outline3 = 0.5f;

		rend.material.SetColor("_OutlineColor", colorwhite);
		rend.material.SetFloat("_Outline", outline0);
	}

	// Update is called once per frame
	void Update () {
	}

	public void ChangeColor(int i){
		switch (i) {
		case 0:
			rend.material.SetColor("_OutlineColor", colorwhite);
			break;
		case 1:
			rend.material.SetColor("_OutlineColor", colorwater);
			break;
		case 2:
			rend.material.SetColor("_OutlineColor", colorearth);
			break;
		case 3:
			rend.material.SetColor("_OutlineColor", colorfire);
			break;
		case 4:
			rend.material.SetColor("_OutlineColor", colorair);
			break;
		}
	}
	public void ChangeSize(int j){
		switch (j) {
		case 0:
			rend.material.SetFloat("_Outline", outline0);
			break;
		case 1:
			rend.material.SetFloat("_Outline", outline1);
			break;
		case 2:
			rend.material.SetFloat("_Outline", outline2);
			break;
		case 3:
			rend.material.SetFloat("_Outline", outline3);
			break;
		}
	}
}
