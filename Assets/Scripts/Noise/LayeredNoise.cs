using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayeredNoise
{
    public static float GenerateNoise(int x, int z, float scale, float roughness)
    {
        return SimplexNoise.Noise.Generate(x * scale, z * scale) * roughness;
    }
}
