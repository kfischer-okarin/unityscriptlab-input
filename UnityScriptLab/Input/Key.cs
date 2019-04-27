using UnityEngine;

namespace UnityScriptLab.Input {
  using Value;

  public static class Key {
    public static TriggerInput Pressed(KeyCode key) => new TriggerInput($"KeyPressed-{key}", input => input.GetKeyDown(key));

    public static TriggerInput Released(KeyCode key) => new TriggerInput($"KeyReleased-{key}", input => input.GetKeyUp(key));

    public static TriggerInput Held(KeyCode key) => new TriggerInput($"KeyHeld-{key}", input => input.GetKey(key));
  }
}
