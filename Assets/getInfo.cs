using UnityEngine;
using System.Collections.Generic;


//Summary:
//      Find all components on object that inherit from this class
//      and aggregate their infos into one string and debug.log that.

public class getInfo : MonoBehaviour {

    List<Component> components;

	// Use this for initialization
	public virtual void Start () {
        components = new List<Component>();
        components.AddRange(GetComponents<getInfo>());
        foreach (Component c in components)
        {
            //print(c.gameObject +", " + gameObject);
            //if (c.gameObject == gameObject)
              //  print("getInfo gameObjects found: " + c.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
