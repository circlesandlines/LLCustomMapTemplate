using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard3D : MonoBehaviour
{
    public GameObject player;
    public bool _override = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Climber");//.GetComponent<ClimbingAbilityV2>().defaultCamera; < don't care about actual implementation. Calling Scene namespace will be used
    }

    // Update is called once per frame
    void Update()
    {
        if (_override)
        {
            if (!player)
            {
                player = GameObject.Find("Climber");
                return;
            }

            transform.LookAt(player.transform.position);

            /*
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            eulerAngles.x = 90;
            eulerAngles.y = 90;
            transform.eulerAngles = eulerAngles;*/
        }
        else
        {
            transform.LookAt(player.transform.position, transform.position + (transform.position - player.transform.position));
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 180;
            eulerAngles.z = 0;
            eulerAngles.y = 0;
            transform.eulerAngles = eulerAngles;
        }
    }
}
