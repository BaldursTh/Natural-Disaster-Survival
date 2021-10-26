using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [Tooltip("Distance after which the rotation physics starts")]
    public float squareMaxDistance = 20;

    [Tooltip("The axis that the caught objects will rotate around")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0);

    [Tooltip("Angle that is added to the object's velocity (higher lift -> quicker on top)")]
    [Range(0, 90)]
    public float lift = 45;

    [Tooltip("The force that will drive the caught objects around the tornado's center")]
    public float rotationStrength = 50;

    [Tooltip("Tornado pull force")]
    public float tornadoStrength = 2;

    public float rotationAngle;

    public float suckForce;

    Rigidbody r;
    Vector3 pull;

    List<Torable> caughtObject = new List<Torable>();
    void Start()
    {
        rotationAxis.Normalize();

        r = GetComponent<Rigidbody>();
        r.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < caughtObject.Count; i++)
        {
            if (caughtObject[i] != null)
            {
                pull = transform.position - caughtObject[i].transform.position;
                if (pull.sqrMagnitude > squareMaxDistance)
                {
                    caughtObject[i].rb.AddForce(pull * suckForce, ForceMode.VelocityChange);
                    caughtObject[i].enabled = false;
                    print("ok");
                }
                else
                {
                    caughtObject[i].enabled = true;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody) return;
        if (other.attachedRigidbody.isKinematic) return;
        if (!other.GetComponent<Torable>()) return;

        //Add caught object to the list
        Torable caught = other.GetComponent<Torable>();
        if (!caught)
        {
            caught = other.gameObject.AddComponent<Torable>();
        }

        caught.Init(this, r, tornadoStrength);

        if (!caughtObject.Contains(caught))
        {
            caughtObject.Add(caught);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Release caught object
        Torable caught = other.GetComponent<Torable>();
        if (caught)
        {
            caught.Release();

            if (caughtObject.Contains(caught))
            {
                caughtObject.Remove(caught);
            }
        }
    }

    public float GetStrength()
    {
        return rotationStrength;
    }

    //The axis the caught objects rotate around
    public Vector3 GetRotationAxis()
    {
        return rotationAxis;
    }
}
