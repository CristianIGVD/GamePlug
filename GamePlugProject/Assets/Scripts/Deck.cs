using UnityEngine;

public class Deck : MonoBehaviour
{
    public StandardCard[] cards;

    public StandardCard DrawFrom ()
    {
        return cards[Random.Range(0, cards.Length - 1)];
    }

    public void Discard (StandardCard card)
    {
        // do stuff here
    }
}
