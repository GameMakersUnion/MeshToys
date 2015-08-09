using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Summary:
//      Project a staircase from a single object
public class stairs : MonoBehaviour {

    List<GameObject> steps = new List<GameObject>();
    GameObject baseStep;
    //var to project ray for stairs, for now only a line
    Quaternion direction;
    Bounds bounds;
    float dist = 40f;
    float angle = 60f;
    bool created = false;

	// Use this for initialization
	void Start () {
        direction = Quaternion.Euler(new Vector3(0, angle, angle));
        bounds = GetComponent<Renderer>().bounds;
        baseStep = gameObject;
        Destroy(baseStep.GetComponent<stairs>());
        Destroy(baseStep.GetComponent<snapGround>());
        steps = new List<GameObject>();


        Build();

	}
	
	// Update is called once per frame
	void Update () {
        Project();
    }

    void Build()
    {
        if (!created)
        {
            //draw stairs
            //calculate position with SOHCAHTOA
            float startPos = baseStep.transform.position.y + bounds.extents.y;
            float endPos = startPos + dist;
            
            //GameObject.CreatePrimitive("")

            for (int i = 0; i < (int)dist; i++)
            {
                Vector3 spawnPos = transform.position + new Vector3(0, bounds.extents.y * i * 2, bounds.extents.z * i * 5);
                GameObject step = (GameObject)Instantiate(baseStep, spawnPos, Quaternion.identity);
                //step.transform.parent = 
                steps.Add(step);
            }
            created = true;
        }

    }

    void Project()
    {
        //Draw Ray of direction
        Vector3 dir = Quaternion.ToEulerAngles(direction);
        print(dir);
        Debug.DrawRay(transform.position, dir * dist, Color.green);
    }


}
