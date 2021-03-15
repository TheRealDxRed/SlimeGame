using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, GameControls.IPlayerActions {
    GameControls controls;

    void Awake() {
        controls = new GameControls();
        controls.Player.SetCallbacks(this);
    }

    void OnEnable() {
        controls.Player.Enable();
    }

    void OnDisable() {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context) {
        gameObject.SendMessage("MoveInput", context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context) {
        gameObject.SendMessage("JumpInput", context.ReadValueAsButton());
    }
}
