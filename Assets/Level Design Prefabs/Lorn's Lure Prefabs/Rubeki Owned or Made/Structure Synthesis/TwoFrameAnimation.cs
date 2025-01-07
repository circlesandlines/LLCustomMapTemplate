using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoFrameAnimation : MonoBehaviour
{
    public Material mat;

    public Vector2 UVOffset0;
    public Vector2 UVOffset1;

    public float randomStartOffsetRange = 3f;

    public float playFrameAfterSeconds = 1.5f;

    public bool started = false;
    public bool playNextFrame = true;

    public int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        StartCoroutine(PlayFrame(randomStartOffsetRange));
    }

    IEnumerator PlayFrame(float offset = 0)
    {
        if (offset != 0)
            yield return new WaitForSeconds(playFrameAfterSeconds + Random.Range(0, offset));
        else
            yield return new WaitForSeconds(playFrameAfterSeconds);

        if (frame == 0)
        {
            mat.mainTextureOffset = UVOffset0;
            frame = 1;
        }
        else
        {
            mat.mainTextureOffset = UVOffset1;
            frame = 0;
        }

        StartCoroutine(PlayFrame());
    }
}
