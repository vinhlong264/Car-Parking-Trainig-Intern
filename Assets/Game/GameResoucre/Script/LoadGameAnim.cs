using UnityEngine;

public class LoadGameAnim : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeAnimtion()
    {
        animator.SetBool("transition", true);
    }

    public void ResetTrigger()
    {
        animator.SetBool("transition" , false);
    }


}
