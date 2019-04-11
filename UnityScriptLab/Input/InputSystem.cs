using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    public interface InputSystem {
      bool GetKeyDown(KeyCode key);
      bool GetKeyUp(KeyCode key);
      bool GetKey(KeyCode key);
    }
  }
}
