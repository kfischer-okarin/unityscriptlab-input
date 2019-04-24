using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
  namespace Input {
    namespace Event {
      /// <summary>
      /// InputTrigger that is triggered in response to a InputValue update.
      /// </summary>
      public class InputTriggerValue : InputValue {
        bool active;
        float lastValue;
        float value;

        public InputTriggerValue(string name, InputTrigger trigger, float value) : base(name) {
          trigger.Triggered += () => active = true;
          trigger.Stopped += () => active = false;
          this.getValue = _ => active ? value : 0;
        }

        public InputTriggerValue(string name, InputTrigger trigger) : this(name, trigger, 1) { }
      }
    }
  }
}
