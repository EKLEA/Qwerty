using System;

[System.Serializable]

public class ExampleItem : IItem
{

    public IItemInfo info { get; set; }

    public IItemState state { get; set; }
    public ExampleItem(IItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IItem Clone()
    {
        var clonedExampleItem = new ExampleItem(info);
        clonedExampleItem.state.count = state.count;
        return clonedExampleItem;
    }
}
