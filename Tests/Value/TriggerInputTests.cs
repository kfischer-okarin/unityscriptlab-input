using NSubstitute;

using NUnit.Framework;

using Tests.Helper;

using UnityEngine;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Value;

namespace Tests.Value {
  public class TriggerInputTests {
    [SetUp]
    public void Reset() {
      InputEvent.ResetBindings();
    }

    [Test]
    public void KeyPressedTest() {
      ValueSpy<bool> spy = new ValueSpy<bool>(Key.Pressed(KeyCode.Space));

      spy.SimulateInput(i => i.GetKeyDown(KeyCode.Space).Returns(true));
      spy.AssertWasUpdatedTo(true);

      spy.SimulateInput(i => i.GetKeyDown(KeyCode.Space).Returns(false));
      spy.AssertWasUpdatedTo(false);
    }

    [Test]
    public void KeyReleasedTest() {
      ValueSpy<bool> spy = new ValueSpy<bool>(Key.Released(KeyCode.Space));

      spy.SimulateInput(i => i.GetKeyUp(KeyCode.Space).Returns(true));
      spy.AssertWasUpdatedTo(true);

      spy.SimulateInput(i => i.GetKeyUp(KeyCode.Space).Returns(false));
      spy.AssertWasUpdatedTo(false);
    }

    [Test]
    public void KeyHeldTest() {
      ValueSpy<bool> spy = new ValueSpy<bool>(Key.Held(KeyCode.Space));

      spy.SimulateInput(i => i.GetKey(KeyCode.Space).Returns(true));
      spy.AssertWasUpdatedTo(true);

      spy.WaitFrame();
      spy.AssertNothingHappened();

      spy.SimulateInput(i => i.GetKey(KeyCode.Space).Returns(false));
      spy.AssertWasUpdatedTo(false);
    }

    [Test]
    public void AndTest() {
      TriggerStub triggerA = new TriggerStub("A");
      TriggerStub triggerB = new TriggerStub("B");
      TriggerInput combined = triggerA.And(triggerB);

      Assert.That(combined.ToString(), Is.EqualTo("A+B"));
      ValueSpy<bool> spy = new ValueSpy<bool>(combined);

      triggerA.Update(true);
      spy.WaitFrame();
      spy.AssertNothingHappened();

      triggerA.Update(false);
      triggerB.Update(true);
      spy.WaitFrame();
      spy.AssertNothingHappened();

      triggerA.Update(true);
      spy.WaitFrame();
      spy.AssertWasUpdatedTo(true);

      triggerA.Update(false);
      spy.WaitFrame();
      spy.AssertWasUpdatedTo(false);

      triggerB.Update(false);
      spy.WaitFrame();
      spy.AssertNothingHappened();
    }

    [Test]
    public void AsScalarTest() {
      TriggerStub trigger = new TriggerStub();
      ValueSpy<float> spy = new ValueSpy<float>(trigger.AsScalar);

      spy.WaitFrame();
      spy.AssertNothingHappened();

      trigger.Update(true);
      spy.WaitFrame();
      spy.AssertWasUpdatedTo(1);

      spy.WaitFrame();
      spy.AssertNothingHappened();

      trigger.Update(false);
      spy.WaitFrame();
      spy.AssertWasUpdatedTo(0);
    }
  }
}
