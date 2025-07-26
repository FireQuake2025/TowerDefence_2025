
using UnityEngine;

public class Explode : MonoBehaviour
{
    private Camera cam;
    private Collider[] Hits;
    [SerializeField] private ParticleSystem ParticleSystemPrefab;

    public int MaxHits = 25;
    public float Radius = 10;
    public LayerMask HitLayer;
    public LayerMask BlockExplosion;
    public int MaxDamage;
    public int MinDamage;
    public float ExplosiveForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cam = GetComponent<Camera>();
        Hits = new Collider[MaxHits];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(ParticleSystemPrefab, hit.point, Quaternion.identity);
                int hits = Physics.OverlapSphereNonAlloc(hit.point, Radius, Hits, HitLayer);
                for (int i = 0; i < hits; i++)
                {
                    if (Hits[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
                    {
                        float distance = Vector3.Distance(hit.point, Hits[i].transform.position);
                        if (!Physics.Raycast(hit.point, (Hits[i].transform.position - hit.point).normalized, distance, BlockExplosion.value))
                        {
                            rb.AddExplosionForce(ExplosiveForce, hit.point, Radius);
                        }
                    }
                }
            }
        }
    }
}
