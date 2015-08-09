using UnityEngine;
using System.Collections;

public class addTriggerZone : MonoBehaviour {

    public enum Position { Above, Center, Below }
    public Vector2 margin = Vector2.zero; //single margin
    public Position position = Position.Above;
    public float yHeight = 0f;

	// create a bounding box the same width and length as this object, but a controllled height
	void Awake () {
        Vector3 thisSize = GetComponent<Collider>().bounds.size;
        if (thisSize == null)
        {
            Debug.LogWarning(this.name + ": no collider found on: " + transform.name);
            return;
        }
        //check if trigger region exists already
        BoxCollider bc = GetComponent<BoxCollider>();
        bool hasTrigger = bc != null && bc.isTrigger;
        if (!hasTrigger){
            //add collider
            bc = gameObject.AddComponent<BoxCollider>();
            bc.isTrigger = true;
            //margin calculation not what I wanted on a plane, but workable,
            //and yCentering not what I want on non-planes, but oh well.
            //on planes size = 10,0,10 matches plane exactly 
            //on cubes size = 1,1,1 matches cube exactly
            float xSize = (bc.size.x - margin.x * 2 > 0) ? bc.size.x - margin.x * 2 : bc.size.x;
            float zSize = (bc.size.z - margin.y * 2 > 0) ? bc.size.z - margin.y * 2 : bc.size.z;
            float ySize = (yHeight==0) ? 3f : yHeight;
            float yCenter = -(ySize / 2f * (int)position - ySize / 2f + transform.position.y);
            //position collider
            bc.size = new Vector3(xSize, ySize, zSize);
            bc.center = new Vector3(0, yCenter, 0);
        }
	}	
}
