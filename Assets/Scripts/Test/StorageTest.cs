using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTest : MonoBehaviour
{
    public Storage storage;
    public Item testObject;
    private int itemIntex = 0;

    private void Start()
    {
        InvokeRepeating("StorageFiller", 1.0f, 1f);
    }

    private void StorageFiller()
    {
        Item testItem = Instantiate(testObject, null);
        testItem.name = $"{itemIntex++}";

        storage.SetItem(testItem);
    }
}
