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
    private readonly float speed = 2.5f;
    private SpriteRenderer playerSprite;
    private SpriteRenderer swordArcSprite;

    private PlayerAnimations playerAnim;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimations>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
        CheckGrounded();
        if (Input.GetMouseButtonDown(0) && grounded)
        {
            playerAnim.Attack();
        }
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (move > 0)
        {
            Flip(false);
        }
        else if (move < 0)
        {
            Flip(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            grounded = false;
            playerAnim.Jump(true);
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeeded());
        }

        rigid.velocity = new Vector2(move * speed, rigid.velocity.y);
        playerAnim.Move(move);
    }

    private void Flip(bool faceRight)
    {
        if (!faceRight)
        {
            playerSprite.flipX = false;
            swordArcSprite.flipX = false;
            swordArcSprite.flipY = false;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight)
        {
            playerSprite.flipX = true;
            swordArcSprite.flipX = true;
            swordArcSprite.flipY = true;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            swordArcSprite.transform.localPosition = newPos;
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
                playerAnim.Jump(false);
            }
        }
    }

    private IEnumerator ResetJumpNeeded()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}
