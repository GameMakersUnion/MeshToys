using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//todo: scaling
//detecting primitive types and compensating positions accordingly
//option to highlight all polygons attached to sphere.

//Summary:
//      attach to any object, it will detect meshes and highlight them with colored spheres and drawing rays
[ExecuteInEditMode]
public class TraceMesh : MonoBehaviour
{

    Mesh mesh;
    GameObject[] vertMarkers;
    string NO_MESH_FILTER_MSG;
    string NO_MESH_MSG;
    string PERFORMANCE_WARN_MSG;
    int GREAT_PERFORMANCE;
    int GOOD_PERFORMANCE;
    string PERFORMANCE_WARN_MSG2;
    bool WARN_GIVEN;
    bool TRACED;
    Transform trace;
    public bool rebuild;
    p PRIMITIVE;

    enum c { Red, Orange, Yellow, Green, Blue, Violet }
    int cSize = Enum.GetNames(typeof(c)).Length;
    Dictionary<c, Color> cooleurs;
    Dictionary<c, Material> mats;
    enum p { Cube, Sphere }
    int pSize = Enum.GetNames(typeof(p)).Length;
    Dictionary<p, PrimitiveType> primitives = new Dictionary<p, PrimitiveType>(){
        {p.Cube, PrimitiveType.Cube},
        {p.Sphere, PrimitiveType.Sphere}
    };

    // Use this for initialization
    void Start()
    {

        cooleurs = new Dictionary<c, Color>()
        {  
            {c.Red, Resources.Load<Material>("Red").color},
            {c.Orange, Resources.Load<Material>("Orange").color},
            {c.Yellow, Resources.Load<Material>("Yellow").color},
            {c.Green, Resources.Load<Material>("Green").color},
            {c.Blue, Resources.Load<Material>("Blue").color},
            {c.Violet, Resources.Load<Material>("Violet").color},
        };


        mats = new Dictionary<c, Material>()
        {  
            {c.Red, Resources.Load<Material>("Red")},
            {c.Orange, Resources.Load<Material>("Orange")},
            {c.Yellow, Resources.Load<Material>("Yellow")},
            {c.Green, Resources.Load<Material>("Green")},
            {c.Blue, Resources.Load<Material>("Blue")},
            {c.Violet, Resources.Load<Material>("Violet")},
        };



        NO_MESH_FILTER_MSG = "WARN: No Mesh Filter found on gameObject: " + gameObject.name;
        NO_MESH_MSG = "WARN: No Mesh found on gameObject: " + gameObject.name;
        GREAT_PERFORMANCE = 50;
        PERFORMANCE_WARN_MSG = "WARN: Your system may perform slowly with over " + GREAT_PERFORMANCE + " vertices being rendered as spheres on gameObject: " + gameObject.name + ", so Cubes have been used.";
        GOOD_PERFORMANCE = 100;
        PERFORMANCE_WARN_MSG2 = "WARN: Your system may perform slowly with over " + GOOD_PERFORMANCE + " vertices being rendered for any reason on gameObject: " + gameObject.name + ", so 1 out of " + GOOD_PERFORMANCE + " are being shown.";
        WARN_GIVEN = false;
        rebuild = true;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogWarning(NO_MESH_FILTER_MSG);
            return;
        }

        mesh = meshFilter.sharedMesh;

        if (mesh == null)
        {
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

        //buildItWillCome();
    }

    // Update is called once per frame
    void Update()
    {
        if (mesh == null && !WARN_GIVEN)
        {
            Debug.LogWarning(NO_MESH_FILTER_MSG);
            WARN_GIVEN = true;
        }

        if (mesh == null) return;

        if (rebuild)
        {
            rebuild = false;
            //buildItWillCome();
        }

        //drawItItCame();

    }

    //the problem with building it vs drawing it, is i'm basing my start method off 4 vertices, but updating over 6,

