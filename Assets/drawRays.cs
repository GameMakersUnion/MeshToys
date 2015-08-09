using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class drawRays : MonoBehaviour {
    enum c { Red, Green, Blue, Orange, Yellow, Violet }
    int cSize = Enum.GetNames(typeof(c)).Length;
    Dictionary<c, Color> colors = new Dictionary<c, Color>()
    {  
        {c.Red, Color.red},
        {c.Orange, (Color.red + Color.yellow)/2},
        {c.Yellow, Color.yellow},
        {c.Green, Color.green},
        {c.Blue, Color.blue},
        {c.Violet, Color.magenta},
    };

    enum d { x,y,z, xm, ym, zm }
    int dSize = Enum.GetNames(typeof(c)).Length;
    Dictionary<d, Vector3> directions = new Dictionary<d, Vector3>(){
        {d.x, Vector3.forward},
        {d.y, Vector3.up},
        {d.z, Vector3.right},
        {d.xm, Vector3.back},
        {d.ym, Vector3.down},
        {d.zm, Vector3.left}
    };

    Bounds bounds;

	// Use this for initialization
	void Start () {
        bounds = GetComponent<Renderer>().bounds;
	}
	
	// Update is called once per frame
	void Update () {

        for(int i = 0; i < cSize; i++){

            Vector3 relativeDir = FindRelativeDir(i, transform);
            //Debug.Log(relativeDir);
            Vector3 dist = Vector3.Scale(bounds.extents, directions[(d)((i) % dSize)]);
            float relSz = RelevantDir(i, dist);
            //Debug.Log(relSz + " " + dist);
            Color color = colors[(c)((i) % cSize)];
            Debug.DrawRay(transform.position, relativeDir + dist, color);

            RaycastHit[] hit = new RaycastHit[6];
            Ray ray = new Ray(transform.position, relativeDir);
            if (Physics.Raycast(ray, out hit[i])){
                if (hit[i].collider != null)
                {
                    Collider mc = hit[i].collider.gameObject.GetComponent<Collider>();
                    if (mc == null) return;
                    Vector3 hitSize = mc.bounds.size;
                    Vector3 hitted = hitSize / 2 + hit[i].transform.position;
                    //if (i==0) Debug.Log(hitted + " " + hit[i].transform.name);
                }
            }
        }

        
	}

    //Summary:
    //      Given an index and transform, return the vector facing
    //      where each index refers to x,y,z,-x,-y,-z respectively.
    Vector3 FindRelativeDir(int i, Transform transform)
    {
        Vector3 dir;
        switch (i)
        {
            case 0:
                dir = transform.right;
                break;
            case 1:
                dir = transform.up;
                break;
            case 2:
                dir = transform.forward;
                break;
            case 3:
                dir = -transform.right;
                break;
            case 4:
                dir = -transform.up;
                break;
            case 5:
                dir = -transform.forward;
                break;
            default:
                dir = Vector3.zero;
                break;
        }
        return dir;
    }

    //these variables need to be renamed
    float RelevantDir(int i, Vector3 dist){
        float retSz;
        switch (i)
        {
            case 0:
                retSz = dist.x;
                break;
            case 1:
                retSz = dist.y;
                break;
            case 2:
                retSz = dist.z;
                break;
            case 3:
                retSz = -dist.x;
                break;
            case 4:
                retSz = -dist.y;
                break;
            case 5:
                retSz = -dist.z;
                break;
            default:
                retSz = 0;
                break;
        }

        return retSz;
    }
}
