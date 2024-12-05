using UnityEngine;
using UnityEngine.UI;

public class DepthMapVisualizer : MonoBehaviour
{
    public Camera leftCamera;            
    public Camera rightCamera;         
    public RenderTexture leftDepthTexture;  
    public RenderTexture rightDepthTexture; 
    public RawImage depthMapDisplay;    

    private Texture2D depthMapTexture;

    void Start()
    {
        // Initialize Texture2D to hold the combined depth map
        depthMapTexture = new Texture2D(leftDepthTexture.width, leftDepthTexture.height, TextureFormat.RGBA32, false);
    }

    void Update()
    {
        UpdateDepthMap();
    }

    void UpdateDepthMap()
    {
        CopyDepthToTexture2D(leftDepthTexture, depthMapTexture); //copy depth data from left to depth map
        Texture2D leftDepthTex2D = new Texture2D(depthMapTexture.width, depthMapTexture.height, TextureFormat.RFloat, false);
        CopyDepthToTexture2D(rightDepthTexture, leftDepthTex2D); //copy depth data from right to leftDepthTex2D 

        int width = depthMapTexture.width;
        int height = depthMapTexture.height;

        // Combine depth values from both cameras for visualization
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Get depth values from both left and right camera depth textures
                float leftDepth = depthMapTexture.GetPixel(x, y).r;  
                float rightDepth = leftDepthTex2D.GetPixel(x, y).r; 

                // Average the depth values from both cameras 
                float combinedDepth = Mathf.Lerp(leftDepth, rightDepth, 0.5f); // You could adjust the mix ratio

                // Normalize the depth value to range [0, 1] for RGB visualization
                float normalizedDepth = Mathf.InverseLerp(0f, 1f, combinedDepth);  // Normalize the depth range

                Color depthColor = GetShadedColorFromDepth(normalizedDepth);

                // Set the RGB color into the texture
                depthMapTexture.SetPixel(x, y, depthColor);
            }
        }

        depthMapTexture.Apply(); //apply changes 

        depthMapDisplay.texture = depthMapTexture; //display texture 
    }

    // Function to generate a shaded color gradient 
    //red--> far
    //blue --> close 
    Color GetShadedColorFromDepth(float normalizedDepth)
    {
        if (normalizedDepth < 0.25f)
        {
            return Color.Lerp(new Color(0.5f, 0f, 0f), Color.red, normalizedDepth * 4f); //red gradients 
        }
        else if (normalizedDepth < 0.5f)
        {
            return Color.Lerp(new Color(0.8f, 0.4f, 0f), new Color(1f, 0.5f, 0f), (normalizedDepth - 0.25f) * 4f);  //orange gradients 
        }
        else if (normalizedDepth < 0.75f)
        {
            return Color.Lerp(new Color(0.1f, 0.5f, 0f), Color.green, (normalizedDepth - 0.5f) * 4f);  // green gradients 
        }
        else
        {
            return Color.Lerp(new Color(0f, 0.1f, 0.5f), Color.blue, (normalizedDepth - 0.75f) * 4f);  // blue gradients 
        }
    }

    void CopyDepthToTexture2D(RenderTexture renderTexture, Texture2D texture2D)
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = renderTexture;

        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply(); // Apply the changes to the Texture2D

        RenderTexture.active = currentRT;
    }
}
