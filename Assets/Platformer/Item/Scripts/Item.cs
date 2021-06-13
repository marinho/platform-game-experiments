using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class Item : MonoBehaviour
{
    [SerializeField] bool stayActiveWhenCollected;
    [SerializeField] [TagSelector] string triggerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            var itemContainer = GetItemContainer();
            itemContainer.AddItem(this);
            if (!stayActiveWhenCollected) {
                gameObject.SetActive(false);
            }
        }
    }

    ItemContainer GetItemContainer() {
        var sceneChildren = gameObject.scene.GetRootGameObjects();
        foreach (var item in sceneChildren)
        {
            ItemContainer itemContainer = item.GetComponent<ItemContainer>();
            if (itemContainer != null) {
                return itemContainer;
            }
        }
        return null;
    }

}
