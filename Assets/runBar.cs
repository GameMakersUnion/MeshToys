using UnityEngine;
using System.Collections;

/** attached to image, reading whether running is occuring autonomously **/
public class runBar : MonoBehaviour {

    GameObject capsule;
    player script;
    RectTransform rectTransform;
    float origWidth;

	// Use this for initialization
	void Start () {
        capsule = GameObject.Find("Capsule");
        if (capsule == null) Debug.LogWarning("Player Capsule not found");
        else script = capsule.GetComponent<player>();
        if (script == null) Debug.LogWarning("Player Capsule's comp script script not found");
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null) Debug.LogWarning("Canvas' comp rect transform not found");
        else origWidth = rectTransform.sizeDelta.x;
    }
	
	// Update is called once per frame
	void Update () {
        float xWidth = origWidth * script.sprintEnergy;
        rectTransform.sizeDelta = new Vector2(xWidth, rectTransform.sizeDelta.y); 
	}
}
