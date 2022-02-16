using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Template", order = 1)]
public class CardDataTemplate : ScriptableObject
{
    [SerializeField] GameObject cardModel;
    int value;
    Suits suit;
    
    private void OnValidate()
    {
        if (cardModel == null)
            return;
        char[] temp = cardModel.name.Substring(13, cardModel.name.Length - 13).ToCharArray();
        int startIndex = 1;
        if (char.IsNumber(temp[0]))
        {
            value = temp[0] - 48;
            if (value == 1)
            {
                value *= 10;
                startIndex = 2;
            }
        }
        else
        {
            switch (temp[0])
            {
                case 'J':
                    value = 11;
                    break;

                case 'Q':
                    value = 12;
                    break;

                case 'K':
                    value = 13;
                    break;

                case 'A':
                    value = 14;
                    break;
            }
        }
        switch (temp[startIndex])
        {
            case 'C':
                suit = Suits.Club;
                break;

            case 'D':
                suit = Suits.Diamond;
                break;

            case 'H':
                suit = Suits.Heart;
                break;

            case 'S':
                suit = Suits.Spades;
                break;
        }
        name = value.ToString()+temp[startIndex];
    }
    public enum Suits
    {
        Club,
        Diamond,
        Heart,
        Spades
    }
    public GameObject GetCardModel()
    {
        return cardModel;
    }
    public int GetCardValue()
    {
        return value;
    }
    public Suits GetCardSuit()
    {
        return suit;
    }
}