    void buildItWillCome()
    {
        //convenience storage obj.
        trace = transform.Find("Trace");
        if (trace == null)
        {
            trace = new GameObject("Trace").transform;
            trace.position = transform.position;
            trace.parent = transform;
        }

        PRIMITIVE = (mesh.vertices.Length > GREAT_PERFORMANCE) ? p.Cube : p.Sphere;
        vertMarkers = new GameObject[mesh.vertices.Length];
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            int t = mesh.triangles[i];
            int nextT = mesh.triangles[(int)i / 3 * 3 + (i + 1) % 3];
            int nextNextT = mesh.triangles[(int)i / 3 * 3 + (i + 2) % 3];

            //check if child exists, if so, continue instead of cloning. only matters in [ExecuteInEditMode]
            Transform soughtVertice = gameObject.transform.Find("Trace/Vertice" + i);
            if (soughtVertice)
            {
                try
                {
                    //collect references if exists
                    vertMarkers[i] = soughtVertice.gameObject;

                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.Log("IndexOutOfRangeException");
                    Debug.Break();

                }
                continue;
            }

            makeVertice(i, t);
            makeVertice(i+1, nextT);
            makeVertice(i+1+1, nextNextT);

            //mat.color = cooleur1;

            try
            {
                Mesh vMesh = vertMarkers[i].GetComponent<MeshFilter>().sharedMesh;
                int sz = vMesh.colors.Length;
                for (int j = 0; j < sz; j++)
                {
                    print("quack");
                    Color cooleur1 = mats[(c)((i) % cSize)].color;
                    vMesh.colors[j] = cooleur1;
                }

                //print(i + ": " + cooleur1);
            }
            catch (NullReferenceException e)
            {
                Debug.Log("NullReferenceException");
                Debug.Break();
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log("IndexOutOfRangeException");
                Debug.Break();
            }
        }
    }

    void makeVertice(int iteration, int tvi)
    {
        //create spheres to show vertices 
        print(iteration);
        vertMarkers[tvi] = GameObject.CreatePrimitive(primitives[PRIMITIVE]);
        vertMarkers[tvi].AddComponent<V>();
        vertMarkers[tvi].transform.parent = trace.transform;
        vertMarkers[tvi].name = "Vertice" + iteration;
		vertMarkers[tvi].transform.position = transform.localToWorldMatrix.MultiplyPoint(mesh.vertices[tvi]);
		Vector3 whichSize = vertMarkers[tvi].transform.localScale * 0.05f;
        int whichMaterial = ((int)iteration / 3) % cSize;
        vertMarkers[tvi].GetComponent<Renderer>().material = mats[(c)whichMaterial];
        print("(int)i / 3:" + (int)iteration / 3);
        vertMarkers[tvi].transform.localScale = whichSize;

        print("start: " + (int)iteration / 3);

    }

    void drawItItCame()
    {
        Vector3 p = transform.position;
        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            int t = mesh.triangles[i];
            int nextT = mesh.triangles[(int)i / 3 * 3 + (i + 1) % 3];
            int nextNextT = mesh.triangles[(int)i / 3 * 3 + (i + 2) % 3];


            //if (vertMarkers[t] != null && vertMarkers[nextT] != null && vertMarkers[nextNextT] != null)
            {
                //draw rays the color of the first of three vertices in a triangle.
                //Material mat;
                //v.GetComponent<Renderer>().material = mats[(c)((i) % cSize)];


                Vector3 p1 = vertMarkers[t].transform.position;
                Vector3 p2 = (vertMarkers[nextT]).transform.position;
                Vector3 p3 = (vertMarkers[nextNextT]).transform.position;

                Vector3 start = p1;
                Vector3 dir = p2 - vertMarkers[t].transform.position;
                //Color cooleur = vertMarkers[t].GetComponent<Renderer>().material.color; //loops through say six colors        //                print("i:" + i + "cSz:" + cSize + "i%cSz:"+i%cSize);
                int whichMaterial = ((int)i / 3) % cSize;
                Color cooleur = mats[(c)whichMaterial].color;

                Debug.DrawRay(start, dir, cooleur);
                //print("::: triIdx: " + t + ", nextT: " + nextT + ", vert@t: " + mesh.vertices[t]*2 + ", vert@nextT: " + mesh.vertices[nextT]*2 + /*", col@t: " + (c)((t) % cSize) +*/ ", p2: " + p2 );
                //float frac = i / (float)mesh.triangles.Length;
                //Color cc = new Color(0, frac, frac);
                //Debug.Log(i + " " + cc + " " + i / mesh.triangles.Length + " " + mesh.triangles.Length);
                //1 and 2 >> towards 3
                //2 and 3 >> towards 1
                //3 and 1 >> towards 2.


            }
            /*else
            {
                print("a vertMarkers has left the building. Thank you very much.");
                rebuild = true;
            }*/
        }
        //Debug.Log("stop");
    }

    public void RemoveMe(String name){
        vertMarkers.ToList().RemoveAll(item => item.name == name);

        /*
         *  ArgumentNullException: Argument cannot be null.
            Parameter name: source
            System.Linq.Check.Source (System.Object source)
            System.Linq.Enumerable.ToList[GameObject] (IEnumerable`1 source)
            TraceMesh.RemoveMe (System.String name) (at Assets/TraceMesh.cs:223)
            V.OnDestroy () (at Assets/V.cs:18)
         */
    }
}
