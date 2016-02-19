using UnityEngine;
using System.Collections;

public class V : MonoBehaviour {

    TraceMesh tm;

    void Start()
    {
        tm = transform.parent.parent.GetComponent<TraceMesh>();
        if (tm == null) Debug.LogWarning("Vertice instantiated without parent.parent having TraceMesh!");
    }

    void OnDestroy()
    {
        if (tm == null) return;
        //notify parent
        tm.RemoveMe(name);
    }
}
