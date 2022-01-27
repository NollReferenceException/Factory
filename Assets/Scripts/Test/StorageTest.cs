using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTest : MonoBehaviour
{
    public Storage storage;
    public ItemsBase testObject;

    public void FillTest()
    {

    }

    private void Start()
    {
        InvokeRepeating("Setter", 1.0f,1f);
    }

    private void Setter()
    {
        ItemsBase testItem = Instantiate(testObject, null);

        if (storage.Receiving)
        {
            storage.SetItem(testItem);
        }

        //storage.SetItem(testItem);
    }
}
