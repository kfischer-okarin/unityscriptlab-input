using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            string buttonName;
            public Button(string buttonName) { 
                this.buttonName = buttonName; 
            }

            public event TriggerEvent Triggered;

            public void Update() {
                if (UnityEngine.Input.GetButtonDown(buttonName)) {
                    Triggered();
                }
            }
        }
    }
}
