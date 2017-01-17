using UnityEngine;

public class Attack : Photon.MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    const float runSpeed = 8.0f;
    const float attackDistance = 2.0f;

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
            anim.SetTrigger("runAttack");
        }


    }

    void HitEvent(int type)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackDistance);
        foreach (var enemyCollider in hitColliders)
        {
            if (enemyCollider.tag == "Enemy")
            {
                float damageValue = 0.0f;
                switch (type)
                {
                    case 0: damageValue = 20.0f; break;
                    case 1: damageValue = 50.0f; break;
                }
                enemyCollider.GetComponent<CharacterHealth>().damage(damageValue);
            }
        }
    }

}
