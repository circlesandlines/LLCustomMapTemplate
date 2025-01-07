using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject player;
    public bool _override = true;
    public bool thirdOverride = false;
    public bool fourthOverride = false;
    public float ea_x = 0;
    public float ea_y = -90;
    public float ea_z = 90;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Climber");
    }

    // Update is called once per frame
    void Update()
    {
        if (thirdOverride)
        {
            transform.LookAt(player.transform.position, transform.position + (transform.position - player.transform.position));

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            eulerAngles.x = 0;
            eulerAngles.y += 90;
            transform.eulerAngles = eulerAngles;
        }
        else if (_override)
        {
            transform.LookAt(player.transform.position, transform.position + (transform.position - player.transform.position));

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            eulerAngles.x = 90;
            transform.eulerAngles = eulerAngles;
        }
        else if (fourthOverride)
        {
            transform.LookAt(player.transform.position, transform.position + (transform.position - player.transform.position));

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = ea_z;
            eulerAngles.x = ea_x;
            eulerAngles.y += ea_y;
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            transform.LookAt(player.transform.position, transform.position + (transform.position - player.transform.position));
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 180;
            eulerAngles.z = 0;
            transform.eulerAngles = eulerAngles;
        }
    }
}
