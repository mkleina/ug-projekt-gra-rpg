using UnityEngine;

public class Attack : Photon.MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    const float runSpeed = 8.0f;
    bool animationFinish = false;
    bool animationStart = true;
    public GameObject getPosition;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.isMine)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("fastAttack");

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animationStart = true;
            animationFinish = false;
            anim.SetTrigger("runAttack");
        }


    }

    //public void animationFinished()
    //{
    //    animationFinish = true;
    //}

    //private void LateUpdate()
    //{
    //    if (animationFinish && animationStart)
    //    {
    //        animationStart = false;

    //       //rb.AddRelativeForce(Vector3.forward * 45, ForceMode.VelocityChange);

    //    }
    //}

}
