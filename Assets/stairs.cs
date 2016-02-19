using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//Summary:
//      Project a staircase from a single object
public class stairs : MonoBehaviour {

    public enum inputMethod_t { XY, Angle }
    public inputMethod_t inputMethod = inputMethod_t.XY;

    List<GameObject> steps = new List<GameObject>();
    GameObject baseStep;
    //var to project ray for stairs, for now only a line
    //Quaternion direction;
    Bounds bounds;
    float dist = 40f;
    //float angle = 60f;
    bool created = false;

    public Vector3 distance_;
    //public Vector3 distance { get { return distance_; } set { distance_ = new Vector3(0, value.y, value.z); } }

	// Use this for initialization
	void Start () {
        //direction = Quaternion.Euler(new Vector3(0, angle, angle));
        bounds = GetComponent<Renderer>().bounds;
        baseStep = gameObject;
        Destroy(baseStep.GetComponent<snapGround>());
        steps = new List<GameObject>();
        distance_ = new Vector3(0, bounds.size.y, bounds.size.z*2.5f);

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
            
            GameObject stairs = new GameObject("Stairs"); //container
            transform.parent = stairs.transform;

            for (int i = 1; i <= (int)dist; i++)
            {
                Vector3 spawnPos = transform.position + distance_*i;
                GameObject step = (GameObject)Instantiate(baseStep, spawnPos, Quaternion.identity);
                Destroy(step.GetComponent<stairs>());
                step.name = "Step" + i;
                step.transform.parent = stairs.transform;
                steps.Add(step);
            }
            created = true;
        }
    }

    void Project()
    {
        foreach (GameObject step in steps)
        {
            //Debug.DrawRay()
        }
        //Draw Ray of direction
        //Vector3 dir = Quaternion.ToEulerAngles(direction);
        //print(dir);
        //Debug.DrawRay(transform.position, dir * dist, Color.green);
    }

    enum c { Red, Orange, Yellow, Green, Blue, Violet }
    int cSize = Enum.GetNames(typeof(c)).Length;
    Dictionary<c, Color> cooleurs = new Dictionary<c, Color>()
    {  
        {c.Red, Color.red},
        {c.Orange, (Color.red + Color.yellow)/2},
        {c.Yellow, Color.yellow},
        {c.Green, Color.green},
        {c.Blue, Color.blue},
        {c.Violet, Color.magenta},
    };
}
