using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private Animator swordAnim;

    public GameObject swordArc;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        swordAnim = swordArc.GetComponent<Animator>();
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
        swordAnim.SetTrigger("Sword Arc");
    }
}