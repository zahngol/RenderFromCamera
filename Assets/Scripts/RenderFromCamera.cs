using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RenderFromCamera : MonoBehaviour
{
    private Camera cameraComponent;
    
    private int resWidth = 1920;
    private int resHeight = 1080;
        
    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = GetComponent<Camera>();

        if (cameraComponent.targetTexture == null)
        {
            cameraComponent.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }

        cameraComponent.enabled = false;
    }

    public void CreateRender()
    {
        cameraComponent.enabled = true;
    }

    private void LateUpdate()
    {
        if (cameraComponent.enabled)
        {
            Texture2D texture = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            cameraComponent.Render();
            RenderTexture.active = cameraComponent.targetTexture;
            texture.ReadPixels(new Rect(0,0,resWidth, resHeight), 0,0);
            byte[] bytes = texture.EncodeToPNG();
            string filename = BuildFileName();
            System.IO.File.WriteAllBytes(filename, bytes);
            cameraComponent.enabled = false;
        }
    }

    private string BuildFileName()
    {
        return $"{Application.dataPath}/Snapshots/{gameObject.name} {DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
    }
}
