using System;

namespace UnityScriptLab.Input.Value.Provider {
  /// <summary>
  /// Combines two InputValues into one.
  /// </summary>
  public class Combinator<In1, In2, Out> : ValueProvider<Out> {
    In1 currentValue1;
    In2 currentValue2;
    Func<In1, In2, Out> combine;

    public Combinator(InputValue<In1> input1, InputValue<In2> input2, Func<In1, In2, Out> combine) {
      input1.Updated += v => currentValue1 = v;
      input2.Updated += v => currentValue2 = v;
      this.combine = combine;
    }

    public Out GetValue(InputSystem input) {
      return combine(currentValue1, currentValue2);
    }
  }
}
