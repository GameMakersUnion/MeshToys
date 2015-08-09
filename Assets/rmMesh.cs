using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Summary:
//      accesses the mesh data of whatever this object collides with, removing the verts
public class rmMesh : MonoBehaviour {

    Collider collider; //other
    Collision collision;

    void Start()
    {
        //make add 2 collider, one as trigger, one not.
        Collider c = GetComponent<Collider>();
        if (c == null) c = gameObject.AddComponent<BoxCollider>();
        c.isTrigger = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(new Vector3(0, 0, 500f));
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Triggering");
        collider = other;
    }

    void OnCollisionStay(Collision collision)
    {
        this.collision = collision;
    }


    void Update()
    {

        if (collision != null)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.blue);
            }
        }


        GameObject gameObject1 = gameObject; //this object
        GameObject gameObject2 = null; //other object
        Bounds bounds1 = gameObject1.GetComponent<Renderer>().bounds;
        Bounds bounds2 = new Bounds(Vector3.zero, Vector3.zero);
        if (collider != null)
        {
            gameObject2 = collider.gameObject;
            bounds2 = gameObject2.GetComponent<Renderer>().bounds;
        }

        bool testCond = bounds1.Intersects(bounds2);
        if (collider != null && testCond && gameObject2!=null)
        {
            //Debug.Log(bounds1 + ", " + bounds2);
            //access mesh data now.
            gameObject1.GetComponent<Renderer>();

            MeshFilter mf = (MeshFilter)gameObject2.GetComponent(typeof(MeshFilter));
            Mesh mesh = mf.mesh;

            string s = "";
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                s += i + ":" + mesh.vertices[i] + ";";
            }
            //Debug.Log(s);
            
        }


    }


}
