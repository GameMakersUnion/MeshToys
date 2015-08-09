using UnityEngine;
using System.Collections;

// when this script is attached to an object, it will snap to the surface beneath it
// by emitting a ray downward from it's center, and snaps to whatever it hits 
public class snapGround : MonoBehaviour {

    public enum When { Start, Update }
    public When when = When.Start;
    public float howFarAbove = 0f;

	void Start () {
        if (when == When.Start)
            SnapGround();
	}
	
	// Update is called once per frame
	void Update () {
        if (when == When.Update)
            SnapGround();
	}

    void SnapGround()
    {
        Vector3 thisSize = GetComponent<Collider>().bounds.size;
        if (thisSize == null)
        {
            Debug.LogWarning(this.name + ": no box collider found on: " + transform.name);
            return;
        }
        Vector3 down = transform.TransformDirection(-Vector3.up);
        RaycastHit hit;
        Ray ray = new Ray(transform.position, down);
        if (Physics.Raycast(ray, out hit))
            if (hit.collider != null)
            {
                Collider mc = hit.collider.gameObject.GetComponent<Collider>();
                if (mc == null) return;
                Vector3 hitSize = mc.bounds.size;
                float hitTop = hitSize.y / 2 + hit.transform.position.y;
                //Debug.Log(hitTop);
                float thisBottom = transform.position.y + thisSize.y / 2;
                if (hitTop < thisBottom) transform.position = new Vector3(transform.position.x, hitTop + thisSize.y / 2 + howFarAbove, transform.position.z);
            }


        //if (Physics.Raycast(transform.position, down, 10))
        //  print("There is something in front of the object!");


    }
}
