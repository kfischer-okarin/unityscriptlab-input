using UnityEngine;

using UnityInput = UnityEngine.Input;

namespace UnityScriptLab.Input {
  /// <summary>
  /// Wrapper around UnityEngine.Input.
  /// </summary>
  public class UnityInputSystem : InputSystem {
    public UnityInputSystem() { }

    public bool GetKey(KeyCode key) => UnityInput.GetKey(key);
    public bool GetKeyDown(KeyCode key) => UnityInput.GetKeyDown(key);
    public bool GetKeyUp(KeyCode key) => UnityInput.GetKeyUp(key);
    public float GetAxis(string name) => UnityInput.GetAxis(name);
    public float GetAxisRaw(string name) => UnityInput.GetAxisRaw(name);
  }
}
