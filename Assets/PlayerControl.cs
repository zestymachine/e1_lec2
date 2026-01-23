using UnityEngine;
//import input system 
using UnityEngine.InputSystem;


public class newScript : MonoBehaviour
{
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f; 
    [SerializeField] float jumpForce = 25f;
    int jumpCount = 0; 
    [SerializeField] int maxJumps = 2; 
    Rigidbody2D rb; 
    bool isGrounded = false;
    int score = 0; 
    Animator animator; 
    SpriteRenderer spriteRenderer; 
    bool jumpPressed = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMove(InputValue value) { 
        //first part of vector 2 is value you're moving it to 
        //+1 is to right, -1 to left
        Vector2 v = value.Get<Vector2>(); 
        movementX = v.x; 
        movementY = v.y; 
        Debug.Log("Movement X = " + movementX);
        Debug.Log("Movement Y = " + movementY);
        

    }

    void OnJump(InputValue value) { 
        if (value.isPressed) { 
            jumpPressed = true; 
        }
    }
    //moves according to tick assigned in serialized field 
    void FixedUpdate() { 
        //because global, can take from any function 
        // float movementDistanceX = movementX * speed * Time.deltaTime; 
        // float movementDistanceY = movementY * speed * Time.deltaTime; 
        // transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
        // rb.linearVelocity = new Vector2(movementX * speed, movementY * speed); 
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);

        //jumping code 
      
        // if (movementY >0 && isGrounded) 
        // { 
        //     rb.AddForce(new Vector2(0,100)); 
        //     jumpCount+=1; 
        //     animator.SetBool("isJumping", true); 
        
        // }
        // jumping code (FixedUpdate)
        if (jumpPressed && jumpCount < maxJumps){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++;
            animator.SetBool("isJumping", true);
        }

        // prevent repeated jumps while holding input
        jumpPressed = false; 
        // else {
        //     animator.SetBool("isJumping", false);
        // }

        //is movement X close to 0? close to 0 -> player is not moving 
        //if movement x is not 0, set isRunning bool to true 
        if (!Mathf.Approximately(movementX, 0f)) { 
            animator.SetBool("isRunning", true);

            //flipX requires bool, if facing left flip the value
            //determine if facing left thru movementX is left, facing left  
            //set flipX to true 
            spriteRenderer.flipX = movementX < 0; 

        }
      
        else { 
            animator.SetBool("isRunning", false);
        }

    
    }
    private void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject.CompareTag("Ground")) { 
            isGrounded = true; 
            //reset jumps 
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) { 
        if (collision.gameObject.CompareTag("Ground")) { 
            isGrounded = false; 
        }
    }

    void OnTriggerEnter2D(Collider2D collision) { 
        //check if tag is collectible 
        if (collision.gameObject.CompareTag("Collectible")) { 
            //incrementing score 
            score +=1;  
            // Destroy(collision.gameObject); 
            collision.gameObject.SetActive(false); 
            Debug.Log("Score: " + score); 

        }

    }


}
