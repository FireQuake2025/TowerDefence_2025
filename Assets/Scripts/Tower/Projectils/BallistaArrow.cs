using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BallistaArrow : Projectile
{
    public int damage;
    public float speed = 10;

    private GoldManagment goldManagment;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldManagment = GameObject.FindAnyObjectByType<GoldManagment>();
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
            Health health = other.GetComponent<Health>();
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                health.TakeDamage(damage);
                if(health.currentHealth <= 0)
                {
                    goldManagment.GetComponent<GoldManagment>().DropGold();
                    Destroy(enemy.gameObject);

                }
            }
            Destroy(gameObject);
        }
    }
}
