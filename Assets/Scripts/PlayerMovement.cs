using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    private Animator anim; 
    private Collider2D coll;
 
    //FSM
    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;
   
    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;

    // Enemy Component
    private Enemy enemyComponent;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        UIPermanente.perm.healthAmount.text = UIPermanente.perm.health.ToString();
    }
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        Movement();

        VelocityState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            UIPermanente.perm.cherries += 1;
            UIPermanente.perm.cherryText.text = UIPermanente.perm.cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        { 
            enemyComponent = collision.gameObject.GetComponent<Enemy>();
            if(state == State.falling)
            {
                if (collision.gameObject.GetComponent<Enemy>())
                {
                    enemyComponent.JumpedOn();
                }
                else
                {
                    Debug.Log("Choque con algo pero me falta el componente", collision.gameObject);
                }
                Jump();
            }

            else
            {
                state = State.hurt;
                HandleHealth();
                if(collision.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therfore i should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }

                else
                {
                    //Enemy is to my right therfore i should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void HandleHealth()
    {
        UIPermanente.perm.health -= 1;
        UIPermanente.perm.healthAmount.text = UIPermanente.perm.health.ToString();
        if (UIPermanente.perm.health <= 0)
        {
            UIPermanente.perm.Reset();
            SceneManager.LoadScene("DeathScene");
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
       
        //Moving Left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //Moving Right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }


    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }
    private void VelocityState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }

        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }
}