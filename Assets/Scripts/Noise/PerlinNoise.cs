using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public float layerFactor = 0.2f;

    private void Start()
    {
        // RANDOMIZE THE OFFSET FOR RANDOM NOISE
        Renderer renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float[,] firstLayer = GenerateNoiseLayer(width, height, scale, Modes.Perlin);
        float[,] secondLayer = GenerateNoiseLayer(width, height, scale, Modes.Ridgid, ridgidPower = 3f);

        GetComponent<Renderer>().material.mainTexture = GenerateTexture(GenerateLayeredNoise(firstLayer, secondLayer, layerFactor, width, height));
    }

    public float[,] GenerateNoiseLayer(int width, int height, float scale, Modes mode, float offsetX = 100, float offsetY = 100, float ridgidPower = 3f)
    {
        switch (mode)
        {
            case Modes.Perlin:
                return GeneratePerlinNoiseLayer(width, height, scale, offsetX, offsetY);
            case Modes.Ridgid:
                return GenerateRidgidNoiseLayer(width, height, scale, ridgidPower, offsetX, offsetY);
            default:
                float[,] noiseLayer = new float[width, height];
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        noiseLayer[x, y] = 0f;
                    }
                }
                return noiseLayer;
        }
    }

    public float[,] GeneratePerlinNoiseLayer(int width, int height, float scale, float offsetX = 100, float offsetY = 100)
    {
        float[,] noiseLayer = new float[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                noiseLayer[x, y] = sample;
            }
        }
        return noiseLayer;
    }

    public float[,] GenerateRidgidNoiseLayer(int width, int height, float scale, float ridgidScale, float offsetX = 100, float offsetY = 100)
    {
        float[,] noiseLayer = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                sample = Utils.Remap(sample, 0, 1, -1, 1); // Remaps the value from range (0-1) to range (-1 to 1)
                sample = 1f - Mathf.Abs(sample);
                sample = Mathf.Pow(sample, ridgidPower);
                noiseLayer[x, y] = sample;
            }
        }
        return noiseLayer;
    }

    float[,] GenerateLayeredNoise(float[,] baseLayer, float[,] additionLayer, float factor, int width, int height)
    {
        return Utils.Add2DFloatArray(baseLayer, Utils.Multiply2DFloatArray(additionLayer, factor));
    }

    Texture2D GenerateTexture(float[,] noiseLayer)
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

    Color CalculatePointColour(float[,] noiseLayer, int x, int y)
    {
        return new Color(noiseLayer[x, y], noiseLayer[x, y], noiseLayer[x, y]);
    }
}
