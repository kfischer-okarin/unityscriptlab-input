using System;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      /// <summary>
      /// Value emitted by the input system, to which handlers can be subscribed.
      /// </summary>
      public class Adapter<In, Out> : Value<Out> {
        In currentValue;
        Func<In, Out> convert;

        /// <param name="name">Unique name of the event</param>
        public Adapter(string name, Value<In> input, Func<In, Out> convert) : base(name) {
          input.Updated += v => currentValue = v;
          this.convert = convert;
        }

        /// <summary>
        /// Get the current value of the input.
        /// </summary>
        public override Out GetValue(InputSystem input) {
          return convert(currentValue);
        }
      }
    }
  }
}
