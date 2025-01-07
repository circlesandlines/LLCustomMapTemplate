using UnityEngine;

public class RandomAnimationOffset : MonoBehaviour
{
    public Animator animator;
    public float maxOffset = 1f; // Maximum offset time in seconds

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        float randomOffset = Random.Range(0f, maxOffset);
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, randomOffset);
    }
}
