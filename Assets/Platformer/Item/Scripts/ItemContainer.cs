using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] UnityEvent onAddItem;

    List<Item> items;

    public void AddItem(Item item) {
        if (items == null) {
            items = new List<Item>{};
        }
        items.Add(item);
        onAddItem.Invoke();
    }
}
