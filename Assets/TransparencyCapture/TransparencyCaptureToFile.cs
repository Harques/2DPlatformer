using System.Collections;
using UnityEngine;

public class TransparencyCaptureToFile : MonoBehaviour
{
    private int fpsCounter = 0;

    public IEnumerator capture()
    {

        yield return new WaitForEndOfFrame();
        //After Unity4,you have to do this function after WaitForEndOfFrame in Coroutine
        //Or you will get the error:"ReadPixels was called to read pixels from system frame buffer, while not inside drawing frame"
        zzTransparencyCapture.captureScreenshot(fpsCounter.ToString() + ".png");
    }

    public void Update()
    {
        fpsCounter++;
        if (fpsCounter <= 15)
            StartCoroutine(capture());
    }
}