using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePool : MonoBehaviour
{
    public static PlanePool Instance { get; private set; }

    public GameObject planePrefab;
    public int poolSize = 5;

    private List<GameObject> planePool;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of PlanePool!");
        }

        planePool = new List<GameObject>();

        // Populate the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject plane = Instantiate(planePrefab);
            plane.SetActive(false);
            planePool.Add(plane);
        }
    }

    public GameObject GetPlane()
    {
        foreach (GameObject plane in planePool)
        {
            if (!plane.activeInHierarchy)
            {
                // Get the SpriteRenderer components
                SpriteRenderer[] sprites = plane.GetComponentsInChildren<SpriteRenderer>();

                // Disable all sprites
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.enabled = false;
                }

                // Enable a random sprite
                int randomIndex = Random.Range(0, sprites.Length);
                sprites[randomIndex].enabled = true;

                plane.SetActive(true);
                return plane;
            }
        }

        // If no inactive planes are available, create a new one
        GameObject newPlane = Instantiate(planePrefab);

        // Get the SpriteRenderer components
        SpriteRenderer[] newPlaneSprites = newPlane.GetComponentsInChildren<SpriteRenderer>();

        // Enable a random sprite
        int newPlaneRandomIndex = Random.Range(0, newPlaneSprites.Length);
        newPlaneSprites[newPlaneRandomIndex].enabled = true;

        planePool.Add(newPlane);
        newPlane.SetActive(true);
        return newPlane;
    }

    public void ReturnPlane(GameObject plane)
    {
        plane.SetActive(false);
    }
}
