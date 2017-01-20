using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private const float attackDistance = 2.0f;
    private const float followDistance = 12.0f;

    private string state = "patrol";
    private Animator anim;

    //public GameObject[] waypoints;
    //private float accuracyWP = 5.0f;
    //private int currentWP = 0;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void rotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    GameObject findNearestPlayer(float maxDistance)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestPlayer = null;
        float distance = maxDistance;
        foreach (GameObject player in players)
        {
            float distanceFromEnemy = Vector3.Distance(player.transform.position, transform.position);
            if (distanceFromEnemy < distance)
            {
                distance = distanceFromEnemy;
                nearestPlayer = player;
            }
        }
        return nearestPlayer;
    }

    private bool canSeePlayer(GameObject player, float fieldOfViewDegrees)
    {
        Vector3 rayDirection = player.transform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CharacterHealth>().get() == 0)
        {
            return;
        }

        GameObject nearestPlayer = findNearestPlayer(followDistance);

        //if (state == "patrol" && waypoints.Length > 0)
        //{
        //    anim.SetBool("isIdle", false);
        //    anim.SetBool("isWalking", true);
        //    if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
        //    {
        //        //currentWP = Random.Range(0, waypoints.Length);
        //        currentWP++;
        //        if (currentWP >= waypoints.Length)
        //        {
        //            currentWP = 0;
        //        }
        //    }
        //    GetComponent<NavMeshAgent>().destination = waypoints[currentWP].transform.position;
        //}

        if (nearestPlayer != null)
        {
            if (canSeePlayer(nearestPlayer, 120) || state == "pursuing")
            {
                state = "pursuing";
                GetComponent<NavMeshAgent>().destination = nearestPlayer.transform.position;

                if (Vector3.Distance(nearestPlayer.transform.position, transform.position) > attackDistance)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                }
                else
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isWalking", false);
                    rotateTowards(nearestPlayer.transform);
                }
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().ResetPath();
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            state = "patrol";
        }
    }

    void HitEvent()
    {
        GameObject victim = findNearestPlayer(attackDistance);
        if (victim != null)
        {
            victim.GetComponent<CharacterHealth>().damage(20);
        }
    }
}
