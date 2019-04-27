namespace UnityScriptLab.Input.Value.Provider {
  public interface ValueProvider<T> {
    T GetValue(InputSystem input);
  }
}
