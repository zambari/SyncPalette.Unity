using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR // if you want expandable pallets from within provider inspector
using UnityEditor;

#endif
// #if UNITY_EDITOR // if you want expandable pallets from within provider inspector
//using UnityEditor;
//     [CustomPropertyDrawer(typeof(Z.YOUR_INHERITED_TYPE), true)]    public class TypeDrawer : ScriptableObjectDrawer{ }
// #endif
namespace Z
{
    public class PaletteBase<TypeNamed, TypeValue> : ScriptableObject, IPalette
                                   where TypeNamed : NamedValue<TypeValue>, new()
    {
        protected Dictionary<string, TypeValue> objectDict;

        [HideInInspector]
        public string[] validNames { get { return _validNames; } }
        [HideInInspector] public string[] _validNames = new string[0];
        public System.Action onValueChanged;
        protected virtual TypeNamed[] GetValues() { throw new System.NotImplementedException("Please override"); } // cannot make it abstract, sorry mono framework
        protected virtual void SetValues(TypeNamed[] newValues) { throw new System.NotImplementedException("Please override"); } // cannot make it abstract, sorry mono framework
        public ScriptableObject scriptableObject { get { return this; } }
        public virtual void OnValidate()
        {
            InitializeDictionary();
            if (onValueChanged != null) onValueChanged();
            else
            {
                //            Debug.Log("no listenerrs " + name);
            }
            SetAsDirty();
        }
        TypeValue GetValue(MonoBehaviour source, string s)
        {
            return GetValue(s);
        }
        public TypeValue GetValue(string s)
        {
            TypeValue o;
            if (string.IsNullOrEmpty(s))
                return default(TypeValue);
            if (objectDict == null)
            {
                InitializeDictionary();
            }
            if (objectDict == null)
            {
                Debug.Log("pleas initialize object dictionary");
                return default(TypeValue);
            }

            if (objectDict.TryGetValue(s, out o))
                return o;
         //   Debug.Log(typeof(TypeNamed).ToString() + ": Name requested (" + s + ")not present in the dictionary (click to highlight)");
            return default(TypeValue);
        }
        public void ClearEvents()
        {
            onValueChanged = null;
        }
        public virtual TypeNamed CrateItem()
        {
            return new TypeNamed();
        }
        public void InitializeDictionary()
        {
            InitializeDictionary(GetValues());
        }
        public void SetAsDirty()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public void InitializeDictionary(TypeNamed[] vals)
        {

            if (vals == null)
            {
                Debug.Log("no values");
                return;
            }
            objectDict = new Dictionary<string, TypeValue>();

            var nameslist = new List<string>();
            for (int i = 0; i < vals.Length; i++)
            {
                if (string.IsNullOrEmpty(vals[i].name))
                    vals[i].name = zExt.RandomString(4);
                while (objectDict.ContainsKey(vals[i].name))
                    vals[i].name += zExt.RandomString(1);

                objectDict.Add(vals[i].name, vals[i].value);
                nameslist.Add(vals[i].name);
            }
            _validNames = nameslist.ToArray();
        }
        /// <summary>
        /// Used for the editor
        /// </summary>
        /// <value></value>
        public virtual void AddSyncer(GameObject target)
        {
            throw new System.NotImplementedException("manually add syncer type to child class");
        }
        public string AddNew()
        {
            var ValList = new List<TypeNamed>();
            var currentVals = GetValues();
            if (currentVals != null)
                ValList.AddRange(currentVals);
            TypeNamed NewObj = CrateItem();
            NewObj.name = zExt.RandomString(4);
            ValList.Add(NewObj);
            SetValues(ValList.ToArray());
            return NewObj.name;
        }
        public int CountUsage(string searchName)
        {
            int found = 0;
            var allMonos = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)) as MonoBehaviour[];
            var synces = allMonos.ToList().Where(g => (g as IValueSync != null)).ToArray();
            for (int i = 0; i < synces.Length; i++)
                if ((synces[i] as IValueSync).GetNamedType() == typeof(TypeNamed))
                    if ((synces[i] as IValueSync).presetName == searchName)
                        found++;
            return found;
        }
        public void Rename(string oldName, string newName)
        {
            int index = -1;


            var ValList = new List<TypeNamed>();
            var currentVals = GetValues();
            ValList.AddRange(currentVals);
            for (int i = 0; i < ValList.Count; i++)
            {
                if (ValList[i].name == oldName)
                {
                    index = i;
                    break;
                };
            }
            if (index == -1)
            {
                Debug.Log("old name " + oldName + " not found, aborintg");
                return;
            }
            var allMonos = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)) as MonoBehaviour[];
            var synces = allMonos.ToList().Where(g => (g as IValueSync != null)).ToArray();
            for (int i = 0; i < synces.Length; i++)
            {
                if ((synces[i] as IValueSync).GetNamedType() == typeof(TypeNamed))
                {
                    if ((synces[i] as IValueSync).presetName == oldName)
                    {
                        (synces[i] as IValueSync).presetName = newName;
                    }
                }
            }
            currentVals[index].name = newName;
            SetValues(ValList.ToArray());
            InitializeDictionary();
        }
        public void Duplicate(int index) // doent's work properlys
        {

            var ValList = new List<TypeNamed>();
            var currentVals = GetValues();
            ValList.AddRange(currentVals);

            ValList.Insert(index, ValList[index]);
            SetValues(ValList.ToArray());
        }
        public void Remove(int index)
        {

            var ValList = new List<TypeNamed>();
            var currentVals = GetValues();
            ValList.AddRange(currentVals);
            ValList.RemoveAt(index);
            SetValues(ValList.ToArray());

        }
        public void Move(int startIndex, int endindex)
        {
            var ValList = new List<TypeNamed>();
            var currentVals = GetValues();
            ValList.AddRange(currentVals);
            TypeNamed thisVal = ValList[startIndex];
            ValList.Remove(thisVal);
            ValList.Insert(endindex, thisVal);
            SetValues(ValList.ToArray());
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);

