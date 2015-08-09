using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class makeMesh : MonoBehaviour {

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

    Mesh mesh;
    GameObject[] vertMarkers;

	// Use this for initialization
	void Start () {
        mesh = new Mesh();
        mesh.name = "testMesh";
        mesh.Clear();

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0.0f, 0.0f, 0.0f);
        vertices[1] = new Vector3(1.0f, 0.0f, 0.0f);
        vertices[2] = new Vector3(1.0f, 1.0f, 0.0f);
        vertices[3] = new Vector3(0.0f, 1.0f, 0.0f);
        mesh.vertices = vertices;

        //TraceMesh(mesh);

        int[] triangles = new int[6] { 3, 2, 0, 2, 1, 0};
        mesh.triangles = triangles;

        mesh.uv = new Vector2[] {
            new Vector2 (0, 0),
            new Vector2 (0, 1),
            new Vector2 (1, 1),
            new Vector2 (1, 0)
        };

        //Vector3[] normals = new Vector3[4] { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward };
        //mesh.normals = normals;

        mesh.RecalculateNormals();

        MeshFilter mf = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
        MeshRenderer mr = (MeshRenderer)gameObject.GetComponent(typeof(MeshRenderer));
        mf.mesh = mesh;
        mr.material.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 p = transform.position;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //create rays to show connections
            /*Vector3 start = vertMarkers[i].transform.position;
            Vector3 dir = (vertMarkers[(i + 1) % mesh.vertices.Length]).transform.position - vertMarkers[i].transform.position;
            Color cooleur = vertMarkers[i].GetComponent<Renderer>().material.color;
            Debug.DrawRay(start, dir, cooleur);
        */}
    }

    void TraceMesh(Mesh mesh)
    {
        vertMarkers = new GameObject[4];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //create spheres to show vertices 
            vertMarkers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertMarkers[i].transform.parent = transform;
            vertMarkers[i].name = "Vertice";
            vertMarkers[i].transform.position = mesh.vertices[i] + transform.position;
            vertMarkers[i].transform.localScale *= 0.1f;
            Color cooleur1 = cooleurs[(c)((i) % cSize)];
            vertMarkers[i].GetComponent<Renderer>().material.color = cooleur1;
        }

    }
}
