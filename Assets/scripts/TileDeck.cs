using System.Collections.Generic;
using UnityEngine;

public class TileDeck : MonoBehaviour
{
    public GameObject[] tilePrefabs; // Array of tile prefabs to deal

    // Function to deal a hand of tiles (for both player and AI)
    public List<GameObject> DealHand()
    {
        List<GameObject> hand = new List<GameObject>();

        for (int i = 0; i < 13; i++) // A Mahjong hand typically has 13 tiles
        {
            int randomIndex = Random.Range(0, tilePrefabs.Length);
            hand.Add(tilePrefabs[randomIndex]);
        }

        return hand;
    }
}
