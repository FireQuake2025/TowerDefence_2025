using UnityEngine;

public class CannonBall : Projectile
{
    public float explosionRadius = 5;
    public int damage = 10;

    [SerializeField] private float speed = 10;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] private float boulderForce;


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

    // fires at target that has entered the rand of tower 
    protected override void ProjectilePath()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    //Destroys anything in a radius around boulder prefab apon inpact with enemy object

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Health health = other.GetComponent<Health>();
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Enemy enemy = other.GetComponent<Enemy>();
            Collider collider = gameObject.GetComponent<Collider>();
            AudioSource explosionSound = gameObject.GetComponent<AudioSource>();
            if (enemy != null)
            {
                explosionSound.Play();
                health.TakeDamage(damage);
                Collider[] surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (Collider enemyObj in surroundingObjects)
                {
                    Rigidbody enemyRigedbody = enemyObj.GetComponent<Rigidbody>();
                    if (enemyRigedbody == null)
                    {
                        continue;
                    }
                    if (health.currentHealth <= 0)
                    {
                        enemyRigedbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                        Destroy(enemyObj.gameObject);
                    }


                }

            }
            Destroy(gameObject);

        }
    }


}
