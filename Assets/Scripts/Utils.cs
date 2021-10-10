public static class Utils
{
    public static float Remap(float value, float minValue, float maxValue, float newRangeMin, float newRangeMax)
    {
        return (value - minValue) / (maxValue - minValue) * (newRangeMax - newRangeMin) + newRangeMin;
    }

    public static float[,] Multiply2DFloatArray(float[,] noiseLayer, float factor)
    {
        for (int x = 0; x < noiseLayer.GetLength(0); x++)
        {
            for (int y = 0; y < noiseLayer.GetLength(1); y++)
            {
                noiseLayer[x, y] = noiseLayer[x, y] * factor;
            }
        }
        return noiseLayer;
    }

    public static float[,] Multiply2DFloatArray(float[,] noiseLayer, float[,] secondNoiseLayer)
    {
        for (int x = 0; x < noiseLayer.GetLength(0); x++)
        {
            for (int y = 0; y < noiseLayer.GetLength(1); y++)
            {
                noiseLayer[x, y] = noiseLayer[x, y] * secondNoiseLayer[x,y];
            }
        }
        return noiseLayer;
    }

    public static float[,] Add2DFloatArray(float[,] noiseLayer, float[,] secondNoiseLayer)
    {
        for (int x = 0; x < noiseLayer.GetLength(0); x++)
        {
            for (int y = 0; y < noiseLayer.GetLength(1); y++)
            {
                noiseLayer[x, y] = noiseLayer[x, y] + secondNoiseLayer[x, y];
            }
        }
        return noiseLayer;
    }
}