using System;
using UnityEngine;

[Serializable]
public class ModuleData
{
    public string id;
    public int moduleNumber;
    public string iconId;
    public string path;
    public string title;
    public string focus;
    public string whatIDid;
    public string keyResult;
    public string whatILearned;
    public string reflection;
    public string playHook;
    public string thumbnailKey;
    public string outcomeLabel;
    public string playLabel;
    public bool canPlay;
    public string playType;
    public string outcomeType;
}
[Serializable]
public class ModuleDatabase
{
    public ModuleData[] modules;
}
