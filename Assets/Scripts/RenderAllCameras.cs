using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAllCameras : MonoBehaviour
{
    private RenderFromCamera[] cameraRenderComponents;

    private void Awake()
    {
        cameraRenderComponents = FindObjectsOfType<RenderFromCamera>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var renderComponent in cameraRenderComponents)
            {
                renderComponent.CreateRender();
            }
        }
    }
}
