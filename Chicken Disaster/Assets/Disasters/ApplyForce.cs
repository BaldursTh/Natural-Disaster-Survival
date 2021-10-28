using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    GameObject[] rumbles;
    public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        rumbles = GameObject.FindGameObjectsWithTag("Rumble");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartRumble();
        }
    }

    void StartRumble()
    {
       foreach (GameObject r in rumbles)
        {

            r.GetComponent<Rigidbody>().AddForce(new Vector3(magnitude, magnitude, 0))  ;
        }
    }
}
