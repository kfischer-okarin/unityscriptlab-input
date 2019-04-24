using System;

using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityEngine;
using UnityEngine.TestTools;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
    namespace Event {
        public class DerivedInputValueTests {
            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
            }

            [Test]
            public void TriggerAsValueTest() {
                InputTriggerStub trigger = new InputTriggerStub();
                InputValueSpy spy = new InputValueSpy(trigger.AsValue());

                spy.WaitFrame();
                spy.AssertNothingHappened();

                trigger.Activate();
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(1);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                trigger.Deactivate();
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(0);
            }

        }
    }
}
