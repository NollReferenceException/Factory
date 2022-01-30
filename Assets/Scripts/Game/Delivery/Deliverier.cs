using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Deliverier : MonoBehaviour
{
    private Item deliveringObj;
    private GameObject targetObj;

    private float deliverySpeed = 50f;

    public UnityAction Delivered { get; set; }

    public void Init(Item deliveringObj, GameObject targetObj)
    {
        this.deliveringObj = deliveringObj;
        this.targetObj = targetObj;

        deliveringObj.transform.SetParent(null);
    }
    

    // Update is called once per frame
    void Update()
    {
        Delivery();
    }

    private void Delivery()
    {
        deliveringObj.transform.position = Vector3.MoveTowards(deliveringObj.transform.position, targetObj.transform.position, Time.deltaTime * deliverySpeed);

        if (Vector3.Distance(deliveringObj.transform.position, targetObj.transform.position) < 0.01f) //can be optimized
        {
            DeliveryComplited();
        }
    }

    private void DeliveryComplited()
    {
        deliveringObj.transform.SetParent(targetObj.transform);
        deliveringObj.transform.localPosition = Vector3.zero;
        deliveringObj.transform.localRotation = Quaternion.identity;

        Delivered?.Invoke();

        Destroy(gameObject);
    }
}
