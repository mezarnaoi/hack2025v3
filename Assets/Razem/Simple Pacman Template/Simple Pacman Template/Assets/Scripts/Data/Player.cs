using System.Collections.Generic;

[System.Serializable]
public class Player
{
    public List<int> idPurchasedItems = new List<int>();
    public int idEquipedItem;
    public int coins;
    public int highscore;
}