using System;

namespace UnityScriptLab {
  namespace Input {
    namespace Value {
      public class Adapter<In, Out> : ValueProvider<Out> {
        In currentValue;
        Func<In, Out> convert;

        /// <param name="name">Unique name of the event</param>
        public Adapter(InputValue<In> input, Func<In, Out> convert) {
          input.Updated += v => currentValue = v;
          this.convert = convert;
        }

        /// <summary>
        /// Get the current value of the input.
        /// </summary>
        public Out GetValue(InputSystem input) {
          return convert(currentValue);
        }
      }
    }
  }
}
