using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navAgent;
    [SerializeField] private Transform endPoint;
    private Animator animator;
    [SerializeField] private string anamationPram_IsWalking;
    [SerializeField] private int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        navAgent.SetDestination(endPoint.position);
        animator.SetBool(anamationPram_IsWalking, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!navAgent.hasPath || navAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachEnd();
            }
        }

    }

    private void ReachEnd()
    {
        animator.SetBool(anamationPram_IsWalking, false);
        GameManager.Instance.playerHeath.TakeDamage(damage);
        Destroy(gameObject);
    }
}
