using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      /// <summary>
      /// Event triggered by the input system, to which handlers can be subscribed.
      /// </summary>
      public class InputValueTrigger : InputTrigger {
        bool updated;
        float lastValue;
        float newValue;
        Func<float, bool> newValueCondition;
        Func<float, bool> lastValueCondition;

        public InputValueTrigger(string name, InputValue value, Func<float, bool> newValueCondition, bool updateOnly, Func<float, bool> lastValueCondition):
          base(name) {
            value.Updated += v => {
              updated = true;
              lastValue = newValue;
              newValue = v;
            };
            this.triggerCondition = _ => {
              if (updateOnly && !updated) {
                return false;
              }
              return newValueCondition(newValue) && lastValueCondition(lastValue);
            };
            this.stopCondition = i => !this.triggerCondition(i);
          }

        public InputValueTrigger(string name, InputValue value, Func<float, bool> newValueCondition, Func<float, bool> lastValueCondition):
          this(name, value, newValueCondition, true, lastValueCondition) { }

        public InputValueTrigger(string name, InputValue value, Func<float, bool> newValueCondition, bool updateOnly):
          this(name, value, newValueCondition, updateOnly, _ => true) { }

        public InputValueTrigger(string name, InputValue value, Func<float, bool> newValueCondition):
          this(name, value, newValueCondition, false, _ => true) { }

        public InputValueTrigger(string name, InputValue value):
          this(name, value, _ => true, true, _ => true) { }

        public override void HandleInput() {
          base.HandleInput();
          updated = false;
        }
      }
    }
  }
}
