using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayeredNoise
{
    public static float GenerateNoise(int x, int z, float scale, float roughness)
    {
        return SimplexNoise.Noise.Generate(x * scale, z * scale) * roughness;
    }

    /*
     Generate noise maps such that octaves equalt amount of layers.
     frequency = lacunarity^(n-1) // Zero for start map, value larger than 1.
     amplitude = persistance^(n-1) // Value between 0 and 1.
    */
}
