using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering");
    }
}
