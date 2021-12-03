using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private InputMaster Controls;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private bool MouseRotating;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Controls = new InputMaster();

    }
    private void FixedUpdate()
    {
        MoveMent();
        if (MouseRotating)
        {
            MouseRotation();
        }
        else
        {
            JoystickRotation();
        }
    }

    private void MoveMent()
    {
        Vector2 InputVector = Controls.Player.Movement.ReadValue<Vector2>();
        rb.MovePosition(new Vector3(InputVector.x, 0, InputVector.y) * Speed * Time.deltaTime + rb.position);
    }
    private void JoystickRotation()
    {
        Vector2 InputVector = Controls.Player.JoystickDirection.ReadValue<Vector2>();
        float TurnAngle = Mathf.Atan2(InputVector.y, InputVector.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle, 0f));
    }
    private void MouseRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Controls.Player.MouseDirection.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hitinfo, 300f))
        {
            Vector3 Target = hitinfo.point;
            Vector3 LookDirection = Target - transform.position;
            float TurnAngle = Mathf.Atan2(LookDirection.z, LookDirection.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0f, -TurnAngle, 0f));
        }
    }
    private void OnEnable()
    {
        Controls.Enable();
    }
    private void OnDisable()
    {
        Controls.Disable();
    }
}
