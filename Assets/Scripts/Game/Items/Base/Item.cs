using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IStoragable
{
    [Range(0.1f, 9999f)] [SerializeField] private float productionSpeed;

    public float ProductionSpeed { get => productionSpeed; set => productionSpeed = value; }
}
