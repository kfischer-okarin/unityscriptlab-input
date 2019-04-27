using System;

namespace UnityScriptLab.Input.Value.Provider {
  /// <summary>
  /// Converts one type of InputValue to another one
  /// </summary>
  public class Adapter<In, Out> : ValueProvider<Out> {
    In currentValue;
    Func<In, Out> convert;

    public Adapter(InputValue<In> input, Func<In, Out> convert) {
      input.Updated += v => currentValue = v;
      this.convert = convert;
    }

    public Out GetValue(InputSystem input) {
      return convert(currentValue);
    }
  }
}
