using UnityEngine;
using System.Collections;

//Summary:
//      Generate spiral stairs on a given cylindar
public class spiral : MonoBehaviour {

    Mesh mesh;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<Mesh>();
        Debug.Log(mesh.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
