using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The GameObject to spawn
    public GameObject objToSpawn;

    // Reference to an existing instance to the GameObject
    private GameObject existingInstance;

    public void SpawnObject()
    {
        if (objToSpawn == null)
        {
            Debug.LogWarning("No GameObject to spawn has been selected.");
            return;
        }

        if (existingInstance != null)
        {
            Destroy(existingInstance); // If there is already an instance of the GameObject on a scene, destroy it.
        }

        existingInstance = Instantiate(objToSpawn, transform.position, transform.rotation);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }
}
