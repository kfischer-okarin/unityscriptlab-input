using System;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

using static Tests.Helper.Matchers;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Value;

namespace Tests {
    public class InputEventTests {
        [Test]
        public void BoundEventListTest() {
            InputEvent.ResetBindings();

            TriggerInput t = new TriggerInput("t");
            ScalarInput v = new ScalarInput("v");
            Action<bool> boolAction = _ => { };
            Action<bool> boolAction2 = _ => { };
            Action<float> floatAction = _ => { };

            t.Updated += boolAction;
            t.Updated += boolAction2;
            v.Updated += floatAction;
            Assert.That(InputEvent.BoundEvents, ContainsExactly(t, v));

            t.Updated -= boolAction;
            Assert.That(InputEvent.BoundEvents, ContainsExactly(t, v));

            t.Updated -= boolAction2;
            Assert.That(InputEvent.BoundEvents, ContainsExactly(v));

            InputEvent.ResetBindings();
            Assert.That(InputEvent.BoundEvents, Is.Empty);
        }
    }
}
