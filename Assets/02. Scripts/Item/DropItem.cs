using UnityEngine;

// 역할: 드롭 아이템 습득 감지 및 인벤토리 전달만 담당
public class DropItem : MonoBehaviour
{
    [SerializeField] private DropType dropType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        inventory.AddItem(dropType);
        gameObject.SetActive(false);
    }
}
