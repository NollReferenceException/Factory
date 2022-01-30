using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    [Range(20, 240)][SerializeField]private int framerate = 60;
    public int Framerate { get => framerate; set => framerate = value; }

    void Awake()
    {
        Application.targetFrameRate = framerate;
    }
}
