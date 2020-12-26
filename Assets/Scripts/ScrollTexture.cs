using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    private Material material;
    private float currentScroll;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;    
    }

    // Update is called once per frame
    void Update()
    {
        currentScroll += speed * Time.deltaTime;
        material.mainTextureOffset = new Vector2 (currentScroll,0);
    }
}
