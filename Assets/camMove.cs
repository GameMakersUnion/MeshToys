using UnityEngine;
using System.Collections;

public class camMove : MonoBehaviour {

    Camera camera;
    GameObject player;
    float origDist;
    bool isOrigDist;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        if (camera == null) Debug.Log ("Camera not found, which is odd");
        player = transform.parent.gameObject;
        if (player == null && player.tag != "Player") Debug.LogWarning("Player not found");
        origDist = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform);
        isOrigDist = true;
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 20, Color.black);

        //detect if anything in between camera and player
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit != player && isOrigDist)
            {
                //move closer
                transform.position = objectHit.position;
                isOrigDist = false;
            }
            else
            {
                //transform.position = -fwd * origDist;
            }
        }
            /*
        //detect if anything behind camera up until it's original distance
        else if (Physics.Raycast(transform.position, -fwd, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit == null)
            {
                transform.position = -fwd * origDist;
            }

        }*/
	}
}
