using UnityEngine;

public class Attack : Photon.MonoBehaviour
{
    Animator anim;
    const float runSpeed = 8.0f;
    const float attackDistance = 2.0f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //anim.SetTrigger("fastAttack");
            photonView.RPC("triggerAnim", PhotonTargets.All, "fastAttack");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //anim.SetTrigger("runAttack");
            photonView.RPC("triggerAnim", PhotonTargets.All, "runAttack");
        }
    }

    [PunRPC]
    private void triggerAnim(string name)
    {
        anim.SetTrigger(name);
    }

    // HitEvent is animation triggered method
    void HitEvent(int type)
    {
        if (!photonView.isMine) return;

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
                enemyCollider.gameObject.GetPhotonView().RPC("damage", PhotonTargets.All, damageValue);
            }
        }
    }

}
