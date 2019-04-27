using System;

namespace UnityScriptLab.Input.Value.Provider {
  /// <summary>
  /// Emits true when particular change in a InputValue was observed.
  /// </summary>
  public class ChangeObserver<T> : ValueProvider<bool> {
    bool updated;
    T lastValue;
    T currentValue;
    Func<T, T, bool> trigger;

    /// <param name="trigger">Condition for the change to detect. Parameters: (lastValue, currentValue)</param>
    public ChangeObserver(InputValue<T> input, Func<T, T, bool> trigger) {
      input.Updated += v => {
        updated = true;
        lastValue = currentValue;
        currentValue = v;
      };
      this.trigger = trigger;
    }

    /// <summary>
    /// Get the current value of the input.
    /// </summary>
    public bool GetValue(InputSystem input) {
      bool result = updated && trigger(lastValue, currentValue);
      updated = false;
      return result;
    }
  }
}
