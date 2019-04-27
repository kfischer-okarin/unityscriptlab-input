using System;
using System.Collections.Generic;

namespace UnityScriptLab.Input {
  using Value;

  public class InputAction<T> : InputEvent {
    event Action<T, InputValue<T>> triggered;
    public event Action<T, InputValue<T>> Triggered {
      add {
        Bind<InputAction<T>>(v => v.triggered += value);
      }
      remove {
        Unbind<InputAction<T>>(v => v.triggered -= value);
      }
    }

    bool wasTriggered;
    T triggeredValue;
    InputValue<T> triggerSource;

    public InputAction(string name) : base(name) { }

    public override void HandleInput() {
      if (wasTriggered) {
        triggered?.Invoke(triggeredValue, triggerSource);
        wasTriggered = false;
      }
    }

    public void Bind(InputValue<T> input) {
      input.Updated += value => {
        if (!wasTriggered) {
          triggeredValue = value;
          triggerSource = input;
          wasTriggered = true;
        }
      };
    }
  }
}
