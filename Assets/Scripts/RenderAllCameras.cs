using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Temporary implementation of the RenderFromCamera class
public class RenderAllCameras : MonoBehaviour
{
    // Array that will hold all RenderFromCamera components in the scene
    private RenderFromCamera[] cameraRenderComponents;

    private void Awake()
    {
        // Cache all of the RenderFromCamera components 
        cameraRenderComponents = FindObjectsOfType<RenderFromCamera>();
    }
    
    private void Update()
    {
        // When the space bar is pressed, create a render for each of the cameras with the RenderFromCamera component
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (RenderFromCamera renderComponent in cameraRenderComponents)
            {
                // Get a Texture2D from the RenderFromCamera component
                Texture2D temp = renderComponent.CreateRender();

                WritePNGToFile(
                    temp,
                    $"{Application.dataPath}/Snapshots/{renderComponent.gameObject.name} {System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png"
                    );
            }
        }
    }

    private void WritePNGToFile(Texture2D texture, string path)
    {
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
    }
}
