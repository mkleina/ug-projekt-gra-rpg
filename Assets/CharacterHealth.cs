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
        // Sending object health information from current player
        // This fixes online collisions
        if (this.tag != "Player")
            //this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player);
            this.GetComponent<PhotonView>().RequestOwnership();

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