using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    public class UnityInputSystem : InputSystem {
      public UnityInputSystem() { }

      public bool GetKey(KeyCode key) => UnityEngine.Input.GetKey(key);
      public bool GetKeyDown(KeyCode key) => UnityEngine.Input.GetKeyDown(key);
      public bool GetKeyUp(KeyCode key) => UnityEngine.Input.GetKeyUp(key);
    }
  }
}
