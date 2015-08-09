using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    GameObject[] verts;
    MeshFilter mf;
    // Use this for initialization
	void Start () {
        mf = GetComponent<MeshFilter>();
        verts = trace.tracee(mf.mesh, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        trace.tracuu(mf.mesh, gameObject, verts);
    }
}
