using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MasteryDescription : MonoBehaviour
{
    private InputMaster InputSystem;
    private void Start()
    {
        InputSystem = new InputMaster();
        InputSystem.Enable();
    }
    private void Update()
    {
        MoveToPointer();
    }
    private void MoveToPointer()
    {
        Vector2 PointerPos = InputSystem.UI.Position.ReadValue<Vector2>();
        Vector2 DescriptionPos = PointerPos;
        Resolution Resolution = Screen.currentResolution;
        if(PointerPos.y < Resolution.height / 2)
        {
            DescriptionPos += new Vector2(0, 120);
        }
        else
        {
            DescriptionPos += new Vector2(0, -120);
        }
        if(PointerPos.x < (Resolution.width + 350) / 2)
        {
            DescriptionPos += new Vector2(175, 0);
        }
        else
        {
            DescriptionPos += new Vector2(-175, 0);
        }
        transform.position = DescriptionPos;
    }
    private void OnEnable()
    {
        InputSystem.Enable();
    }
    private void OnDisable()
    {
        InputSystem.Disable();
    }
}
