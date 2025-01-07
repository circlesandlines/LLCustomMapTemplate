using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    private bool inside = false;
    public Vector3 forceSetting;
    public Vector3 force;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Climber").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inside)
        {
            force.x = forceSetting.x * Time.deltaTime;
            force.y = forceSetting.y * Time.deltaTime;
            force.z = forceSetting.z * Time.deltaTime;

            rb.AddForce(transform.TransformDirection(force));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            inside = false;
        }
    }
}
