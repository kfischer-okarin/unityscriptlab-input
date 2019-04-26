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

        public Trigger(string name) : this(name, _ => false) { }

        public override bool GetValue(InputSystem input) {
          return getValue(input);
        }

        public override bool ShouldBroadcast(bool value, bool lastValue) {
          return value || lastValue;
        }

        public Trigger And(Trigger other) {
          return new AndTrigger($"{name}+{other.name}", this, other);
        }
      }

      public class AndTrigger : Trigger {
        bool value1;
        bool value2;

        public AndTrigger(string name, Trigger trigger1, Trigger trigger2) : base(name) {
          trigger1.Updated += v => value1 = v;
          trigger2.Updated += v => value2 = v;
        }

        public override bool GetValue(InputSystem _) {
          return value1 && value2;
        }
      }
    }
  }
}
