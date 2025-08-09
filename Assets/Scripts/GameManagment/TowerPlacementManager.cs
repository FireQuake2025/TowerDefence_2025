using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacementManager : MonoBehaviour
{
    public Camera MainCam;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;
    public bool isPlacingTower = false;

    [SerializeField] private float offset = .2f;
    [SerializeField] private bool isTileSelected;

    private GameObject currentTowerToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacmentPos;
    private GoldManagment goldManagment;
    private Tower tower;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldManagment = GetComponent<GoldManagment>();
        tower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacingTower)
        {
            Ray ray = MainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, TileLayer))
            {
                towerPlacmentPos = hitInfo.transform.position + Vector3.up * offset;
                towerPreview.transform.position = towerPlacmentPos;

                towerPreview.SetActive(true);
                isTileSelected = true;
            }
            else
            {
                towerPreview.SetActive(false);
                isTileSelected = false;
            }
        }
    }

    private void OnEnable()
    {
        PlaceTowerAction.Enable();
        PlaceTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        PlaceTowerAction.performed -= OnPlaceTower;
        PlaceTowerAction.Disable();
    }

    //Called on click of button
    public void StartPlacingTower(GameObject towerPrefab)
    {
        
        if (currentTowerToSpawn != towerPrefab)
        {
            isPlacingTower = true;
            currentTowerToSpawn = towerPrefab;
            if (towerPreview != null)
            {
                Destroy(towerPreview);
            }
            towerPreview = Instantiate(currentTowerToSpawn);
        }
    }

    //Creats prefab and sets it to tile apon left clicking
    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if (isPlacingTower && isTileSelected)
        {
            goldManagment = FindAnyObjectByType<GoldManagment>();
            tower = FindAnyObjectByType<Tower>();
            if (goldManagment.currentGold >= tower.TowerCost)
            {
                Instantiate(currentTowerToSpawn, towerPlacmentPos, Quaternion.identity);
                Destroy(towerPreview);
                currentTowerToSpawn = null;
                isPlacingTower = false;
                goldManagment.currentGold -= tower.TowerCost;
            }
            
        }
        

    }
}
