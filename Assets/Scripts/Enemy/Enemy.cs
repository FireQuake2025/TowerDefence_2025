using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private string anamationPram_IsWalking;
    [SerializeField] private int damage;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] private float explosionRadius = 5;

    public ParticleSystem explosion;
    public int speed = 3;
    public int addGold;

    private Animator animator;
    private NavMeshAgent navAgent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetBool(anamationPram_IsWalking, true);
    }
    public void Initialized(Transform inputEndpoint)
    {
        endPoint = inputEndpoint;
        navAgent.speed = speed;
        navAgent.SetDestination(endPoint.position);

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

    //Destroy enemy that reaches the end of its path
    private void ReachEnd()
    {
        animator.SetBool(anamationPram_IsWalking, false);
        GameManager.Instance.playerHeath.TakeDamage(damage);
        Destroy(gameObject);
    }

    //UFO destroys everything in its radius apon death
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "UFO")
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Collider[] surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider Tower in surroundingObjects)
            {
                Rigidbody towerRigedbody = Tower.GetComponent<Rigidbody>();
                if (towerRigedbody == null)
                {
                    continue;
                }
                towerRigedbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
