using System;

using UnityEngine;

namespace UnityScriptLab.Input.Value {
  using Provider;

  public class Vector2Input : InputValue<Vector2> {
    public Vector2Input(string name, ValueProvider<Vector2> provider) : base(name, provider) { }

    public Vector2Input(string name, Func<InputSystem, Vector2> getValue) : base(name, getValue) { }

    public Vector2Input(string name) : this(name, _ => new Vector2(0, 0)) { }

    public override bool ShouldBroadcast(Vector2 value, Vector2 lastValue) {
      return !(Mathf.Approximately(value.x, lastValue.x) && Mathf.Approximately(value.y, lastValue.y));
    }
  }
}
