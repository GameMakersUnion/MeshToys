using UnityEngine;
using System.Collections;

public class getBounds : getInfo {

    Bounds bounds;
    public bool showBounds = true;
    public string statement;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        if (showBounds) { 
            bounds = GetComponent<Renderer>().bounds;
            if (bounds != null)
            {
                //Debug.Log(gameObject.name + " has " + bounds);
                statement = gameObject.name + " has " + bounds;
            }
            else Debug.Log("No Renderer found, cannot show Bounds");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
