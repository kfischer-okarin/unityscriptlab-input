using System;

namespace UnityScriptLab.Input.Value {
  using Provider;

  /// <summary>
  /// Value emitted by the input system, to which handlers can be subscribed.
  /// </summary>
  public class InputValue<T> : InputEvent {
    public InputValue(string name, ValueProvider<T> provider) : base(name) {
      this.provider = provider;
    }

    public InputValue(string name, Func<InputSystem, T> getValue) : this(name, new ValueFunction<T>(getValue)) { }

    T value;
    ValueProvider<T> provider;
    event Action<T> updated;

    /// <summary>
    /// Triggered every frame the value was updated.
    /// </summary>
    public event Action<T> Updated {
      add {
        Bind<InputValue<T>>(v => v.updated += value);
      }
      remove {
        Unbind<InputValue<T>>(v => v.updated -= value);
      }
    }

    /// <summary>
    /// Broadcast the specified value to all subscribed listeners.
    /// </summary>
    protected void Broadcast(T value) {
      updated?.Invoke(value);
    }

    /// <summary>
    /// Should return `true` if the new value should be broadcasted to the listeners.
    /// </summary>
    /// <param name="value">Current value</param>
    /// <param name="lastValue">Value of the last frame</param>
    public virtual bool ShouldBroadcast(T value, T lastValue) => !value.Equals(lastValue);

    public override void HandleInput() {
      T newValue = provider.GetValue(this.Input);
      if (ShouldBroadcast(newValue, value)) {
        Broadcast(newValue);
      }
      value = newValue;
    }
  }
}
