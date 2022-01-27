using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private ItemsBase[] _storageTypes;

    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private int _zSize;

    [SerializeField] private float _cellSpacing;
    [SerializeField] private float _yOffset;

    private GameObject[] _storageCellPositions;
    private ItemsBase[] _storagedObjects;

    private int _storageEmptyPointer;

    private bool _receiving = true;
    private ItemsBase deliveringItem;

    public ItemsBase[] StorageTypes { get => _storageTypes; set => _storageTypes = value; }
    public bool Receiving { get => _receiving; set => _receiving = value; }

    private void Start()
    {
        _storageCellPositions = new GameObject[_xSize * _ySize * _zSize];
        _storagedObjects = new ItemsBase[_xSize * _ySize * _zSize];

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

                    _storageCellPositions[pointer] = cell;

                    pointer++;
                }
            }
        }
    }

    private void Update()
    {
        Delivery();
    }

    private void Delivery()
    {
        if (_receiving == false)
        {
            GameObject targetCell = _storageCellPositions[_storageEmptyPointer];

            deliveringItem.transform.position = Vector3.Lerp(deliveringItem.transform.position, targetCell.transform.position, Time.deltaTime * 10f);

            if (Vector3.Distance(deliveringItem.transform.position, targetCell.transform.position) < 0.01f) //can be optimized
            {
                deliveringItem.transform.SetParent(targetCell.transform);
                _receiving = true;

                FinishDelivery();
            }
        }
    }

    public ItemsBase GetItem()
    {
        int tempPointer =  _storageEmptyPointer;

        _storageEmptyPointer--;

        return _storagedObjects[tempPointer];
    }

    public void SetItem(ItemsBase storagableObject)
    {
        if (_storageEmptyPointer < _storagedObjects.Length)
        {
            StartDelivery(storagableObject);
        }
        else
        {
            //overflow event
        }
    }

    private void StartDelivery(ItemsBase targetObj)
    {
        _receiving = false;
        deliveringItem = targetObj;
    }

    private void FinishDelivery()
    {
        _storagedObjects[_storageEmptyPointer] = deliveringItem;
        _storageEmptyPointer++;
    }
}
