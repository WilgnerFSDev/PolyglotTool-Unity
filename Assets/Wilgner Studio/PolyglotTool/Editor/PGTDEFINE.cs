using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PGTDEFINE {

    static PGTDEFINE()
    {
        BuildTargetGroup btg = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines_field = PlayerSettings.GetScriptingDefineSymbolsForGroup(btg);
        List<string> defines = new List<string>(defines_field.Split(';'));
        if (!defines.Contains("WS_PGT"))
        {
            defines.Add("WS_PGT");
            PlayerSettings.SetScriptingDefineSymbolsForGroup(btg, string.Join(";", defines.ToArray()));
        }


    }
}
