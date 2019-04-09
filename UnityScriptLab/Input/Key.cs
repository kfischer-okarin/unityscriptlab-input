using System;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        public class Key : InputControl {
            KeyCode key;

            public Key(KeyCode key): base() {
                this.key = key;
            }

            public Key Press {
                get {
                    this.triggerCondition = () => UnityEngine.Input.GetKeyDown(key);
                    this.stopCondition = () => true;
                    return this;
                }
            }

            public Key Release {
                get {
                    this.triggerCondition = () => UnityEngine.Input.GetKeyUp(key);
                    this.stopCondition = () => true;
                    return this;
                }
            }

            public Key Hold {
                get {
                    this.triggerCondition = () => UnityEngine.Input.GetKey(key);
                    this.stopCondition = () => UnityEngine.Input.GetKeyUp(key);
                    return this;
                }
            }
        }
    }
}
