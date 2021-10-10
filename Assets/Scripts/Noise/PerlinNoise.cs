using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    private void Start()
    {
        // RANDOMIZE THE OFFSET FOR RANDOM NOISE
        Renderer renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        GetComponent<Renderer>().material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        // GENERATE TEXTURE NOISE
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color colour = CalculateColour(x, y);
                //Color colour = CalculateRidgedColour(x, y);
                texture.SetPixel(x, y, colour);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColour(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }

    Color CalculateRidgedColour(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        sample = sample - 0.5f; // Center values around 0 (-0.5 -> 0.5)
        sample = 1f - Mathf.Abs(sample);
        sample = sample * sample;

        return new Color(sample, sample, sample);
    }
}
