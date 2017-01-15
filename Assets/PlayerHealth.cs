using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Photon.MonoBehaviour
{
    public float health;
    public float healthBarWidthMax;

    // Use this for initialization
    void Start()
    {
    }

    public void damage(float value)
    {
        health = Mathf.Clamp(health - value, 0, 100);
    }

    public float get()
    {
        return health;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            RectTransform healthBarTransform = GameObject.Find("PlayerHealthBar").GetComponent<Image>().rectTransform;
            healthBarTransform.sizeDelta = new Vector2(get() / 100 * healthBarWidthMax, healthBarTransform.sizeDelta.y);
        }
    }
}