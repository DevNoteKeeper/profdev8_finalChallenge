using System;
using UnityEngine;

[Serializable]
public class ModuleData
{
    public string id;
    public int moduleNumber;
    public string iconId;
    public string iconTitle;
    public string path;
    public string title;
    public string focus;
    public string whatIDid;
    public string keyResult;
    public string whatILearned;
    public string reflection;
    public string[] tags;
    public string completed;

    public string playHook;
    public string thumbnailKey;

    public string outcomeLabel;
    public string outcomeTitle;
    public OutcomeType outcomeType;
    public string url;
    public Sprite[] images;

    public string playLabel;
    public bool canPlay;
    public string playType;
}
[Serializable]
public class ModuleDatabase
{
    public ModuleData[] modules;
}
public enum OutcomeType
{
    None,
    Url,
    ImageGallery
}
