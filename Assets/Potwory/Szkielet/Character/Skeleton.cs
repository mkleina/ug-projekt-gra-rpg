using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    private const float attackDistance = 2.0f;
    private const float followDistance = 20.0f;

    public Transform head;
    public float rotSpeed;
    public float speed;
    public float pursueSpeed;
    public GameObject[] waypoints;
    public GameObject healthBar;

    private string state = "patrol";
    private Animator anim;
    private float accuracyWP = 5.0f;
    private int currentWP = 0;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void rotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
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

    // Update is called once per frame
    void Update()
    {
        if (healthBar.transform.localScale.x == 0)
        {
            return;
        }

        GameObject nearestPlayer = findNearestPlayer(followDistance);
        Vector3 direction;

        if (state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                //currentWP = Random.Range(0, waypoints.Length);
                currentWP++;
                if(currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }
            }
            GetComponent<NavMeshAgent>().destination = waypoints[currentWP].transform.position;
        }

        if (nearestPlayer != null) {
            direction = nearestPlayer.transform.position - transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(direction, head.up);

            if (angle < 100 || state == "pursuing")
            {
                state = "pursuing";
                GetComponent<NavMeshAgent>().destination = nearestPlayer.transform.position;

                if (direction.magnitude > attackDistance)
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
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
            state = "patrol";
        }
    }

    void HitEvent()
    {
        GameObject victim = findNearestPlayer(attackDistance);
        if (victim != null) {
            victim.GetComponent<CharacterHealth>().damage(20);
        }
    }
}
