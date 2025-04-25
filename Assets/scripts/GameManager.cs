using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool isPlayerTurn = true; // Flag to check if it's the player's turn

    public PlayerScript playerScript; // Reference to the player script
    public AIScript aiScript; // Reference to the AI script
    public TileDeck tileDeck; // Reference to the TileDeck script

    void Start()
    {
        // Initialize game
        playerScript = FindObjectOfType<PlayerScript>();
        aiScript = FindObjectOfType<AIScript>();
        tileDeck = FindObjectOfType<TileDeck>(); // Get the TileDeck

        // Deal tiles to both the player and the AI
        DealTiles();
    }

    // Deal tiles to the player and the AI
    void DealTiles()
    {
        List<GameObject> playerHand = tileDeck.DealHand(); // Deal tiles to the player
        List<GameObject> aiHand = tileDeck.DealHand(); // Deal tiles to the AI

        // Add the player's hand tiles to the PlayerScript's hand
        playerScript.handTiles = playerHand.ToArray();

        // Add the AI's hand tiles to the AIScript's hand
        aiScript.handTiles = aiHand.ToArray();

        // Optionally, instantiate the tiles in the scene
        PositionTiles(playerHand, playerScript.transform.position, null); // Player's hand has no parent
        PositionTiles(aiHand, aiScript.transform.position, GameObject.Find("AIHand").transform); // AI's hand is a child of AIHand
    }

    // Function to position tiles in the scene
    void PositionTiles(List<GameObject> hand, Vector3 startPosition, Transform handContainer)
    {
        float offset = 1.5f; // Space between tiles
        Vector3 currentPosition = startPosition;

        foreach (GameObject tile in hand)
        {
            // Instantiate the tile in the scene
            GameObject tileInstance = Instantiate(tile, currentPosition, Quaternion.identity);

            if (handContainer != null)
            {
                // Set the parent to the handContainer (e.g., AIHand for AI tiles)
                tileInstance.transform.SetParent(handContainer);
            }

            currentPosition += new Vector3(offset, 0, 0); // Move the tile to the next position
        }
    }

    // Function to switch turns
    public void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn; // Toggle turn
    }
}
