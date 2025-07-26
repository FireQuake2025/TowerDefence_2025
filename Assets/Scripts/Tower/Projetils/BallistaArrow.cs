using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BallistaArrow : Projectile
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 10;

    
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
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
