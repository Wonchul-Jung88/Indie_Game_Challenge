using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Stat")]
public class Stats : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string StatName;
    [Range(1, 999)]
    public int MaximumStacks = 1;

    protected static readonly StringBuilder sb = new StringBuilder();

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual Stats GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {

    }

    public virtual string GetItemType()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
