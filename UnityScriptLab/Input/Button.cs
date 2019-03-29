using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            enum State {
                Pressed,
                Released,
            }
            KeyCode key;
            State buttonState;
            public Button(KeyCode key) {
                this.key = key;
                Pressed();
            }

            public Button Pressed() {
                this.buttonState = State.Pressed;
                return this;
            }
            public Button Released() {
                this.buttonState = State.Released;
                return this;
            }

            public event Action Triggered;

            public void Update() {
                if (WasTriggered()) {
                    Triggered?.Invoke();
                }
            }

            bool WasTriggered() {
                switch (buttonState) {
                    case State.Pressed:
                        return UnityEngine.Input.GetKeyDown(key);
                    case State.Released:
                        return UnityEngine.Input.GetKeyUp(key);
                }
                return false;
            }
        }
    }
}
