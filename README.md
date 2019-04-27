# UnityScriptLab - Input

This package was modeled roughly after the new InputSystem of Unity.

## Example

Add the InputProcessor component somewhere in your scene.

Add bindings like this:

```csharp
using UnityScriptLab.Input;
using UnityEngine;

public class Player : MonoBehaviour {

  bool jump;

  void Awake() {
    InputAction<bool> jump = new InputAction<bool>("Jump");
    jump.Bind(Key.Pressed(KeyCode.Space));
    jump.Triggered += (value, _) => jump = value;
  }

  void Update() {
    if (jump) {
      // Jump
    }
  }
}
```
