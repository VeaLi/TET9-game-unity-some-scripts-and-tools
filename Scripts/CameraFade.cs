using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is for fading between the scenes
public class CameraFade : MonoBehaviour
{
    // Start is called before the first frame update
    public Color color1;
    public Color color2;
    public float duration = 4.0F;

    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(color1, color2, t);
    }
}