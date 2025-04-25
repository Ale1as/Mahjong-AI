using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public GameObject[] handTiles; // Array to hold the AI's hand tiles
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Get the GameManager reference
    }

    // Update function to simulate AI's turn
    void Update()
    {
        if (!gameManager.isPlayerTurn) // If it's the AI's turn
        {
            // AI randomly discards a tile after a short delay to simulate thinking
            StartCoroutine(AIDiscardTile());
        }
    }

    // Coroutine to simulate the AI discarding a tile after a short delay
    IEnumerator AIDiscardTile()
    {
        // Wait for a short time (simulating AI thinking)
        yield return new WaitForSeconds(1f);

        if (handTiles.Length > 0)
        {
            // Pick a random tile to discard
            int randomIndex = Random.Range(0, handTiles.Length);
            GameObject tileToDiscard = handTiles[randomIndex];

            // Remove the tile from the AI's hand array
            List<GameObject> handList = new List<GameObject>(handTiles);
            handList.RemoveAt(randomIndex);
            handTiles = handList.ToArray();

            // Destroy the tile from the scene
            Destroy(tileToDiscard);

            // Log the AI's action for debugging
            Debug.Log("AI discarded: " + tileToDiscard.name);

            // Switch turn after discarding
            gameManager.SwitchTurn();
        }
    }
}
