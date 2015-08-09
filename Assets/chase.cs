using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class chase : MonoBehaviour {

    Vector3 thisSize;
    SphereCollider sc;
    bool newSearch = false;
    const float SEARCH_RADIUS = 5f;
    float SEARCH_EXPAND_RATE = 0.05f; //from 0.3f (fastest) to 0.05f (calmest)
    GameObject searchSphere;
    Material searchMat;

    // create a sphere collider trigger several times the object's size to search for the player within
    void Awake()
    {
        thisSize = GetComponent<Collider>().bounds.size;
        if (thisSize == null)
        {
            Debug.LogWarning(this.name + ": no collider found on: " + transform.name);
            return;
        }
    }

    void Update()
    {
        //emit pulse sphere
        if (!newSearch)
        {
            searchSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            searchSphere.transform.position = transform.position;
            searchSphere.transform.localScale = thisSize; //close enough
            searchSphere.GetComponent<SphereCollider>().isTrigger = true;
            searchMat = searchSphere.GetComponent<MeshRenderer>().material;
            searchMat.shader = Shader.Find("Legacy Shaders/Transparent/Bumped Specular");
            searchMat.color = new Color(searchMat.color.r, searchMat.color.g, searchMat.color.b, 0.5f);
            newSearch = true;
        }
        //expand pulse sphere visualizing search distance
        else
        {
            //expand
            searchSphere.transform.localScale += new Vector3(SEARCH_EXPAND_RATE, SEARCH_EXPAND_RATE, SEARCH_EXPAND_RATE);

            //sphere finally reaches search radius boundary
            if (searchSphere.transform.localScale.x > SEARCH_RADIUS * thisSize.x) //|| searchSphere.transform.localScale.y > SEARCH_RADIUS || searchSphere.transform.localScale.z > SEARCH_RADIUS
            {
                Destroy(searchSphere);
                searchSphere = null; //not sure does anything meaningful
                newSearch = false;
            }
        }

    }

    void OnTriggerStay(Collider other) 
    {
        player player = other.GetComponent<player>();
        if (player != null){

            //look towards 

            //player exists within trigger
            //far away 5% chance to move towards within 40% of 180 degrees when facing

            //near by
        }
    }
}
