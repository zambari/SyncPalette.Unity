using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{
    public interface IPalette
    {
        string[] validNames { get; }
        void Duplicate(int index);
        void Remove(int index);
        void Move(int startIndex, int endindex);
        string AddNew();
        void SetAsDirty();
        int CountUsage(string name);
        bool ContainsName(string name);
        void Rename(string oldName, string name);
        void OnValidate();
        void AddSyncer(GameObject target);
        void ClearEvents();
        void InitializeDictionary();
        ScriptableObject scriptableObject { get; }
        System.Type GetNamedType();
    }
    public interface IPaletteUpdater
    {
        void UpdatePalette();
        GameObject gameObject { get; }
        string name { get; }
    }
    public interface IPaletteProvider
    {
        bool IsOfType(System.Type p);
        IPalette palette { get; }
        void RegisterListener(IPaletteUpdater source);
        void UnRegisterListener(IPaletteUpdater source);
        //        System.Action onPaletteChange {get;set;}
        //System.Action onPaletteChange {get;set;}
        GameObject gameObject { get; }
        string name { get; }
    }
    public interface INamedSelectionProvider
    {
        string[] GetNames(System.Type T);
    }
    public interface IValueSync
    {
        System.Type GetNamedType();
        System.Type GetValueType();
        System.Type GetPaletteType();
        NameSelectionDrawer GetDrawer();
        GameObject gameObject { get; }
        string name { get; }
        string presetName { get; set; }
        void UpdateValue();
    }
    public interface IGetCurrentValue
    {
        T GetCurrentValue1<T>();
    }

    public interface IAddCurrentValue
    {
        void AddCurrentValue();
    }
    public static class GetExtentsions
    {

    }
}