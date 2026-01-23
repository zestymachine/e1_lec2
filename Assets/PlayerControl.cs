using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;
    bool isGrounded;
    //int score=0;

    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        spriteRenderer=GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float movementDistanceX = movementX * speed * Time.deltaTime;
        //float movementDistanceY = movementY * speed * Time.deltaTime;
        //transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y+movementDistanceY);
        rb.linearVelocity=new Vector2(movementX*speed, rb.linearVelocity.y);
        if (!Mathf.Approximately(movementX, 0f))
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX=movementX<0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (movementY>0 && isGrounded)
        {
            rb.AddForce(new Vector2(0,100));
        }
    }
    void OnMove(InputValue value)
    {
        Vector2 v= value.Get<Vector2>();
        movementX=v.x;
        movementY=v.y;
        Debug.Log("Movement X = " + movementX);
        Debug.Log("Movement Y = " + movementY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded=true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded=false;
        }
    }


}
