using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : BaseFactory
{
    [SerializeField] private Storage[] inPutStorages;
    [SerializeField] private Storage outPutStorage;

    [SerializeField] private Item product;
    [Range(0.1f, 9999f)] [SerializeField] private float productionSpeedFactory = 1;

    private Coroutine productionCoroutine;

    bool creatingAllowed = true;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
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
        if (inPutStorages.Length > 0)
        {
            productionCoroutine = StartCoroutine(Craft());
        }
        else
        {
            productionCoroutine = StartCoroutine(Create());
        }
    }

    private IEnumerator Craft()
    {
        while (!outPutStorage.CheckOverflow())
        {
            creatingAllowed = true;

            for (int i = 0; i < inPutStorages.Length; i++)
            {
                if (!inPutStorages[i].ItemsToProduction())
                {
                    Debug.Log(inPutStorages[i].ItemsToProduction());
                    creatingAllowed = false;
                }
            }

            if (creatingAllowed)
            {
                Item item = Instantiate(product, transform);
                outPutStorage.PutInItem(item);

                yield return new WaitForSeconds(productionSpeedFactory * item.ProductionSpeed);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private IEnumerator Create()
    {
        while (!outPutStorage.CheckOverflow())
        {
            Item item = Instantiate(product, transform);
            outPutStorage.PutInItem(item);

            yield return new WaitForSeconds(productionSpeedFactory * item.ProductionSpeed);
        }
    }
}
