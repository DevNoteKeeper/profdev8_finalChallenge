using System;
using UnityEngine;


public class DesktopManager : MonoBehaviour
{
    [SerializeField] private TextAsset moduleJson;
    private ModuleDatabase moduleDatabase;

    private void Awake()
    {
        if(moduleJson == null)
        {
            Debug.LogError("Module Json isn't assigned");
            return;
        }

        moduleDatabase = JsonUtility.FromJson<ModuleDatabase>(moduleJson.text);

        if(moduleDatabase == null  || moduleDatabase.modules == null)
        {
            Debug.LogError("Failed to parse module JSON");
            return;
        }
    }

    // receive icon title when double click icon
    public void OnIconClicked(string title)
    {
        string moduleID = ExtractModuleId(title);
        if (string.IsNullOrEmpty(title)){
            Debug.LogError("Could not extract module id from " + title);
            return;
        }
        ModuleData data = FindModuleById(moduleID);

        Debug.Log("=== MODULE FOUND ===");
        Debug.Log("ID: " + data.id);
        Debug.Log("Title: " + data.title);
        Debug.Log("Focus: " + data.focus);
        Debug.Log("Outcome Type: " + data.outcomeType);
        Debug.Log("Can Play: " + data.canPlay);
        Debug.Log("Path: " + data.path);
    }

    // extract id ex) M1: Design & Scrum -> M1
    private string ExtractModuleId(string title)
    {
        string[] parts = title.Split(':');
        if (parts.Length == 0) { return ""; }
        return parts[0].Trim();
    }

    // find module data using module Id
    private ModuleData FindModuleById(string moduleID)
    {
        foreach(ModuleData module in moduleDatabase.modules)
        {
            if(module.id == moduleID)
            {
                return module;
            }
        }
        return null;
    }
}
