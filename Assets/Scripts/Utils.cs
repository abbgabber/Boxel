public static class Utils
{
    public static float Remap(float value, float minValue, float maxValue, float newRangeMin, float newRangeMax)
    {
        return (value - minValue) / (maxValue - minValue) * (newRangeMax - newRangeMin) + newRangeMin;
    }
}