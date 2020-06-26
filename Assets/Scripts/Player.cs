using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField]
    private readonly float jumpForce = 5f;
    public bool grounded = false;
    private bool resetJumpNeeded = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            grounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeeded());
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.green);

        if (hitInfo.collider != null)
        {
            if (!resetJumpNeeded)
            {
                grounded = true;
            }
        }

        rigid.velocity = new Vector2(move, rigid.velocity.y);
    }

    private IEnumerator ResetJumpNeeded()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}
