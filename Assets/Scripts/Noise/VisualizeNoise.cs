using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public PerlinNoise.Modes mode = PerlinNoise.Modes.Perlin;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float ridgidPower = 3f;
    public float layerFactor = 0.2f;


    private void Start()
    {
        // RANDOMIZE THE OFFSET FOR RANDOM NOISE
        Renderer renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float[,] firstLayer = PerlinNoise.GenerateNoiseLayer(width, height, scale, PerlinNoise.Modes.Perlin, offsetX, offsetY);
        /*
        float[,] secondLayer = PerlinNoise.GenerateNoiseLayer(width, height, scale, PerlinNoise.Modes.Ridgid, ridgidPower = 3f);
        */
        float[,] simplexNoise = new float[height,width];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                simplexNoise[x, y] = SimplexNoise.Noise.Generate(x, y);
            }
        }
        
        GetComponent<Renderer>().material.mainTexture = GenerateTexture(firstLayer);
    }

    public Texture2D GenerateTexture(float[,] noiseLayer)
    {
        Texture2D texture = new Texture2D(width, height);

        // GENERATE TEXTURE NOISE
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color colour = CalculatePointColour(noiseLayer, x, y);
                texture.SetPixel(x, y, colour);
            }
        }
        texture.Apply();
        return texture;
    }

    public static Color CalculatePointColour(float[,] noiseLayer, int x, int y)
    {
        return new Color(noiseLayer[x, y], noiseLayer[x, y], noiseLayer[x, y]);
    }
}
