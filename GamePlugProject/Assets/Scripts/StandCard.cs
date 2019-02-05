using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (
    fileName = "Card",
    menuName = "Cards/StandardCard",
    order = 1)]
public class StandardCard : ScriptableObject
{
   public enum Elemental
    {
        Neutrak, Fire, Water, Air, Ground
    }

    public Elemental element;
    public string title;
    public string description;
    public int health;
    public int attackPower;

    public virtual void SpecialMove() { }
}
