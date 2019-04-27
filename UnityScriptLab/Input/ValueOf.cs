namespace UnityScriptLab.Input {
  using Value;

  public static class ValueOf {
    public static ScalarInput Axis(string name) => new ScalarInput($"Axis-{name}", input => input.GetAxis(name));

    public static ScalarInput RawAxis(string name) => new ScalarInput($"RawAxis-{name}", input => input.GetAxisRaw(name));
  }
}
