using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{

    public float magnitude;
    public float duration;
    public float changeInterval;
    Rigidbody rb;
    bool isRumbling = false;

    float x;
    float z;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Rumble());
        }
    }
    IEnumerator Rumble()
    {
        float elapsed = 0;
        float time = Time.time;
        isRumbling = true;
        while (elapsed < duration)
        {

            x = Random.Range(-1f, 1f) * magnitude;
            z = Random.Range(-1f, 1f) * magnitude;
            
            yield return new WaitForSeconds(changeInterval);
            elapsed = Time.time - elapsed;
        }
        isRumbling = false;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Rumble"))
        {
            
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null && isRumbling)
            {
                //rb.velocity = new Vector3(x, rb.velocity.y, z);
                rb.AddForce(new Vector3(x, 0, z));
            }
        }
    }

}
