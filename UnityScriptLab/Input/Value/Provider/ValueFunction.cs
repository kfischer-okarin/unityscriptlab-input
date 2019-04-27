using System;

namespace UnityScriptLab.Input.Value.Provider {
  public class ValueFunction<T> : ValueProvider<T> {
    Func<InputSystem, T> getValue;

    public ValueFunction(Func<InputSystem, T> getValue) {
      this.getValue = getValue;
    }
    public T GetValue(InputSystem input) {
      return getValue(input);
    }
  }
}
