using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    private NavMeshAgent navAgent;
    [SerializeField] private Transform endPoint;
    private Animator animation;
    [SerializeField] private string anamationPram_IsWalking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animator>();
    }

    private void Start()
    {
        navAgent.SetDestination(endPoint.position);
        animation.SetBool(anamationPram_IsWalking, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!navAgent.hasPath || navAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                animation.SetBool(anamationPram_IsWalking, false);
            }
        }
        
    }
}
