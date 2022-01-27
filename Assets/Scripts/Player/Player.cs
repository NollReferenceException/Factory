using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private VariableJoystick _variableJoystick;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Camera playerCamera;


    private CharacterController _characterController;

    private Vector3 _direction;
    

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {

        if (JoystickTouching())
        {
            _direction = playerCamera.transform.forward * _variableJoystick.Vertical + playerCamera.transform.right * _variableJoystick.Horizontal;
            _direction = Vector3.Scale(_direction, new Vector3(1, 0, 1));

            LookAhead(_direction);
        }
        else
        {
            _direction = Vector3.zero;
        }

        _characterController.SimpleMove(_direction * _speed);
    }

    bool JoystickTouching()
    {
        return (_variableJoystick.Vertical * _variableJoystick.Vertical) + (_variableJoystick.Horizontal * _variableJoystick.Horizontal) > 0;
    }

    void LookAhead(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * _rotationSpeed
            );
        }
    }
}
