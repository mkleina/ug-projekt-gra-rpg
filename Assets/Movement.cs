using UnityEngine;

public class Movement : MonoBehaviour {

	public Animator anim;
    Vector3 direction;
    GameObject healthBar;
    public float rotSpeed =0.5f;
    private float attackDistance = 2f;
    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        //if (healthBar.transform.localScale.x == 0)
        //{
        //    return;
        //}


        GameObject player = findNearestPlayer(20);
        
        if (player)
        {
            direction = player.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (Vector3.Distance(player.transform.position, transform.position) < 20 && angle < 90)
            {
                direction.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                anim.SetBool("isIdle", false);
                if (direction.magnitude>attackDistance)
                {
                    transform.Translate(0, 0, 0.05f);
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isAttacking", false);
                }
                else
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isWalking", false);
                    rotateTowards(player.transform);
                }

            }

        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
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
}
