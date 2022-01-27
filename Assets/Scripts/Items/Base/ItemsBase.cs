using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemsBase : MonoBehaviour, IStoragable
{
    private bool delivered;
    public bool Delivered { get => delivered; set => delivered = value; }

    Vector3 _storageCellPos;

    private void Delivery()
    {
        transform.position = Vector3.Lerp(transform.position, _storageCellPos, Time.deltaTime * 0.3f);

        if(transform.position == _storageCellPos)
        {
            delivered = true;
        }
    }

    public void GoDelivery(Vector3 storageCellPos)
    {
        _storageCellPos = storageCellPos;
        delivered = false;
    }

    void Update()
    {
        //Delivery();
    }
}
