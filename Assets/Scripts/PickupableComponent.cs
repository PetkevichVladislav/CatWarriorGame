using UnityEngine;

public class PickupableComponent : MonoBehaviour 
{
    [SerializeField]
    public ItemType pickableItemType;

    public ItemType GetTypePickableItem()
    {
        return pickableItemType;
    }

    public void PickupItem()
    {
        Destroy(gameObject);
    }

    public enum ItemType
    {
        Coins = 0,
    }
}
