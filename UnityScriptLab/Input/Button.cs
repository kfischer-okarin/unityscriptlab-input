namespace UnityScriptLab {
    namespace Input {
        public class Button : InputControl {
            string buttonName;
            public Button(string buttonName) {
                this.buttonName = buttonName;
            }

            public event InputControlEvent Triggered;

            public void Update() {
                if (UnityEngine.Input.GetButtonDown(buttonName)) {
                    Triggered();
                }
            }
        }
    }
}
