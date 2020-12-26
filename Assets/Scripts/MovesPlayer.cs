using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesPlayer : MonoBehaviour
{
    public PlayerMovement pm;
    public float platformSpeed;
    private void Start()
    {
        pm.setMovePlatformSpeed(platformSpeed);
    }

    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            pm.setMovePlatform(true);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pm.setMovePlatform(false);
        }
    }

}
