using System;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      public class FunctionProvider<T> : ValueProvider<T> {
        Func<InputSystem, T> getValue;

        public FunctionProvider(Func<InputSystem, T> getValue) {
          this.getValue = getValue;
        }
        public T GetValue(InputSystem input) {
          return getValue(input);
        }
      }
    }
  }
}
