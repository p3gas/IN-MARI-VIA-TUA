using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Color32 clearWaterColor = new Color32(120, 128, 168, 255);
    public Color32 normalWaterColor = new Color32(99, 134, 147, 255);
    public Color32 dirtyWaterColor = new Color32(97, 130, 114, 255);
    
    public float dirtiness = 5;
    private float oldDirtness = 5;

    private float depth = 0;

    void Start()
    {
        UpdateFog();
    }

    void Update()
    {
        if (oldDirtness != dirtiness)
        {
            oldDirtness = dirtiness;
            UpdateFog();
        }
    }

    void UpdateFog()
    {
        if (dirtiness < 2)
        {
            RenderSettings.fogColor = clearWaterColor;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = dirtiness / 20.0f;
            RenderSettings.fog = true;
        }
        else if (dirtiness < 5)
        {
            RenderSettings.fogColor = normalWaterColor;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = dirtiness / 18.0f;
            RenderSettings.fog = true;
        }
        else if (dirtiness < 7)
        {
            RenderSettings.fogColor = dirtyWaterColor;
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = dirtiness / 15.0f;
            RenderSettings.fog = true;
        }
        else
        {
            RenderSettings.fogColor = dirtyWaterColor;
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = dirtiness / 10.0f;
            RenderSettings.fog = true;
        }
    }
}
