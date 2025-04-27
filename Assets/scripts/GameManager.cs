using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool isPlayerTurn = true;

    public PlayerScript playerScript;
    public AIScript aiScript;
    public TileDeck tileDeck;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        aiScript = FindObjectOfType<AIScript>();
        tileDeck = FindObjectOfType<TileDeck>();

        DealTiles();
    }

    void DealTiles()
    {
        List<GameObject> playerHand = tileDeck.DealHand();
        List<GameObject> aiHand = tileDeck.DealHand();

        PositionTiles(playerHand, playerScript.transform.position, GameObject.Find("Player Hand").transform);
        PositionTiles(aiHand, aiScript.transform.position, GameObject.Find("AI hand").transform);
    }

    void PositionTiles(List<GameObject> handPrefabs, Vector3 startPosition, Transform handContainer)
    {
        float offset = 1.5f;
        Vector3 currentPosition = startPosition;

        List<GameObject> instantiatedTiles = new List<GameObject>();

        foreach (GameObject prefab in handPrefabs)
        {
            GameObject tileInstance = Instantiate(prefab, currentPosition, Quaternion.identity);
            tileInstance.transform.SetParent(handContainer);
            instantiatedTiles.Add(tileInstance);
            currentPosition += new Vector3(offset, 0, 0);
        }

        if (handContainer.name == "Player Hand")
        {
            playerScript.handTiles = instantiatedTiles.ToArray();
        }
        else if (handContainer.name == "AI hand")
        {
            aiScript.handTiles = instantiatedTiles.ToArray();
        }
    }

    public void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (isPlayerTurn)
        {
            playerScript.DrawTile();
        }
        else
        {
            aiScript.TakeTurn();
        }
    }
}
