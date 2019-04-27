using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnityScriptLab {
    namespace Input {
        namespace Event {
            /// <summary>
            /// Value emitted by the input system, to which handlers can be subscribed.
            /// </summary>
            public class ScalarInput : InputValue<float> {
                public ScalarInput(string name, ValueProvider<float> provider) : base(name, provider) { }

                public ScalarInput(string name, Func<InputSystem, float> getValue) : base(name, getValue) { }

                public ScalarInput(string name) : this(name, _ => 0) { }

                public override bool ShouldBroadcast(float value, float lastValue) {
                    return !Mathf.Approximately(value, lastValue);
                }

                /// <summary>
                /// InputValue that returns the absolute value of this InputValue.
                /// </summary>
                public ScalarInput WithoutSign {
                    get {
                        return new ScalarInput($"{name}-WithoutSign", new Adapter<float, float>(this, v => Mathf.Abs(v)));
                    }
                }

                /// <summary>
                /// InputTrigger that activates every frame this InputValue is over the specified treshold.
                /// </summary>
                public TriggerInput IsOver(float threshold) {
                    return new TriggerInput($"{name}-IsOver-{threshold}", new Adapter<float, bool>(this, v => v > threshold));
                }

                /// <summary>
                /// InputTrigger that activates every frame this InputValue is below the specified treshold.
                /// </summary>
                public TriggerInput IsBelow(float threshold) {
                    return new TriggerInput($"{name}-IsBelow-{threshold}", new Adapter<float, bool>(this, v => v < threshold));
                }

                /// <summary>
                /// InputTrigger that activates when this InputValue surpasses the specified threshold.
                /// </summary>
                public TriggerInput Surpassed(float threshold) {
                    return new TriggerInput($"{name}-Surpassed-{threshold}",
                        new ChangeObserver<float>(this, (before, after) => before <= threshold && after > threshold));
                }

                /// <summary>
                /// InputTrigger that activates when this InputValue falls below the specified threshold.
                /// </summary>
                public TriggerInput FellBelow(float threshold) {
                    return new TriggerInput($"{name}-FellBelow-{threshold}",
                        new ChangeObserver<float>(this, (before, after) => before >= threshold && after < threshold));
                }

                public static ScalarInput operator +(ScalarInput x, ScalarInput y) {
                    return new ScalarInput($"{x}-plus-{y}", new Combinator<float, float, float>(x, y, (v1, v2) => v1 + v2));
                }

                public static ScalarInput operator -(ScalarInput x, ScalarInput y) {
                    return new ScalarInput($"{x}-plus-{y}", new Combinator<float, float, float>(x, y, (v1, v2) => v1 - v2));
                }
            }
        }
    }
}
