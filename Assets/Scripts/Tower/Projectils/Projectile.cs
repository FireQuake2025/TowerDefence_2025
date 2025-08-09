using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3;

    public Transform target;
    public Health health;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target != null)
        {
            ProjectilePath();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void ProjectilePath();

    public void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }

    protected abstract void OnTriggerEnter(Collider other);



}
