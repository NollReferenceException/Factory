using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private Item[] _storageTypes;
    [SerializeField] private bool ignoreItemTypes;

    [SerializeField] private int _xSize = 1;
    [SerializeField] private int _ySize = 1;
    [SerializeField] private int _zSize = 1;

    [SerializeField] private float _yOffset = 0;
    [SerializeField] private float _xOffset = 0;
    [SerializeField] private float _zOffset = 0;

    [SerializeField] private float _cellSpacing = 1;

    [SerializeField] private int requestedQuantity = 1;


    private bool initialized;

    private GameObject[] _storageCellobj;
    private Item[] _storagedObjects;

    private int _storageEmptyPointer;

    public bool Delivering { get; private set; }
    public bool Full { get; private set; }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (initialized)
        {
            return;
        }
        else
        {
            _storageCellobj = new GameObject[_xSize * _ySize * _zSize];
            _storagedObjects = new Item[_xSize * _ySize * _zSize];

            _storageEmptyPointer = 0;

            GenerateStorage();

            initialized = true;
        }
    }
    private void GenerateStorage()
    {
        int pointer = 0;

        for (int y = 0; y < _ySize; y++)
        {
            for (int x = 0; x < _xSize; x++)
            {
                for (int z = 0; z < _zSize; z++)
                {
                    GameObject cell = new GameObject($"cell : {x},{y},{z}");

                    cell.transform.SetParent(transform);

                    cell.transform.position = new Vector3(
                        transform.position.x + _xOffset + (_cellSpacing * x),
                        transform.position.y + _yOffset + (_cellSpacing * y),
                        transform.position.z + _zOffset + (_cellSpacing * z)
                        );

                    _storageCellobj[pointer] = cell;

                    pointer++;
                }
            }
        }
    }

    public bool CheckFreeSlots()
    {
        if (_storageEmptyPointer >= _storagedObjects.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ItemsToProduction()
    {
        if (Delivering)
        {
            return;
        }
        else
        {
            if (CheckRequestedQuantityItems())
            {
                for (int j = 0; j < requestedQuantity; j++)
                {
                    Destroy(ExtractLastItem().gameObject);
                }
            }
        }
    }

    public bool CheckRequestedQuantityItems()
    {
        if (Delivering)
        {
            return false;
        }
        else
        {
            int itemsFoundedCounter = 0;

            for (int i = 0; i < _storagedObjects.Length; i++)
            {
                if (_storagedObjects[i])
                {
                    itemsFoundedCounter++;

                    if (itemsFoundedCounter >= requestedQuantity)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public void SendItemTo(Storage destStorage)
    {
        if (!destStorage.CheckFreeSlots() || destStorage.Delivering)
        {
            return;
        }
        else
        {
            if (destStorage.CheckItemType(GetLastItem()))
            {
                destStorage.PutInItem(ExtractLastItem());
            }
        }
    }

    public void PutInItem(Item storagableItem)
    {
        if (CheckFreeSlots())
        {
            if (storagableItem)
            {
                StartDelivery(storagableItem);
            }
        }
        else
        {
            //overflow event
        }
    }

    private bool CheckItemType(Item srcItem)
    {
        if (ignoreItemTypes)
        {
            return true;
        }
        else if(srcItem != null)
        {
            for (int i = 0; i < _storageTypes.Length; i++)
            {
                if (srcItem.GetType() == _storageTypes[i].GetType())
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }

    private Item ExtractLastItem()
    {
        if (Delivering)
        {
            return null;
        }
        else
        {
            if (_storageEmptyPointer > 0) _storageEmptyPointer--;

            Item itemToExport = _storagedObjects[_storageEmptyPointer];

            _storagedObjects[_storageEmptyPointer] = null;

            itemToExport?.transform.SetParent(null);

            return itemToExport;
        }
    }

    private Item GetLastItem()
    {
        if (Delivering)
        {
            return null;
        }
        else
        {
            if(_storageEmptyPointer > 0)
            {
                return _storagedObjects[_storageEmptyPointer - 1];
            }
            else
            {
                return null;
            }
        }
    }

    private void StartDelivery(Item targetItem)
    {
        Delivering = true;

        _storagedObjects[_storageEmptyPointer] = targetItem;

        Deliverier delivelier = new GameObject("Delivelier").AddComponent<Deliverier>();
        delivelier.Init(targetItem, _storageCellobj[_storageEmptyPointer]);
        delivelier.Delivered += FinishDelivery;

        delivelier.transform.SetParent(transform);

        _storageEmptyPointer++;
    }

    private void FinishDelivery()
    {
        Delivering = false;
    }
}
