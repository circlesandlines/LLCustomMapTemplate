using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOnTrigger : MonoBehaviour
{
    public Animator an;
    public AudioSource aso;
    public string objectTriggerName;
    public string animatedObjectName;
    public string anim;
    public bool activated = false;

    private void Start()
    {
        if (!aso)
            aso = GetComponent<AudioSource>();

        an = GameObject.Find(animatedObjectName).GetComponent<Animator>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == objectTriggerName && !activated)
        {

            if (aso)
                aso.Play();

            activated = true;

            an.CrossFade(anim, 0.5f, 0);
        }
    }
}
