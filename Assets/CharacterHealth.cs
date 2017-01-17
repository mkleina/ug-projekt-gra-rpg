using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : Photon.MonoBehaviour
{
    public float healthValue;
    public float healthValueMax;

    // Use this for initialization
    void Start()
    {
    }

    public void damage(float value)
    {
        healthValue = Mathf.Clamp(healthValue - value, 0, healthValueMax);
    }

    public float get()
    {
        return healthValue;
    }

    public float getPercent()
    {
        return healthValue / healthValueMax;
    }

    public float getMax()
    {
        return healthValueMax;
    }
}