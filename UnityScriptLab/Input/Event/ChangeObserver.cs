using System;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      public class ChangeObserver<In> : ValueProvider<bool> {
        bool updated;
        In lastValue;
        In currentValue;
        Func<In, In, bool> trigger;

        /// <param name="name">Unique name of the event</param>
        public ChangeObserver(InputValue<In> input, Func<In, In, bool> trigger) {
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
  }
}
