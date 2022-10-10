using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityExtensions.Tween
{
    public abstract class TweenFloat<TTarget> : TweenFromTo<float, TTarget> where TTarget : Object
    {
        public override float Addictive(float a, float b)
        {
            return  a + b;
        }

        public override void Interpolate(float factor)
        {
            current = (destiny - From) * factor + From;
        }

#if UNITY_EDITOR

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            base.OnPropertiesGUI(player, property);

            var (runtimeFromProp,fromProp, toProp) = GetFromToProperties(property);
            EditorGUILayout.PropertyField(runtimeFromProp);
            var disableFrom = runtimeFromProp.boolValue;
            FromToFieldLayout("Value", fromProp, toProp,disableFrom);
        }

#endif

    } // class TweenFloat<TTarget>

} // namespace UnityExtensions.Tween