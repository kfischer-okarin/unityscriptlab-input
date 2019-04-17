using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    /// <summary>
    /// Interface wrapper around Unity's input system (for stubbing).
    /// </summary>
    public interface InputSystem {
      bool GetKeyDown(KeyCode key);
      bool GetKeyUp(KeyCode key);
      bool GetKey(KeyCode key);
      float GetAxis(string name);
      float GetAxisRaw(string name);
    }
  }
}
