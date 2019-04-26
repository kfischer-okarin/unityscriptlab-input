using System;

using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      public class Trigger : Value<bool> {
        Func<InputSystem, bool> getValue;

        public Trigger(string name, Func<InputSystem, bool> getValue) : base(name) {
          this.getValue = getValue;
        }

        public override bool GetValue(InputSystem input) {
          return getValue(input);
        }

        public override bool ShouldBroadcast(bool value, bool lastValue) {
          return value || lastValue;
        }
      }
    }
  }
}
