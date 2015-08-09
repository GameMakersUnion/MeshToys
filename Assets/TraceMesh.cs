using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//todo: scaling
//detecting primitive types and compensating positions accordingly
//option to highlight all polygons attached to sphere.

//Summary:
//      attach to any object, it will detect meshes and highlight them with colored spheres and drawing rays
public class TraceMesh : MonoBehaviour {

    Mesh mesh;
    GameObject[] vertMarkers;
    string NO_MESH_FILTER_MSG;
    string NO_MESH_MSG;
    string PERFORMANCE_WARN_MSG;
    int GREAT_PERFORMANCE;
    int GOOD_PERFORMANCE;
    string PERFORMANCE_WARN_MSG2;
    bool WARN_GIVEN;

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
    enum p { Cube, Sphere }
    int pSize = Enum.GetNames(typeof(p)).Length;
    Dictionary<p, PrimitiveType> primitives = new Dictionary<p, PrimitiveType>(){
        {p.Cube, PrimitiveType.Cube},
        {p.Sphere, PrimitiveType.Sphere}
    };

	// Use this for initialization
	void Start () {

        NO_MESH_FILTER_MSG = "WARN: No Mesh Filter found on gameObject: " + gameObject.name;
        NO_MESH_MSG = "WARN: No Mesh found on gameObject: " + gameObject.name;
        GREAT_PERFORMANCE = 50;
        PERFORMANCE_WARN_MSG = "WARN: Your system may perform slowly with over " + GREAT_PERFORMANCE + " vertices being rendered as spheres on gameObject: " + gameObject.name + ", so Cubes have been used.";
        GOOD_PERFORMANCE = 100;
        PERFORMANCE_WARN_MSG2 = "WARN: Your system may perform slowly with over " + GOOD_PERFORMANCE + " vertices being rendered for any reason on gameObject: " + gameObject.name + ", so 1 out of " + GOOD_PERFORMANCE + " are being shown.";
        WARN_GIVEN = false;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null){
            Debug.LogWarning(NO_MESH_FILTER_MSG);
            return;
        }
        mesh = meshFilter.mesh;
        if (mesh == null){
            Debug.LogWarning(NO_MESH_MSG);
            return;
        }
        if (mesh.vertices.Length > GREAT_PERFORMANCE)
        {
            Debug.LogWarning(PERFORMANCE_WARN_MSG);
        }
        if (mesh.vertices.Length > GOOD_PERFORMANCE)
        {
            Debug.LogWarning(PERFORMANCE_WARN_MSG2);
        }

        p PRIMITIVE = (mesh.vertices.Length > GREAT_PERFORMANCE) ? p.Cube : p.Sphere;
        vertMarkers = new GameObject[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //create spheres to show vertices 
            vertMarkers[i] = GameObject.CreatePrimitive(primitives[PRIMITIVE]);
            vertMarkers[i].transform.parent = transform;
            vertMarkers[i].name = "Vertice" + i;
            vertMarkers[i].transform.position = Vector3.Scale(mesh.vertices[i], transform.localScale) + transform.position;
            vertMarkers[i].transform.localScale *= 0.05f;
            Color cooleur1 = cooleurs[(c)((i) % cSize)];
            vertMarkers[i].GetComponent<Renderer>().material.color = cooleur1;
        }


    }
	
	// Update is called once per frame
	void Update () {
        if (mesh == null && !WARN_GIVEN)
        {
            Debug.LogWarning(NO_MESH_FILTER_MSG);
            WARN_GIVEN = true;
        }

        if (mesh == null) return;

        Vector3 p = transform.position;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //create rays to show connections
            Vector3 start = vertMarkers[i].transform.position;
            Vector3 dir = (vertMarkers[(i + 1) % mesh.vertices.Length]).transform.position - vertMarkers[i].transform.position;
            Color cooleur = vertMarkers[i].GetComponent<Renderer>().material.color;
            Debug.DrawRay(start, dir, cooleur);
        }
        
	}
}
