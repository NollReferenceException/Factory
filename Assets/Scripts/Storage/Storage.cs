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

    private GameObject[] _storageCellobj;
    private ItemsBase[] _storagedObjects;

    private int _storageEmptyPointer;

    private void Start()
    {
        _storageCellobj = new GameObject[_xSize * _ySize * _zSize];
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

                    _storageCellobj[pointer] = cell;

                    pointer++;
                }
            }
        }
    }

    private void Update()
    {
    }


    public ItemsBase GetItem()
    {
        int tempPointer = _storageEmptyPointer;

        if(_storageEmptyPointer > 0) _storageEmptyPointer--;

        ItemsBase itemToExport = _storagedObjects[tempPointer];

        _storagedObjects[tempPointer] = null;

        return itemToExport;
    }

    public void SetItem(ItemsBase storagableObject)
    {
        if (_storageEmptyPointer < _storagedObjects.Length && storagableObject)
        {
            AddItemToStorage(storagableObject);
        }
        else
        {
            //overflow event
        }
    }

    private void AddItemToStorage(ItemsBase targetItem)
    {
        _storagedObjects[_storageEmptyPointer] = targetItem;
        targetItem.transform.SetParent(_storageCellobj[_storageEmptyPointer].transform);
        targetItem.transform.localPosition = Vector3.zero;

        _storageEmptyPointer++;
    }
}
