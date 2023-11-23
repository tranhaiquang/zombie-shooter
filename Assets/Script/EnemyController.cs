using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private int hp = 5;
    [SerializeField] private GameObject player;
    [SerializeField] private float stoppingDistance = 4f;
    [SerializeField] public GameManager gameManager;
    private Animator enemyAnimator;
    private NavMeshAgent enemyNMA;
    private PlayerHP playerHP;
    private float timeOfLastAttack = 0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyNMA = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        playerHP = player.GetComponent<PlayerHP>();


        FindObjectOfType<AudioManager>().Play("Zombie Groan");


    }


    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        enemyNMA.destination = player.transform.position;

        if (distanceToPlayer <= stoppingDistance) // Attack
        {
            //enemyAnimator.SetBool("Attack", true);
            enemyNMA.isStopped = true;
            enemyAnimator.SetTrigger("Attack");

            if (Time.time >= timeOfLastAttack + 2.3f)
            {
                timeOfLastAttack = Time.time;
                playerHP.takeDamage(1);
            }


        }

        else
        {
            if (IsAnimationDone("Zombie Attack"))
            {
                enemyNMA.isStopped = false;
                enemyAnimator.SetBool("Attack", false);
            }


        }

        if (hp <= 0)
        {
            enemyNMA.isStopped = true;
            enemyAnimator.SetTrigger("Dead");
        }

        if (IsAnimationDone("Zombie Death"))
        {
            gameManager.enemiesAlive--;
            Destroy(gameObject);

        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            enemyNMA.isStopped = true;
            enemyAnimator.SetTrigger("Hit");
            hp--;
            StartCoroutine(ResetHitTrigger(2.1f));
        }

    }

    bool IsAnimationDone(string animName)
    {
        if (enemyAnimator != null)
        {
            AnimatorStateInfo stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);

            return stateInfo.IsName(animName) && stateInfo.normalizedTime >= 1f;
        }

        return false;
    }

    IEnumerator ResetHitTrigger(float time)
    {
        yield return new WaitForSeconds(time); // Adjust the delay as needed

        enemyNMA.isStopped = false;

        enemyAnimator.ResetTrigger("Hit");
    }
}
