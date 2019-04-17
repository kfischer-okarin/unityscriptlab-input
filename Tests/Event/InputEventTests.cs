using System;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class InputEventTests {
            NUnit.Framework.Constraints.Constraint ContainsExactly(params InputEvent[] evs) {
                return Is.EquivalentTo(evs);
            }

            [Test]
            public void BoundEventListTest() {
                InputEvent.ResetBindings();

                InputTrigger ev1 = new InputTrigger("test", default);
                InputTrigger ev2 = new InputTrigger("test2", default);
                Action action = () => { };
                Action secondAction = () => { };

                ev1.Triggered += action;
                ev1.Stopped += secondAction;
                ev2.Stopped += action;
                Assert.That(InputEvent.BoundEvents, ContainsExactly(ev1, ev2));

                ev1.Triggered -= action;
                Assert.That(InputEvent.BoundEvents, ContainsExactly(ev1, ev2));

                ev1.Stopped -= secondAction;
                Assert.That(InputEvent.BoundEvents, ContainsExactly(ev2));

                InputEvent.ResetBindings();
                Assert.That(InputEvent.BoundEvents, Is.Empty);
            }
        }
    }
}
