using UnityEngine;
using ItemType = PickupableComponent.ItemType;

public class LootComponent : MonoBehaviour
{
    [SerializeField]
    public ItemType itemTypeToCollect;

    [SerializeField]
    public AudioSource collectItemAudioSource;

    [SerializeField]
    public int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupableComponent = collision.gameObject.GetComponent<PickupableComponent>();
        if (pickupableComponent)
        {
            var isItemTypeTheSame = itemTypeToCollect == pickupableComponent.GetTypePickableItem();
            if (isItemTypeTheSame)
            {
                pickupableComponent.PickupItem();
                count++;
                if (collectItemAudioSource)
                {
                    collectItemAudioSource.Play();
                }
                Debug.Log(count);
            }
        }
    }
}
