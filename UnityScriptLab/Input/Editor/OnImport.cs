#if UNITY_EDITOR && (UNITYSCRIPTLAB_INPUT == false)

using UnityEditor;

using UnityEngine;

namespace UnityScriptLab.Input {
  [InitializeOnLoad]
  public class Autorun {
    static Autorun() {
      BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
      string definedSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
      PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, $"{definedSymbols};UNITYSCRIPTLAB_INPUT");
    }
  }
}

#endif
