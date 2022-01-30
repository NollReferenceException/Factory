using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : BaseFactory
{
    [SerializeField] private Storage[] inPutStorages;
    [SerializeField] private Storage outPutStorage;

    [SerializeField] private Item product;
    [Range(0.5f, 9999f)] [SerializeField] private float productionSpeedFactory = 1;

    private Coroutine productionCoroutine;

    private Notice factoryNotice;
    private string alertMessage;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        InitNotice();
        InitStorages();
        StartProduction();
    }

    private void InitStorages()
    {
        for (int i = 0; i < inPutStorages.Length; i++)
        {
            inPutStorages[i].Init();
        }

        outPutStorage.Init();
    }

    private void InitNotice()
    {
        factoryNotice = UIManager.Instance.NoticePanel.CreateNotice();
    }

    public void StopProduction()
    {
        if (productionCoroutine == null)
        {
            return;
        }
        else
        {
            StopCoroutine(productionCoroutine);
        }
    }

    public void StartProduction()
    {
        productionCoroutine = StartCoroutine(Craft());
    }

    private IEnumerator Craft()
    {
        while (true)
        {
            if (StockAvailabilityCheck() && outPutStorage.CheckFreeSlots())
            {
                NoticeRevoke();

                GetItemsFromStorages();

                yield return Produce();

            }
            else
            {
                if (!StockAvailabilityCheck())
                {
                    NotEnoughItemsNoticeVoke();
                }
                else if (!outPutStorage.CheckFreeSlots())
                {
                    StorageOverFlowNoticeVoke();
                }

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private IEnumerator Produce()
    {
        Item item = Instantiate(product, transform);

        yield return new WaitForSeconds(productionSpeedFactory * item.ProductionSpeed);

        outPutStorage.PutInItem(item);
    }

    private void NoticeRevoke()
    {
        factoryNotice.Hide();
    }

    private void StorageOverFlowNoticeVoke()
    {
        alertMessage = "Output storage overflow";

        SendNotice();
    }

    private void NotEnoughItemsNoticeVoke()
    {
        alertMessage = "Input storages have not enough items";

        SendNotice();
    }

    private void SendNotice()
    {
        factoryNotice.SetMessage($"{gameObject.name} : {alertMessage}");
        factoryNotice.Show();
    }

    private bool StockAvailabilityCheck()
    {
        for (int i = 0; i < inPutStorages.Length; i++)
        {
            if (!inPutStorages[i].CheckRequestedQuantityItems())
            {
                return false;
            }
        }

        return true;
    }

    private void GetItemsFromStorages()
    {
        for (int i = 0; i < inPutStorages.Length; i++)
        {
            inPutStorages[i].ItemsToProduction();
        }
    }
}
