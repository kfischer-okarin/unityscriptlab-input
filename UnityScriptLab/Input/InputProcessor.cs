using UnityEngine;

using UnityScriptLab.Input;

public class InputProcessor : MonoBehaviour {
    void Update() {
        foreach (InputEvent ev in InputEvent.BoundEvents) {
            ev.HandleInput();
        }
    }
}
