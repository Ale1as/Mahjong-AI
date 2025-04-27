using System.Collections.Generic;
using UnityEngine;

public class TileDeck : MonoBehaviour
{
    public List<GameObject> tilePrefabs; // All possible tile prefabs
    private List<GameObject> deck = new List<GameObject>();

    void Start()
    {
        // Create deck at start
        CreateDeck();
    }

    void CreateDeck()
    {
        deck.Clear();

        // Add one copy of each tilePrefab (or more if needed)
        foreach (GameObject tile in tilePrefabs)
        {
            deck.Add(tile);
        }

        Shuffle(deck);
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public GameObject DrawTile()
    {
        if (deck.Count == 0) return null;

        GameObject drawnTile = deck[0];
        deck.RemoveAt(0);
        return drawnTile;
    }

    public List<GameObject> DealHand(int count = 5)
    {
        List<GameObject> hand = new List<GameObject>();

        for (int i = 0; i < count && deck.Count > 0; i++)
        {
            hand.Add(DrawTile());
        }

        return hand;
    }
}
