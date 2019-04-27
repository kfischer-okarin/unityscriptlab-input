using System;

using NSubstitute;

using NUnit.Framework;

using UnityScriptLab.Input;
using UnityScriptLab.Input.Event;

namespace Tests {
  namespace Helper {
    public class ValueSpy<T> {
      InputValue<T> val;
      T newValue;
      bool updated;
      InputSystem input;

      public ValueSpy(InputValue<T> val) {
        this.val = val;
        input = Substitute.For<InputSystem>();
        val.Updated += v => {
          updated = true;
          newValue = v;
        };
        val.SetInputSystem(input);
      }

      public void SimulateInput(Action<InputSystem> action) {
        updated = false;
        action?.Invoke(input);
        val.HandleInput();
      }

      public void WaitFrame() {
        SimulateInput(null);
      }

      public void AssertWasUpdatedTo(T value) {
        Assert.That(updated, Is.True, "But wasn't updated");
        Assert.That(newValue, Is.EqualTo(value), $"But was updated to {newValue}");
      }

      public void AssertNothingHappened() {
        Assert.That(updated, Is.False, "But was updated");
      }
    }

    public class TriggerStub : TriggerInput {
      public TriggerStub(string name = "Stub") : base(name) { }

      public void Update(bool value) {
        Broadcast(value);
      }
    }

    public class ScalarStub : ScalarInput {
      public ScalarStub(string name = "Stub") : base(name) { }

      public void Update(float value) {
        Broadcast(value);
      }
    }
  }
}
