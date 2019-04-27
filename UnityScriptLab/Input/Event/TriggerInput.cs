using System;

using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      public class TriggerInput : InputValue<bool> {
        public TriggerInput(string name, ValueProvider<bool> provider) : base(name, provider) { }

        public TriggerInput(string name, Func<InputSystem, bool> getValue) : base(name, getValue) { }

        public TriggerInput(string name) : this(name, _ => false) { }

        public TriggerInput And(TriggerInput other) {
          return new TriggerInput($"{name}+{other.name}", new Combinator<bool, bool, bool>(this, other, (v1, v2) => v1 && v2));
        }

        public ScalarInput AsScalar(float value = 1) {
          return new ScalarInput($"{name}-AsValue", new Adapter<bool, float>(this, active => active ? value : 0));
        }
      }
    }
  }
}
