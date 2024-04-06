using UnityEngine;

[CreateAssetMenu(fileName = "New Egg", menuName = "Items/Egg")]

public class Eggs : Item
{
    public EggType EggType;
    public int Score; 
}

public enum EggType
{
    Common = 0,
    Rare = 1,
    Epic = 2,
}
