using UnityEngine;
using System.Collections;

public class slowPlayer : MonoBehaviour {

    BoxCollider bc;

	// Use this for initialization
	void Start () {
        bc = GetComponent<BoxCollider>();
        if (bc == null || !bc.isTrigger)
        {
            Debug.LogWarning(this.name + ": No trigger found on: " + gameObject.name);
            return;
        }
        
	}

    void OnTriggerStay(Collider other) {
        player player = other.GetComponent<player>();
        if (player!=null)
        {
            player.slowPenalty = 0.5f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        player player = other.GetComponent<player>();
        if (player != null)
        {
            player.slowPenalty = 1f;
        }
    }

}
