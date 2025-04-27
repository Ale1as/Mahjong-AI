using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    public GameObject[] handTiles;
    public Transform playerHandContainer;
    private GameManager gameManager;
    private TileDeck tileDeck;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tileDeck = FindObjectOfType<TileDeck>();
    }

    void Update()
    {
        if (!gameManager.isPlayerTurn) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.IsChildOf(playerHandContainer))
                {
                    GameObject selected = hit.transform.gameObject;
                    Debug.Log("Player discards: " + selected.name);

                    List<GameObject> handList = new List<GameObject>(handTiles);
                    handList.Remove(selected);
                    handTiles = handList.ToArray();

                    Destroy(selected);

                    gameManager.SwitchTurn();
                }
            }
        }
    }

    public void DrawTile()
    {
        GameObject drawn = tileDeck.DrawTile();
        if (drawn == null) return;

        GameObject newTile = Instantiate(drawn, playerHandContainer.position + Vector3.right * handTiles.Length * 1.5f, Quaternion.identity);
        newTile.transform.SetParent(playerHandContainer);

        List<GameObject> newHand = new List<GameObject>(handTiles);
        newHand.Add(newTile);
        handTiles = newHand.ToArray();

        Debug.Log("Player drew: " + newTile.name);
    }
}
