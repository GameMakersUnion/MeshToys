using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//Summary:
//      attach to any object, it will detect meshes and highlight them with colored spheres and drawing rays
public static class trace
{
    static bool WARN_GIVEN;
    static string NO_MESH_FILTER_MSG = "WARN: No Mesh Filter found on gameObject: ";

    enum c { Red, Orange, Yellow, Green, Blue, Violet }
    static int cSize = Enum.GetNames(typeof(c)).Length;
    static Dictionary<c, Color> cooleurs = new Dictionary<c, Color>()
    {  
        {c.Red, Color.red},
        {c.Orange, (Color.red + Color.yellow)/2},
        {c.Yellow, Color.yellow},
        {c.Green, Color.green},
        {c.Blue, Color.blue},
        {c.Violet, Color.magenta},
    };
    enum p { Cube, Sphere }
    static int pSize = Enum.GetNames(typeof(p)).Length;
    static Dictionary<p, PrimitiveType> primitives = new Dictionary<p, PrimitiveType>(){
        {p.Cube, PrimitiveType.Cube},
        {p.Sphere, PrimitiveType.Sphere}
    };

    // Use this for initialization

    public static GameObject[] tracee(this Mesh mesh, GameObject gameObject)
    {
        GameObject[] vertMarkers; 
        string NO_MESH_MSG;
        string PERFORMANCE_WARN_MSG;
        int GREAT_PERFORMANCE;
		int GOOD_PERFORMANCE;
		string PERFORMANCE_WARN_MSG2;
		int POOR_PERFORMANCE;
		string PERFORMANCE_WARN_MSG3;
		bool WARN_GIVEN;

        NO_MESH_MSG = "WARN: No Mesh found on gameObject: " + gameObject.name;
        GREAT_PERFORMANCE = 50;
        PERFORMANCE_WARN_MSG = "WARN: Your system may perform slowly with over " + GREAT_PERFORMANCE + " vertices being rendered as spheres on gameObject: " + gameObject.name + ", so Cubes have been used.";
        GOOD_PERFORMANCE = 100;
        PERFORMANCE_WARN_MSG2 = "WARN: Your system may perform slowly with over " + GOOD_PERFORMANCE + " vertices being rendered for any reason on gameObject: " + gameObject.name + ", so 1 out of " + GOOD_PERFORMANCE + " are being shown.";
		POOR_PERFORMANCE = 200;
		PERFORMANCE_WARN_MSG3 = "WARN: Your system may perform very slowly with over " + POOR_PERFORMANCE + " vertices being rendered for any reason on gameObject: " + gameObject.name + ", so total amount will be capped at " + POOR_PERFORMANCE + ".";
		WARN_GIVEN = false;

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogWarning(NO_MESH_FILTER_MSG + gameObject.name);
            return null;
        }
        mesh = meshFilter.mesh;
        if (mesh == null)
        {
            Debug.LogWarning(NO_MESH_MSG);
            return null;
        }
        if (mesh.vertices.Length > GREAT_PERFORMANCE)
        {
            Debug.LogWarning(PERFORMANCE_WARN_MSG);
        }
        if (mesh.vertices.Length > GOOD_PERFORMANCE)
        {
            Debug.LogWarning(PERFORMANCE_WARN_MSG2);
        }
		if (mesh.vertices.Length > POOR_PERFORMANCE)
		{
			Debug.LogWarning(PERFORMANCE_WARN_MSG3);
		}

        p PRIMITIVE = (mesh.vertices.Length > GREAT_PERFORMANCE) ? p.Cube : p.Sphere;
        vertMarkers = new GameObject[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
			if (i > POOR_PERFORMANCE) return vertMarkers;
            //create spheres to show vertices 
            vertMarkers[i] = GameObject.CreatePrimitive(primitives[PRIMITIVE]);
            vertMarkers[i].transform.parent = gameObject.transform;
            vertMarkers[i].name = "Vertice" + i;
			vertMarkers[i].transform.position = gameObject.transform.localToWorldMatrix.MultiplyPoint(mesh.vertices[i]);
			vertMarkers[i].transform.localScale *= 0.05f;
            Color cooleur1 = cooleurs[(c)((i) % cSize)];
            vertMarkers[i].GetComponent<Renderer>().material.color = cooleur1;
        }
        return vertMarkers;
    }

    public static void tracuu(Mesh mesh, GameObject gameObject, GameObject[] vertMarkers)
    {
        if (mesh == null && !WARN_GIVEN)
        {
            Debug.LogWarning(NO_MESH_FILTER_MSG + gameObject.name);
            WARN_GIVEN = true;
        }

        if (mesh == null) return;

        Vector3 p = gameObject.transform.position;
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
