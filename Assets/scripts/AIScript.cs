using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIScript : MonoBehaviour
{
    public GameObject[] handTiles;
    public Transform aiHandContainer;
    private TileDeck tileDeck;

    void Start()
    {
        tileDeck = FindObjectOfType<TileDeck>();
    }

    public void TakeTurn()
    {
        StartCoroutine(DelayedAIMove());
    }

    private IEnumerator DelayedAIMove()
    {
        yield return new WaitForSeconds(1f);

        GameObject drawn = tileDeck.DrawTile();
        if (drawn != null)
        {
            GameObject newTile = Instantiate(drawn, aiHandContainer.position + Vector3.right * handTiles.Length * 1.5f, Quaternion.identity);
            newTile.transform.SetParent(aiHandContainer);

            List<GameObject> newHand = new List<GameObject>(handTiles);
            newHand.Add(newTile);
            handTiles = newHand.ToArray();

            Debug.Log("AI drew: " + newTile.name);
        }

        if (handTiles.Length > 0)
        {
            int randomIndex = Random.Range(0, handTiles.Length);
            GameObject tileToDiscard = handTiles[randomIndex];

            Debug.Log("AI discards: " + tileToDiscard.name);

            List<GameObject> handList = new List<GameObject>(handTiles);
            handList.RemoveAt(randomIndex);
            handTiles = handList.ToArray();

            Destroy(tileToDiscard);
        }

        FindObjectOfType<GameManager>().SwitchTurn();
    }
}
