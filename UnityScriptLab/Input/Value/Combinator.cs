using System;

namespace UnityScriptLab {
  namespace Input {
    namespace Value {
      /// <summary>
      /// Value emitted by the input system, to which handlers can be subscribed.
      /// </summary>
      public class Combinator<In1, In2, Out> : ValueProvider<Out> {
        In1 currentValue1;
        In2 currentValue2;
        Func<In1, In2, Out> combine;

        /// <param name="name">Unique name of the event</param>
        public Combinator(InputValue<In1> input1, InputValue<In2> input2, Func<In1, In2, Out> combine) {
          input1.Updated += v => currentValue1 = v;
          input2.Updated += v => currentValue2 = v;
          this.combine = combine;
        }

        /// <summary>
        /// Get the current value of the input.
        /// </summary>
        public Out GetValue(InputSystem input) {
          return combine(currentValue1, currentValue2);
        }
      }
    }
  }
}
