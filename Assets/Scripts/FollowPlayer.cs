using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform tf;
    public Transform player;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.position = new Vector3(player.position.x, player.position.y, player.position.z);
    }
}
