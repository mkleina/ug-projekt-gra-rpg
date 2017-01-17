using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : Photon.MonoBehaviour
{
    public CharacterHealth health;
    public float healthBarWidthMax;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            RectTransform healthBarTransform = GameObject.Find("PlayerHealthBar").GetComponent<Image>().rectTransform;
            healthBarTransform.sizeDelta = new Vector2(health.get() / 100 * healthBarWidthMax, healthBarTransform.sizeDelta.y);
        }
    }
}