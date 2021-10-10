using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public enum Modes { Perlin, Ridgid };
    public Modes mode = Modes.Perlin;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float ridgidPower = 3f;


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
                Color colour = GetColour(x, y);
                texture.SetPixel(x, y, colour);
            }
        }
        texture.Apply();
        return texture;
    }

    Color GetColour(int x, int y)
    {
        switch (mode)
        {
            case Modes.Perlin:
                return CalculateColour(x, y);
            case Modes.Ridgid:
                return CalculateRidgedColour(x, y);
        }
        return Color.blue;
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

        sample = Utils.Remap(sample, 0, 1, -1, 1); // Remaps the value from range (0-1) to range (-1 to 1)
        sample = 1f - Mathf.Abs(sample);
        sample = Mathf.Pow(sample, ridgidPower);

        return new Color(sample, sample, sample);
    }
}
