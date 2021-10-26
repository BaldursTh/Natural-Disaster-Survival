using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torable : MonoBehaviour
{
    public Tornado tornado;
    public Rigidbody rb;
    SpringJoint spring;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        Vector3 newPosition = spring.connectedAnchor;
        newPosition.y = transform.position.y;
        spring.connectedAnchor = newPosition;
    }
    private void FixedUpdate()
    {
        Vector3 direction = transform.position - tornado.transform.position;
       
        Vector3 projection = Vector3.ProjectOnPlane(direction, tornado.GetRotationAxis());
        projection.Normalize();
        Vector3 normal = Quaternion.AngleAxis(130, tornado.GetRotationAxis()) * projection;
        normal = Quaternion.AngleAxis(tornado.lift, projection) * normal;
        rb.AddForce(normal * tornado.GetStrength(), ForceMode.Force);

        Debug.DrawRay(transform.position, normal * 10, Color.red);
    }
    public void Init(Tornado tornadoRef, Rigidbody tornadoRigidbody, float springForce)
    {
        //Make sure this is enabled (for reentrance)
        enabled = true;

        //Save tornado reference
        tornado = tornadoRef;

        //Initialize the spring
        spring = gameObject.AddComponent<SpringJoint>();
        spring.spring = springForce;
        spring.connectedBody = tornadoRigidbody;

        spring.autoConfigureConnectedAnchor = false;

        //Set initial position of the caught object relative to its position and the tornado
        Vector3 initialPosition = Vector3.zero;
        initialPosition.y = transform.position.y;
        spring.connectedAnchor = initialPosition;
    }

    public void Release()
    {
        enabled = false;
        Destroy(spring);
    }
}
