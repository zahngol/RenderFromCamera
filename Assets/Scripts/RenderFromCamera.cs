using UnityEngine;

/// Component placed on a camera that can be used to create a Texture2D of the camera's view
[RequireComponent(typeof(Camera))]
public class RenderFromCamera : MonoBehaviour
{
    // The camera component for this game object
    private Camera cameraComponent;
    
    // A texture 2D to be used as a storage location for camera renders
    private Texture2D texture;

    // These will probably be stored somewhere else in the future
    private int resWidth = 1920;
    private int resHeight = 1080;
    
    void Start()
    {
        // Cache the camera component
        cameraComponent = GetComponent<Camera>();

        // If there is no RenderTexture assigned to the target texture, create one
        if (cameraComponent.targetTexture == null)
        {
            cameraComponent.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }

        // Disable the camera component until it is required to render an image
        // Disabling the component rather than the object so that scripts attached to the object will still function
        cameraComponent.enabled = false;
    }

    public Texture2D CreateRender()
    {
        // Enable the camera component
        cameraComponent.enabled = true;
        
        // Reset the texture properties to make sure it matches the desired resolution
        texture = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        
        // Force the camera to render
        cameraComponent.Render();
        
        // Update the RenderTexture with the camera's targetTexture
        RenderTexture.active = cameraComponent.targetTexture;
        
        // Reaad the pixels from the render texture into the Texture2D
        texture.ReadPixels(new Rect(0,0,resWidth, resHeight), 0,0);
        
        // Disable the camera component until it's needed again
        cameraComponent.enabled = false;

        // Return the texture
        return texture;
    }
}
