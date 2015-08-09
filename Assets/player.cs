using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    CharacterController cc;
    private const float turnSpd = 150f;
    private const float moveSpd = 50f;
    private const float jumpyHeighty = 10f;
    public float sprintEnergy = 1f; //0 to 1
    public float runEnergy = 1f; //0 to 1
    private float sprintFactor = 2f;  //1 to 3
    private float runFactor = 1.5f;  //1 to 1.5
    public float slowPenalty = 1f; //0 to 1

	void Start () {
        cc = GetComponent<CharacterController>();
        if (cc == null) Debug.Log("missing CC");
	}
	
	// Update is called once per frame
	void Update () {
        float xRotate = Input.GetAxis("Horizontal"); //rotate when left or right pressed
        float yMove = Input.GetAxis("Vertical"); //move forward or backward
        bool jump = Input.GetAxis("Jump") != 0; //whity can jumpy

        bool run = Input.GetAxis("Run") != 0;
        bool sprint = Input.GetAxis("Sprint") != 0;
        bool fire2 = Input.GetAxis("Fire2") != 0;

        if (xRotate != 0 || yMove != 0)
        {
            //cc.SimpleMove(new Vector3(cc.velocity.x, cc.velocity.y, yMove) * moveSpd * Time.deltaTime);
            cc.SimpleMove(transform.forward * yMove * moveSpd * sprintFactor * runFactor * slowPenalty * Time.deltaTime);
            cc.transform.Rotate(new Vector3(0, xRotate, 0) * turnSpd * sprintFactor * runFactor * slowPenalty * Time.deltaTime);
        }

        if (sprint && yMove !=0)
        {
            sprintEnergy -= 0.005f ;
            if (sprintEnergy < 0) {
                sprintEnergy = 0;
                sprintFactor = 1;
            }
            else
            {
                sprintFactor = 2;
            }
        }
        else
        {
            sprintEnergy += 0.005f;
            if (sprintEnergy > 1) sprintEnergy = 1;
            sprintFactor = 1;
        }


        if (run && yMove != 0)
        {
            runEnergy -= 0.01f;
            if (runEnergy < 0)
            {
                runEnergy = 0;
                runFactor = 1;
            }
            else
            {
                runFactor = 2;
            }
        }
        else
        {
            runEnergy += 0.01f;
            if (runEnergy > 1) runEnergy = 1;
            runFactor = 1;
        }


        /*
        if (jump)
        {
            cc.SimpleMove(new Vector3(cc.velocity.x, cc.velocity.y + jumpyHeighty + 10f, cc.velocity.z) * Time.deltaTime);
        }
        else
        {
            //apply gravity
            cc.SimpleMove(new Vector3(cc.velocity.x, cc.velocity.y - 1, cc.velocity.z) * Time.deltaTime);
        }
         */
    }
}
