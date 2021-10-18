using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraSettings : MonoBehaviour
{
    [SerializeField]
    DepthTextureMode depthTextureMode = DepthTextureMode.Depth;
    void OnEnable()
    {
        Camera.main.depthTextureMode = depthTextureMode;
    }
}
