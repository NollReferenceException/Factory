using System;
using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _followableObject;
    [Tooltip("_smooth value between 0 and 100.")] [SerializeField] private float _smooth;

    private Vector3 _startingDistance;

    private void Start()
    {
        _startingDistance = transform.position - _followableObject.transform.position;
        gameObject.GetComponent<Camera>().transform.LookAt(_followableObject);
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 position = transform.position;
        float tempPositionY = position.y;

        position = Vector3.Lerp(position, _followableObject.position + _startingDistance, _smooth * Time.deltaTime);

        position = new Vector3(position.x, tempPositionY, position.z);

        transform.position = position;
    }
}