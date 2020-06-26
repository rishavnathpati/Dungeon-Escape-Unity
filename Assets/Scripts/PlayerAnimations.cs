using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;


    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Move(float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        anim.SetBool("Jumping", jump);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
