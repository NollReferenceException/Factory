using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private Item[] _storageTypes;

    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private int _zSize;

    [SerializeField] private float _cellSpacing;
    [SerializeField] private float _yOffset;

    private GameObject[] _storageCellobj;
    private Item[] _storagedObjects;

    private int _storageEmptyPointer;

    public bool Delivering { get; private set; }
    public bool Full { get; private set; }

    private void Start()
    {
        _storageCellobj = new GameObject[_xSize * _ySize * _zSize];
        _storagedObjects = new Item[_xSize * _ySize * _zSize];

        _storageEmptyPointer = 0;

        GenerateStorage();
    }

    void GenerateStorage()
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
                        transform.position.x + (_cellSpacing * x),
                        transform.position.y + _yOffset + (_cellSpacing * y),
                        transform.position.z + (_cellSpacing * z)
                        );

                    _storageCellobj[pointer] = cell;

                    pointer++;
                }
            }
        }
    }

    private void Update()
    {
        //Debug.Log($"{_storageEmptyPointer} --- {_storagedObjects.Length}");
    }

    private Item GetItem()
    {
        if (!Delivering)
        {
            if (_storageEmptyPointer > 0) _storageEmptyPointer--;

            Item itemToExport = _storagedObjects[_storageEmptyPointer];

            _storagedObjects[_storageEmptyPointer] = null;

            itemToExport?.transform.SetParent(null);

            Full = false;

            return itemToExport;
        }
        else
        {
            return null;
        }
    }

    public void SendTo(Storage destStorage)
    {
        if(!destStorage.Full && !destStorage.Delivering)
        {
            destStorage.SetItem(GetItem());
        }
    }

    public void SetItem(Item storagableItem)
    {
        {
            if (_storageEmptyPointer < _storagedObjects.Length)
            {
                if(storagableItem)
                {
                    StartDelivery(storagableItem);
                }
            }
            else
            {
                Full = true;
                //overflow event
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
