using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    public GameObject[] handTiles; // Array to hold the player's hand tiles
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Get the GameManager reference
    }

    // Function to handle player tile click and discard
    void OnMouseDown()
    {
        if (gameManager.isPlayerTurn && !transform.IsChildOf(GameObject.Find("AIHand").transform))
        {
            // Handle tile discard logic
            DiscardTile(gameObject);
            gameManager.SwitchTurn(); // Switch turn after discarding
        }
    }

    void DiscardTile(GameObject tile)
    {
        // Remove the tile from the player's hand
        List<GameObject> handList = new List<GameObject>(handTiles);
        handList.Remove(tile);
        handTiles = handList.ToArray();

        // Destroy the tile from the scene
        Destroy(tile);

        // Log the discard for debugging
        Debug.Log("Player discarded: " + tile.name);
    }
}
