using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Transform headposition;
    public float dirtiness = 5.0f;
    public float depth = 0.0f;
    public float maxDepth = 45.0f;

    public Material skyboxUnderWater;
    public Material skyboxOverWater;

    public Color32 clearWaterColor = new Color32(120, 128, 168, 255);
    public Color32 normalWaterColor = new Color32(99, 134, 147, 255);
    public Color32 dirtyWaterColor = new Color32(97, 130, 114, 255);

    public float cleanWaterThreshold = 2.0f;
    public float normalWaterThreshold = 5.0f;
    public float dirtyWaterThreshold = 8.0f;

    public float cleanWaterDensity = 0.05f;
    public float normalWaterDensity = 0.1f;
    public float dirtyWaterDensity = 0.2f;
    public float maxWaterDensity = 0.3f;

    private Color32 waterColor;
    private float oldDirtness = 5;
    private float oldDepth = 0;

    void Start()
    {
        UpdateFog();
    }

    void Update()
    {
        depth = -headposition.position.y;
        if (oldDirtness != dirtiness || oldDepth != depth)
        {
            UpdateColor();
            if (depth > 0)
            {
                UpdateFog();
                skyboxUnderWater.SetColor("_Tint", waterColor);
                RenderSettings.skybox = skyboxUnderWater; 
                RenderSettings.fog = true;
            }
            else
            {
                skyboxOverWater.SetColor("_GroundColor", waterColor);
                RenderSettings.skybox = skyboxOverWater;
                RenderSettings.fog = false;
            }
            
            oldDirtness = dirtiness;
            oldDepth = depth;
        }
    }
    void UpdateColor()
    {
        if (dirtiness < 5)
            waterColor = Color32.Lerp(clearWaterColor, normalWaterColor, Mathf.Clamp01(dirtiness / 5f));
        else
            waterColor = Color32.Lerp(normalWaterColor, dirtyWaterColor, Mathf.Clamp01((dirtiness - 5f) / 5f));
    }


    void UpdateFog()
    {
        //Water - Fog density
        float fogDensity;
        if (dirtiness < cleanWaterThreshold)
            fogDensity = cleanWaterDensity + (dirtiness / (cleanWaterThreshold / (normalWaterDensity - cleanWaterDensity)));
        else if (dirtiness < normalWaterThreshold)
            fogDensity = normalWaterDensity + ((dirtiness - cleanWaterThreshold) / ((normalWaterThreshold - cleanWaterThreshold) / (dirtyWaterDensity - normalWaterDensity)));
        else if (dirtiness < dirtyWaterThreshold)
            fogDensity = dirtyWaterDensity + ((dirtiness - normalWaterThreshold) / ((dirtyWaterThreshold - normalWaterThreshold) / (maxWaterDensity - dirtyWaterDensity)));
        else
            fogDensity = maxWaterDensity;

        fogDensity = cleanWaterDensity + fogDensity * (depth / maxDepth);

        //Fog mode
        if (dirtiness < normalWaterThreshold)
            RenderSettings.fogMode = FogMode.Exponential;
        else
            RenderSettings.fogMode = FogMode.ExponentialSquared;

        //Assigment
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = fogDensity;
    }
}