#endif
        }
        public string FindName(TypeValue value)
        {
            var values = GetValues();
            if (values == null) return null;
            for (int i = 0; i < values.Length; i++)
            {
                TypeValue val1 = values[i].value;
                if (value.Equals(val1)) return values[i].name;
            }
            return null;
        }
        public string AddValue(TypeValue val)
        {
            string currentName = FindName(val);
            if (!string.IsNullOrEmpty(currentName))
            {
                Debug.Log("value was found as " + currentName);
                return currentName;
            }

            string newName = System.DateTime.Now.ToString();
            var currentVals = GetValues();
            var ValList = new List<TypeNamed>();
            ValList.AddRange(currentVals);

            var type = typeof(TypeNamed);
            var obj = (TypeNamed)System.Activator.CreateInstance(type);
            obj.name = newName;
            obj.value = val;
            ValList.Add(obj);
            SetValues(ValList.ToArray());

            InitializeDictionary();
#if UnityEditor
EditorUtility.SetDirty(this); // does this even do anyghin
#endif
            return newName;
        }

        public System.Type GetNamedType()
        {
            return typeof(TypeNamed);
        }

        protected void OnEnable()
        {
            InitializeDictionary(GetValues());
            //    Debug.Log("pal OnEnable " + typeof(TypeNamed).ToString());
        }
        protected virtual void Awake()
        {
            //            Debug.Log("palette awake " + typeof(TypeNamed).ToString());
            var vals = GetValues();
            if (vals == null || vals.Length == 0)
            {
                AddDefaultValues();
            }
            InitializeDictionary(vals);
        }
        protected virtual void AddDefaultValues()
        {

        }

        public bool ContainsName(string name)
        {
            return objectDict.ContainsKey(name);
        }
    }
}
