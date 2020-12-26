using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public LayerMask platformLayerMask;
    private Rigidbody2D rb;
    public float speed;
    private bool[] doublejump = { true, true };
    private bool rightMovement = false;
    private bool leftMovement = false;
    public float gravity = 1f;
    public float jumpVelocity = 100f;
    public Animator playerAnimator;
    private Transform tf;
    private bool flipped = false;
    public GameObject player;
    public Transform startPoint;
    public BoxCollider2D boxCollider2D;
    public ParticleSystem deathParticle;
    private bool movePlatform;
    private float movePlatformSpeed;
    private bool duckBool = false;
    private bool jumpBool = false;
    private bool resized = false;




    public void Jump()
    {
        jumpBool = true;

    }
    public void DuckOnPress()
    {
        duckBool = true;
        playerAnimator.SetBool("DuckBool", true);
        leftMovement = false;
        rightMovement = false;
        /*Vector2 location = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = location;*/
    }
    public void DuckOnRelease()
    {
        duckBool = false;
        playerAnimator.SetBool("DuckBool", false);
        /*Vector2 location = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = location;*/
    }
    public void setMovePlatform(bool move)
    {
        movePlatform = move;
    }
    public void setMovePlatformSpeed(float speed)
    {
        movePlatformSpeed = speed;
    }
    public bool getLeft()
    {
        return leftMovement;
    }
    public bool getRight()
    {
        return rightMovement;
    }
    public void LeftOnPress()
    {
        if (!duckBool)
            leftMovement = true;
    }
    public void LeftOnRelease()
    {
        leftMovement = false;
    }
    public void RightOnPress()
    {
        if (!duckBool)
            rightMovement = true;

    }
    public void RightOnRelease()
    {
        rightMovement = false;
    }
    void Start()
    {

        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (duckBool)
        {
            if (!resized)
            {
                Invoke("Resize", 0.10f);
                resized = true;
            }
        }
        if (!duckBool)
        {
            if (resized)
            {
                Invoke("Resize", 0.0f);
                resized = false;
            }
        }
    }
    private void DuckBoolChanger()
    {
        playerAnimator.SetBool("DuckBool", false);
    }
    private void Resize()
    {
        Vector2 location = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = location;
        if (resized)
        {
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, 1.315147f);
            boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, -0.003656238f);
        }
    }
    void Update()
    {
        if (jumpBool)
        {
            if (doublejump[0] == true && !duckBool) 
            {
                playerAnimator.SetBool("JumpingBool", true);
                doublejump[0] = false;
                rb.velocity = Vector2.up * jumpVelocity;
                jumpBool = false;

            }
            else if (doublejump[0] == false && doublejump[1] == true)
            {
                Debug.Log("Double Jump");
                doublejump[1] = false;
                playerAnimator.SetBool("JumpingBool", true);
                rb.velocity = Vector2.up * jumpVelocity;
                jumpBool = false;

            }
        }
        playerAnimator.SetFloat("JumpingSpeed", rb.velocity.y);
        Vector2 forces2 = new Vector2(0, 1);
        if (Input.GetKeyDown("w"))
        {
            Debug.Log("Bastın");
            if (doublejump[0] == true && !duckBool)
            {
                playerAnimator.SetBool("JumpingBool", true);
                doublejump[0] = false;
                rb.velocity = forces2 * jumpVelocity;


            }
            else if (doublejump[0] == false && doublejump[1] == true)
            {
                playerAnimator.SetBool("JumpingBool", true);
                Debug.Log("Double Jump");
                doublejump[1] = false;
                rb.velocity = forces2 * jumpVelocity;

            }
        }
        else if (IsGrounded() && playerAnimator.GetFloat("JumpingSpeed") == 0)
        {
            playerAnimator.SetBool("JumpingBool", false);
            doublejump[0] = true;
            doublejump[1] = true;
        }
        if (Input.GetKeyDown("d") && !(Input.GetKey("s")))
            rightMovement = true;
        else if (Input.GetKeyUp("d"))
            rightMovement = false;
        if (Input.GetKeyDown("a") && !(Input.GetKey("s")))
            leftMovement = true;
        else if (Input.GetKeyUp("a"))
            leftMovement = false;
        if (Input.GetKeyDown("s"))
        {
            duckBool = true;
            playerAnimator.SetBool("DuckBool", true);
            leftMovement = false;
            rightMovement = false;
        }
        else if (Input.GetKeyUp("s"))
        {
            duckBool = false;
            playerAnimator.SetBool("DuckBool", false);
            if (Input.GetKey("a"))
                leftMovement = true;
            if (Input.GetKey("d"))
                rightMovement = true;
        }


    }
    void FixedUpdate()
    {
        if (rightMovement)
        {

            playerAnimator.SetFloat("RunningSpeed", speed);
            if (flipped)
            {
                flipped = false;
                tf.Rotate(Vector3.up * 180);
            }
            if (movePlatform)
                rb.velocity = new Vector2(speed + movePlatformSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (leftMovement)
        {
            playerAnimator.SetFloat("RunningSpeed", speed);
            if (!flipped)
            {
                flipped = true;
                tf.Rotate(Vector3.up * 180);
            }
            if (movePlatform)
                rb.velocity = new Vector2(-speed + movePlatformSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (!leftMovement && !rightMovement)
        {
            playerAnimator.SetFloat("RunningSpeed", 0);
            if (movePlatform)
                rb.velocity = new Vector2(movePlatformSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    private void Respawn()
    {
        leftMovement = false;
        rightMovement = false;
        player.transform.position = startPoint.position;
        player.transform.rotation = startPoint.rotation;
        flipped = false;
        player.SetActive(true);
    }

    private bool IsGrounded()
    {
        float extraHeightText = .04f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
            rayColor = Color.green;
        else
            rayColor = Color.red;
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.x), rayColor);

        return raycastHit.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Actually collides though.");
            if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex+1)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Fall")
        {
            Debug.Log(deathParticle.isPlaying);

            deathParticle.Play();
            Debug.Log(deathParticle.isEmitting);
            Debug.Log(deathParticle.isPlaying);
            player.SetActive(false);
            Invoke("Respawn", 2f);
        }

    }
}
