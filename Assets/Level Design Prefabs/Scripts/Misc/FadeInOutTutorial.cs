using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInOutTutorial : MonoBehaviour
{
    public TextMesh[] tutorialText;
    public TextMeshProUGUI[] TMPGUITutorialText;
    public TextMeshPro[] TMPTutorialText;
    public MeshRenderer[] tutorialGraphics;
    public float fadeRate = 1f;
    public bool _active = false;
    private bool fadeAllowed = true;


    // Start is called before the first frame update
    void Start()
    {
        foreach (MeshRenderer m in tutorialGraphics)
            m.material.color = new Color(m.material.color.r, m.material.color.g, m.material.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fadeAllowed)
            return;

        fadeAllowed = false;

        if (_active)
        {
            foreach (TextMesh tm in tutorialText)
            {
                if (tm.color.a < 1f)
                {
                    tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a + (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }
            }

            foreach (TextMeshProUGUI tm in TMPGUITutorialText)
            {
                if (tm.color.a < 1f)
                {
                    tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a + (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }
            }

            foreach (TextMeshPro tm in TMPTutorialText)
            {
                if (tm.color.a < 1f)
                {
                    tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a + (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }
            }

            foreach (MeshRenderer m in tutorialGraphics)
            {
                if (m.material.color.a < 1f)
                {
                    m.material.color = new Color(m.material.color.r, m.material.color.g, m.material.color.b, m.material.color.a + (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }

                if (m.material.color.a > 0)
                    m.enabled = true;
            }
        }
        else
        {
            foreach (TextMesh tm in tutorialText)
            {
                if (tm.color.a > 0)
                {
                    tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a - (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }
            }

            foreach (TextMeshProUGUI tm in TMPGUITutorialText)
            {
                if (tm.color.a > 0)
                {
                    tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a - (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }
            }

            foreach (TextMeshPro tm in TMPTutorialText)
            {
                if (tm.color.a > 0)
                {
                    tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a - (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }
            }

            foreach (MeshRenderer m in tutorialGraphics)
            {
                if (m.material.color.a > 0)
                {
                    m.material.color = new Color(m.material.color.r, m.material.color.g, m.material.color.b, m.material.color.a - (fadeRate * Time.deltaTime));
                    fadeAllowed = true;
                }

                if (m.material.color.a <= 0)
                    m.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            _active = true;
            fadeAllowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            _active = false;
            fadeAllowed = true;
        }
    }
}
