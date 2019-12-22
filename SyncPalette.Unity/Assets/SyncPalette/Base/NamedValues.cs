using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Z
{

    /// <summary>
    /// Base class to inherit value types from
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [System.Serializable]
    public abstract class NamedValue<T>
    {
        public NamedValue() { }
        public NamedValue(NamedValue<T> source)
        {
            name = source.name;
            value = source.value;
        }
        public string name;
        public T value;
    }

    // inheritances
    [System.Serializable] public class NamedGameObject : NamedValue<GameObject> { public NamedGameObject() { } }
    [System.Serializable] public class NamedColor : NamedValue<Color> { public NamedColor() { } }
    [System.Serializable] public class NamedFont : NamedValue<Font> { public NamedFont() { } }
    [System.Serializable] public class NamedFontSize : NamedValue<int> { public NamedFontSize() { } }
    [System.Serializable] public class NamedAnimationCurve : NamedValue<AnimationCurve> { public NamedAnimationCurve() { } }
    //   [System.Serializable] public class NamedFontSize : NamedValue<FontSizeSync.FontSizeBox> { }

}