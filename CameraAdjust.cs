using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraAdjust : MonoBehaviour
{
    // SOURCE:
    // https://gamedev.stackexchange.com/questions/167317/scale-camera-to-fit-screen-size-unity


    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 7;

    Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
        if (PlayerPrefs.HasKey("Rcolor"))
        {
            _camera.backgroundColor = new Color(PlayerPrefs.GetFloat("Rcolor"), PlayerPrefs.GetFloat("Gcolor"), PlayerPrefs.GetFloat("Bcolor"), 1);
        }
        else
        {
            _camera.backgroundColor = new Color(0.376f, 0.51f, 0.714f, 1);        // set background to Glaucous by default
        }

    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;

    }

    public void ChangeBackground(Color newColor)
    {
        _camera.backgroundColor = newColor;
    }
}