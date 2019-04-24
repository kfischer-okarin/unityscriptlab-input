using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      /// <summary>
      /// InputTrigger that is triggered in response to a InputValue update.
      /// </summary>
      public class InputValueTrigger : InputTrigger {
        bool updated;
        float lastValue;
        float value;

        private InputValueTrigger(string name, InputValue value, Func<float, bool> valueCondition, bool updateOnly, Func<float, bool> lastValueCondition):
          base(name) {
            value.Updated += v => {
              updated = true;
              lastValue = this.value;
              this.value = v;
            };
            this.triggerCondition = _ => {
              if (updateOnly && !updated) {
                return false;
              }
              return valueCondition(this.value) && lastValueCondition(lastValue);
            };
            this.stopCondition = i => !this.triggerCondition(i);
          }

        /// <summary>
        /// Trigger that will activate on a InputValue update under certain conditions.
        /// </summary>
        /// <param name="name">Name of the trigger</param>
        /// <param name="value">InputValue to watch</param>
        /// <param name="valueCondition">Condition for the updated value</param>
        /// <param name="lastValueCondition">Condition for the value before the update</param>
        public InputValueTrigger(string name, InputValue value, Func<float, bool> valueCondition, Func<float, bool> lastValueCondition):
          this(name, value, valueCondition, true, lastValueCondition) { }

        /// <summary>
        /// Trigger that will activate depending on the current value of the InputValue.
        /// </summary>
        /// <param name="name">Name of the trigger</param>
        /// <param name="value">InputValue to watch</param>
        /// <param name="valueCondition">Condition for the updated value</param>
        /// <param name="updateOnly">
        ///   If true, this trigger will only activate on the frame the value was updated, otherwise every frame the condition is fullfilled.
        /// </param> */
        public InputValueTrigger(string name, InputValue value, Func<float, bool> valueCondition, bool updateOnly):
          this(name, value, valueCondition, updateOnly, _ => true) { }

        /// <summary>
        /// Trigger that activates every frame the InputValue passes the condition.
        /// </summary>
        /// <param name="name">Name of the trigger</param>
        /// <param name="value">InputValue to watch</param>
        /// <param name="valueCondition">Condition for the updated value</param>
        public InputValueTrigger(string name, InputValue value, Func<float, bool> valueCondition):
          this(name, value, valueCondition, false) { }

        /// <summary>
        /// Trigger that activates every time the InputValue was updated.
        /// </summary>
        /// <param name="name">Name of the trigger</param>
        /// <param name="value">InputValue to watch</param>
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
