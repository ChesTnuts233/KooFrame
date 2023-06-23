/// <summary>
/// 材料类
/// </summary>
public class Item_Material : Item
{

    public Item_Material(int id, string name, ItemType type, ItemQuality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite)
        : base(id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
    }
}
