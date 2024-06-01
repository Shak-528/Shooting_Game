using UnityEngine;

public class GunFire : MonoBehaviour
{
    public static GunFire Instance { get; private set; }

    public GameObject dotPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of GunFire!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Stop checking for mouse input if the player is dead
        if (Player.Instance.currentHealth <= 0)
        {
            return;
        }

        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Draw a dot at the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0, because we're in 2D
            Instantiate(dotPrefab, mousePosition, Quaternion.identity);
        }
    }
}
