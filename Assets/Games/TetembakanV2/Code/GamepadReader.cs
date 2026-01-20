using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadReader : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current == null) return;

        Vector2 left = Gamepad.current.leftStick.ReadValue();
        Vector2 right = Gamepad.current.rightStick.ReadValue();

        Debug.Log($"Left: {left} | Right: {right}");

        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            Debug.Log("Button A pressed");

        if (Gamepad.current.leftTrigger.ReadValue() > 0.5f)
            Debug.Log("Left Trigger");
    }
}
