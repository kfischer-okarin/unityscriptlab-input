using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    /// <summary>
    /// Wrapper around UnityEngine.Input.
    /// </summary>
    public class UnityInputSystem : InputSystem {
      public UnityInputSystem() { }

      public bool GetKey(KeyCode key) => UnityEngine.Input.GetKey(key);
      public bool GetKeyDown(KeyCode key) => UnityEngine.Input.GetKeyDown(key);
      public bool GetKeyUp(KeyCode key) => UnityEngine.Input.GetKeyUp(key);
      public float GetAxis(string name) => UnityEngine.Input.GetAxis(name);
      public float GetAxisRaw(string name) => UnityEngine.Input.GetAxisRaw(name);
    }
  }
}
