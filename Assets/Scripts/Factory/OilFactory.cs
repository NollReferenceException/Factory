using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilFactory : BaseFactory
{
    [SerializeField] private Storage inPutStorage;
    [SerializeField] private Storage outPutStorage;

    [SerializeField] private Item product;
    [SerializeField] private float productionSpeed = 1;


    private void Start()
    {
        InvokeRepeating("StorageFiller", 0, productionSpeed);
    }

    private void StorageFiller()
    {
        if(!outPutStorage.CheckFullness())
        {
            Item oil = Instantiate(product, null);
            outPutStorage.SetItem(oil);
        }
    }
}
