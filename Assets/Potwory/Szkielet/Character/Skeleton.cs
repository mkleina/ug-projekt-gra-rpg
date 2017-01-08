using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{

    public Transform player;
    static Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var pozycja = GameObject.FindGameObjectWithTag("Wizzard").transform.position;


        Vector3 direction = pozycja - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        // Dodanie ze potwor widzi nas jak jestesmy z jego przodu.
        //if (Vector3.Distance(pozycja, this.transform.position) < 10 && angle < 30)
        //float dist = Vector3.Distance(pozycja, this.transform.position);
   
        if (Vector3.Distance(pozycja, this.transform.position) < 10)
        {
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            anim.SetBool("isIdle", false);
            if (direction.magnitude > 2)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
}
