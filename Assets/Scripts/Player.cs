using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField]
    private readonly float jumpForce = 5f;
    public bool grounded = false;
    private bool resetJumpNeeded = false;
    [SerializeField]
    private float speed = 2.5f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        CheckGrounded();
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(move * speed, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            grounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeeded());
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);

        if (hitInfo.collider != null)
        {
            if (!resetJumpNeeded)
            {
                grounded = true;
            }
        }
    }

    private IEnumerator ResetJumpNeeded()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}
