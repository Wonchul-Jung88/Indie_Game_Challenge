using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatsDatabase : ScriptableObject
{
    [SerializeField] Stats[] stats;

    public Stats GetStatReference(string statID)
    {
        foreach (Stats stat in stats)
        {
            if (stat.ID == statID)
            {
                return stat;
            }
        }
        return null;
    }

    public Stats GetStatCopy(string statID)
    {
        Stats stat = GetStatReference(statID);
        return stat != null ? stat.GetCopy() : null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadStats();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged -= LoadStats;
        EditorApplication.projectChanged += LoadStats;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadStats;
    }

    private void LoadStats()
    {
        stats = FindAssetsByType<Stats>("Assets/Stats");
    }

    // Slightly modified version of this answer: http://answers.unity.com/answers/1216386/view.html
    public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).Name;

        string[] guids;
        if (folders == null || folders.Length == 0)
        {
            guids = AssetDatabase.FindAssets("t:" + type);
        }
        else
        {
            guids = AssetDatabase.FindAssets("t:" + type, folders);
        }

        T[] assets = new T[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
        return assets;
    }
#endif
}
