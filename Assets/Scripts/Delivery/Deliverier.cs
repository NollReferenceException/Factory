using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliverier : MonoBehaviour
{
    private readonly GameObject deliveringObj;
    private GameObject targetCell;
    private readonly Vector3 destPoint;

    public Deliverier(GameObject deliveringObj, GameObject targetCell, Vector3 destPoint)
    {
        this.deliveringObj = deliveringObj;
        this.targetCell = targetCell;
        this.destPoint = destPoint;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Delivery();
    }

    private void Delivery()
    {
        deliveringObj.transform.position = Vector3.Lerp(deliveringObj.transform.position, destPoint, Time.deltaTime * 10f);

        if (Vector3.Distance(deliveringObj.transform.position, targetCell.transform.position) < 0.01f) //can be optimized
        {
            deliveringObj.transform.SetParent(targetCell.transform);

        }
    }
}
