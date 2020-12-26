using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public bool maintainWidth = true;
    [Range(-1,1)]
    public int adaptPosition;

    float defaultWidth;
    float defaultHeight;

    Vector3 CameraPos;
    // Start is called before the first frame update
    void Start()
    {
        CameraPos = Camera.main.transform.position;

        defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
        defaultHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (maintainWidth)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

            Camera.main.transform.position = new Vector3(CameraPos.x, 1 * (defaultHeight - Camera.main.orthographicSize), CameraPos.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(adaptPosition * (defaultWidth - Camera.main.orthographicSize * Camera.main.aspect), CameraPos.y, CameraPos.z);
        }
        
    }
}
