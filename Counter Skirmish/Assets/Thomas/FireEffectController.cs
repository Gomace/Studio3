using UnityEngine;

public class FireEffectController : MonoBehaviour
{
    [SerializeField]
    private float activationDelay = 1.0f;
    [SerializeField]
    private float fireDuration = 5.0f;

    [SerializeField]
    private GameObject fireVFX;
    
    [SerializeField]
    private GameObject intactRopeModel; // Assign the intact rope model in the inspector
    [SerializeField]
    public GameObject burnedDecal;
    [SerializeField]
    private GameObject burnedRopeModel;
    [SerializeField]
    private GameObject prefabToSpawn; // Prefab to spawn
    [SerializeField]
    private Vector3 initialPosition;
    [SerializeField]
    private bool isPrefabSpawned = false;
    [SerializeField]
    private float lerpAmount = 0.1f; // Small amount to lerp on the Y axis
    [SerializeField]
    private float spawnThreshold = 1.0f;
    void Start()
    {
        initialPosition = transform.position;
        burnedDecal.SetActive(true);
        fireVFX.SetActive(true);
        Invoke(nameof(DeactivateFire), fireDuration);
    }

    void Update()
    {
        // Lerp the transform on the Y axis by a small amount
        float newY = Mathf.Lerp(transform.position.y, transform.position.y + lerpAmount, Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Check the distance from the original position
        if (!isPrefabSpawned && Vector3.Distance(initialPosition, transform.position) >= spawnThreshold)
        {
            Instantiate(prefabToSpawn, initialPosition, Quaternion.identity); // Spawn the prefab at the initial position
            isPrefabSpawned = true; // Ensure that the prefab is only spawned once
        }
    }
    private void ActivateChild()
    {
        // Check if the first child is not null and activate it
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void DeactivateFire()
    {
        // Deactivate the fireVFX if it's not null
        if (fireVFX != null)
        {
            fireVFX.SetActive(false);
            Destroy(this, 1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the rope
        if (collision.gameObject.CompareTag("Rope")) // Make sure your rope GameObject has the "Rope" tag
        {
            if (intactRopeModel != null)
                intactRopeModel.SetActive(false);

            if (burnedRopeModel != null)
                burnedRopeModel.SetActive(true);
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            Invoke(nameof(ActivateChild), activationDelay);
        }
    }
}