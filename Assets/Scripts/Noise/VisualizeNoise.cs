using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public PerlinNoise.Modes mode = PerlinNoise.Modes.Perlin;


    public float offsetX = 100f;
    public float offsetY = 100f;
    [Header("Seed")]
    public int seed = 1;
    [Header("Layered noise stuff")]
    public int octaves = 1;
    [Range(1,100)]
    public float lacunarity = 20f;
    [Range(0,1)]
    public float persistance = .5f;
    [Header("Ridgid noise stuff")]
    public float ridgidPower = 3f;
    public float layerFactor = 0.2f;


    private void Start()
    {
        // RANDOMIZE THE OFFSET FOR RANDOM NOISE
        Renderer renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        //float[,] firstLayer = PerlinNoise.GenerateNoiseLayer(width, height, lacunarity, mode, offsetX, offsetY);

        /*
        float[,] simplexNoise = new float[height,width];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                simplexNoise[x, y] = SimplexNoise.Noise.Generate(x, y);
            }
        }

        height += Mathf.PerlinNoise(child.transform.position.x/(noiseScale*2) + (seed * 10), child.transform.position.z/(noiseScale*2) + (seed * 10), seed);

        */
        float[,] layeredNoise = new float[width,height];
        for (int i = 0; i < octaves; i++)
        {
            float freq = Mathf.Pow(lacunarity, i);
            float amp = Mathf.Pow(persistance, i);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    layeredNoise[x, y] += Mathf.PerlinNoise(x / (freq), y / (freq)) * amp;
                }
            }
        }

        /*
         TODO: Lerp between ridgid noise maps and the above map to create mountain chains? alt: look up how the fuck to do it
         */
        
        GetComponent<Renderer>().material.mainTexture = GenerateTexture(layeredNoise);
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
