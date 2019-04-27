using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Value;

namespace Tests {
    namespace Value {
        public class ScalarInputTests {
            [SetUp]
            public void Reset() {
                InputEvent.ResetBindings();
            }

            [Test]
            public void AxisTest() {
                ValueSpy<float> spy = new ValueSpy<float>(ValueOf.Axis("Horizontal"));

                spy.SimulateInput(i => i.GetAxis("Horizontal").Returns(2.0f));
                spy.AssertWasUpdatedTo(2.0f);

                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void RawAxisTest() {
                ValueSpy<float> spy = new ValueSpy<float>(ValueOf.RawAxis("Horizontal"));

                spy.SimulateInput(i => i.GetAxisRaw("Horizontal").Returns(2.0f));
                spy.AssertWasUpdatedTo(2.0f);

                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void WithoutSignTest() {
                ScalarStub original = new ScalarStub("value");
                ValueSpy<float> spy = new ValueSpy<float>(original.WithoutSign);

                original.Update(-2);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(2.0f);
            }

            [Test]
            public void ValueOverTest() {
                ScalarStub value = new ScalarStub();
                ValueSpy<bool> spy = new ValueSpy<bool>(value.IsOver(2.0f));

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(true);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(false);
            }

            [Test]
            public void ValueBelowTest() {
                ScalarStub value = new ScalarStub();
                ValueSpy<bool> spy = new ValueSpy<bool>(value.IsBelow(2.0f));
                value.Update(3.0f);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(true);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(false);
            }

            [Test]
            public void ValueSurpassedTest() {
                ScalarStub value = new ScalarStub();
                ValueSpy<bool> spy = new ValueSpy<bool>(value.Surpassed(2.0f));

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(true);

                spy.WaitFrame();
                spy.AssertWasUpdatedTo(false);

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void ValueFellBelowTest() {
                ScalarStub value = new ScalarStub();
                ValueSpy<bool> spy = new ValueSpy<bool>(value.FellBelow(2.0f));
                value.Update(2.5f);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                value.Update(1.5f);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(true);

                spy.WaitFrame();
                spy.AssertWasUpdatedTo(false);

                value.Update(2.5f);
                spy.WaitFrame();
                spy.AssertNothingHappened();
            }

            [Test]
            public void OperatorPlusTest() {
                ScalarStub a = new ScalarStub("a");
                ScalarStub b = new ScalarStub("b");
                ValueSpy<float> spy = new ValueSpy<float>(a + b);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                a.Update(3);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(3);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                b.Update(2);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(5);
            }

            [Test]
            public void OperatorMinusTest() {
                ScalarStub a = new ScalarStub("a");
                ScalarStub b = new ScalarStub("b");
                ValueSpy<float> spy = new ValueSpy<float>(a - b);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                a.Update(3);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(3);

                spy.WaitFrame();
                spy.AssertNothingHappened();

                b.Update(2);
                spy.WaitFrame();
                spy.AssertWasUpdatedTo(1);
            }
        }
    }
}
