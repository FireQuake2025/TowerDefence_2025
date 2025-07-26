using UnityEngine;

public class CannonBall : Projectile
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] private float explosionRadius = 5;


    public ParticleSystem explosion;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void ProjectilePath()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Instantiate(explosion,transform.position, explosion.transform.rotation);
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach(var enemyObj in surroundingObjects)
                {
                    var enemyRigedbody = enemyObj.GetComponent<Rigidbody>();
                    if (enemyRigedbody == null)
                        { continue; }
                    enemyRigedbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    Destroy(enemyObj.gameObject);
                }
                
            }
            Destroy(gameObject);
        }
    }
}
