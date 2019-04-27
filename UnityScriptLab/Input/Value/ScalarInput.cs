using System;

using UnityEngine;

namespace UnityScriptLab.Input.Value {
  using Provider;

  public class ScalarInput : InputValue<float> {
    public ScalarInput(string name, ValueProvider<float> provider) : base(name, provider) { }

    public ScalarInput(string name, TriggerInput trigger) : this(name, new TriggerAsScalar(trigger)) { }

    public ScalarInput(string name, TriggerInput positive, TriggerInput negative) : this(name, new TriggerAsAxis(positive, negative)) { }

    public ScalarInput(string name, Func<InputSystem, float> getValue) : base(name, getValue) { }

    public ScalarInput(string name) : this(name, _ => 0) { }

    public override bool ShouldBroadcast(float value, float lastValue) {
      return !Mathf.Approximately(value, lastValue);
    }

    /// <summary>
    /// Absolute value of this InputValue.
    /// </summary>
    public ScalarInput WithoutSign {
      get {
        return new ScalarInput($"{name}-WithoutSign", new Adapter<float, float>(this, v => Mathf.Abs(v)));
      }
    }

    public TriggerInput IsOver(float threshold) {
      return new TriggerInput($"{name}-IsOver-{threshold}", new Adapter<float, bool>(this, v => v > threshold));
    }

    public TriggerInput IsBelow(float threshold) {
      return new TriggerInput($"{name}-IsBelow-{threshold}", new Adapter<float, bool>(this, v => v < threshold));
    }

    public TriggerInput Surpassed(float threshold) {
      return new TriggerInput($"{name}-Surpassed-{threshold}",
        new ChangeObserver<float>(this, (before, after) => before <= threshold && after > threshold));
    }

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

    class TriggerAsScalar : Adapter<bool, float> {
      public TriggerAsScalar(TriggerInput trigger) : base(trigger, Calculate) { }

      private static float Calculate(bool active) => active ? 1 : 0;
    }

    class TriggerAsAxis : Combinator<bool, bool, float> {
      public TriggerAsAxis(TriggerInput positive, TriggerInput negative) : base(positive, negative, Calculate) { }

      private static float Calculate(bool positive, bool negative) => (positive ? 1 : 0) + (negative ? -1 : 0);
    }
  }
}
