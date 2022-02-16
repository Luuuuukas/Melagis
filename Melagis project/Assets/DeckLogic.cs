using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLogic : MonoBehaviour
{
    [SerializeField] List<CardDataTemplate> unshuffeledDeck;
    private List<CardDataTemplate> usedDeck;
    float z = 0.0f;
    float zIncrement = 0.001f;
    private void Start()
    {

        usedDeck = new List<CardDataTemplate>();
        CopyTo(unshuffeledDeck, usedDeck);
    }
    public List<CardDataTemplate> ShuffleDeck(List<CardDataTemplate> deckToShuffle)
    {
        List<CardDataTemplate> shuffledDeck = new List<CardDataTemplate>();
        while (deckToShuffle.Count > 0)
        {
            int randomIndex = Random.Range(0, deckToShuffle.Count);
            shuffledDeck.Add(deckToShuffle[randomIndex]);
            deckToShuffle.RemoveAt(randomIndex);
        }
        return shuffledDeck;
    }
    public void SpawnDeck()
    {
        z = 0.0f;
        foreach(CardDataTemplate card in usedDeck)
        {
            GameObject thing = Instantiate(card.GetCardModel(), new Vector3(transform.position.x, transform.position.y, transform.position.z + zIncrement), Quaternion.identity);
            thing.transform.parent = gameObject.transform;
            thing.transform.localScale *= 20f;
            thing.transform.Rotate(Vector3.right, 180f);
            thing.AddComponent<BoxCollider>();
            thing.tag = "Card";
            z -= zIncrement;
        }
    }
    public void PressButton()
    {
        usedDeck = ShuffleDeck(usedDeck);
        
        foreach(CardDataTemplate card in usedDeck)
        {
            Debug.Log(card.GetCardValue() + " " + card.GetCardSuit());
        }
    }
    private void CopyTo(List<CardDataTemplate> from, List<CardDataTemplate> to)
    {
        foreach(CardDataTemplate card in from)
        {
            to.Add(card);
        }
    }
    public void RemoveCards()
    {
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
