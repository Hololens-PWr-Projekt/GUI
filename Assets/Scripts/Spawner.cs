using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // The GameObject to spawn
    [Tooltip("Obiekt, którego instancjê chcemy utworzyæ na scenie.")]
    public GameObject objToSpawn;

    // Distance in front of Player where the GameObject will be spawned
    [Tooltip("Odleg³oœæ w jakiej obiekt zostanie utworzony od gracza (przed graczem) [metry].")]
    public float spawnDistance;

    // UI button to call SpawnObject()
    [Tooltip("Przycisk UI wywo³uj¹cy funkcjê SpawnObject")]
    public Button spawnButton;

    // Reference to an existing instance of the GameObject
    private GameObject existingInstance;

    void Start()
    {
        if (spawnButton == null)
        {
            Debug.LogWarning("Spawn button is not assigned.");
        }

        spawnButton.onClick.AddListener(SpawnObject);
    }

    void OnDestroy()
    {
        if (spawnButton != null)
        {
            spawnButton.onClick.RemoveListener(SpawnObject);
        }
    }

    public void SpawnObject()
    {
        if (objToSpawn == null)
        {
            Debug.LogWarning("No GameObject to spawn has been selected.");
            return;
        }

        // Main Camera on a scene, represents Player
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("No main camera on the scene has been found. Make sure there is a camera on the scene tagged as 'MainCamera'.");
            return;
        }

        Vector3 cameraForwardXZ = mainCamera.transform.forward;
        cameraForwardXZ.y = 0; // The GameObject ought to be spawned only on Player's XZ plane (Y-axis independence)
        cameraForwardXZ.Normalize();

        Vector3 spawnPosition = mainCamera.transform.position + cameraForwardXZ * spawnDistance;
        Quaternion spawnRotation = Quaternion.LookRotation(cameraForwardXZ); // The spawned GameObject will point upwards by default

        if (existingInstance != null)
        {
            Destroy(existingInstance); // If there is already an instance of the GameObject on a scene, destroy it
        }

        existingInstance = Instantiate(objToSpawn, spawnPosition, spawnRotation);

    }
}
