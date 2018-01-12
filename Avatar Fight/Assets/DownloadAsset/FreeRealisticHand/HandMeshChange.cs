using UnityEngine;
using System.Collections;

public class HandMeshChange : MonoBehaviour {

    public GameObject initialObject;
    public GameObject swapObject;

    Mesh initialMesh;
    Mesh swapMesh;

    GameObject theTarget;

    // Use this for initialization
    void Start () {
        theTarget = initialObject;

        initialMesh = initialObject.GetComponent<MeshFilter>().sharedMesh;
        swapMesh = swapObject.GetComponent<MeshFilter>().sharedMesh;

        this.GetComponent<MeshFilter>().sharedMesh = swapMesh;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
