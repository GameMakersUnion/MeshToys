using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class testt : MonoBehaviour {

    Bounds bounds;
    int x;
    int y;
    int z;

	// Use this for initialization
	void Start () {
        bounds = GetComponent<Renderer>().bounds;
        //find longest side
        x = (bounds.extents.x > bounds.extents.y && bounds.extents.x > bounds.extents.z) ? 1 : 0;
        y = (bounds.extents.y > bounds.extents.x && bounds.extents.y > bounds.extents.z) ? 1 : 0;
        z = (bounds.extents.z > bounds.extents.x && bounds.extents.z > bounds.extents.x) ? 1 : 0;
    }
	
	// Update is called once per frame
	void Update () {

        //draw out detection rays
        Vector3 center = transform.position;
        Vector3 offset = new Vector3(bounds.extents.x * x, bounds.extents.y * y, bounds.extents.z * z);
        Vector3 start1 = center + offset;
        Vector3 start2 = center - offset;
        Vector3 dir = offset.normalized;
        Color colorRay = Color.magenta;
        Debug.DrawRay(start1, dir, colorRay);
        Debug.DrawRay(start2, -dir, colorRay);
    }
}
