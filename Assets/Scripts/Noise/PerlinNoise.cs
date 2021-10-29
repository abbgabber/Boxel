using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PerlinNoise
{
    public enum Modes { Perlin, Ridgid };

    public static float[,] GenerateNoiseLayer(int width, int height, float frequency, Modes mode, float amplitude, float offsetX = 100, float offsetY = 100, float ridgidPower = 3f)
    {
        switch (mode)
        {
            case Modes.Perlin:
                return GeneratePerlinNoiseLayer(width, height, frequency, amplitude, offsetX, offsetY);
            case Modes.Ridgid:
                return GenerateRidgidNoiseLayer(width, height, frequency, ridgidPower, offsetX, offsetY);
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

    public static float[,] GeneratePerlinNoiseLayer(int width, int height, float frequency, float amplitude, float offsetX = 100, float offsetY = 100)
    {
        float[,] noiseLayer = new float[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * frequency + offsetX;
                float yCoord = (float)y / height * frequency + offsetY;

                float sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
                noiseLayer[x, y] = sample;
            }
        }
        return noiseLayer;
    }

    public static float[,] GenerateRidgidNoiseLayer(int width, int height, float frequency, float ridgidfrequency, float offsetX = 100, float offsetY = 100, float ridgidPower = 3f)
    {
        float[,] noiseLayer = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * frequency + offsetX;
                float yCoord = (float)y / height * frequency + offsetY;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                sample = Utils.Remap(sample, 0, 1, -1, 1); // Remaps the value from range (0-1) to range (-1 to 1)
                sample = 1f - Mathf.Abs(sample);
                sample = Mathf.Pow(sample, ridgidPower);
                noiseLayer[x, y] = sample;
            }
        }
        return noiseLayer;
    }

    public static float[,] GenerateLayeredNoise(float[,] baseLayer, float[,] additionLayer, float factor, int width, int height)
    {
        return Utils.Add2DFloatArray(baseLayer, Utils.Multiply2DFloatArray(additionLayer, factor));
    }
}
