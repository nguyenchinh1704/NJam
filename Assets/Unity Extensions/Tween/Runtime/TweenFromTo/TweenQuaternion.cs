using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityExtensions.Tween
{
    public abstract class TweenQuaternion<TTarget> : TweenFromTo<Quaternion, TTarget> where TTarget : Object
    {
        public override void Interpolate(float factor)
        {
            current = Quaternion.SlerpUnclamped(From, destiny, factor);
        }

        public override Quaternion Addictive(Quaternion a, Quaternion b)
        {
            return Quaternion.Euler( a.eulerAngles + b.eulerAngles);
        }

#if UNITY_EDITOR

        Quaternion _fromQuaternion = Quaternion.identity;
        Vector3 _fromEulerAngles = Vector3.zero;
        Quaternion _toQuaternion = Quaternion.identity;
        Vector3 _toEulerAngles = Vector3.zero;


        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            base.OnPropertiesGUI(player, property);
            var (runtimeFromProp,fromProp, toProp) = GetFromToProperties(property);
            EditorGUILayout.PropertyField(runtimeFromProp);
            var disableFrom = runtimeFromProp.boolValue;
            if (_fromQuaternion != fromProp.quaternionValue)
            {
                fromProp.quaternionValue = _fromQuaternion = fromProp.quaternionValue.normalized;
                _fromEulerAngles = _fromQuaternion.eulerAngles;
            }

            if (_toQuaternion != toProp.quaternionValue)
            {
                toProp.quaternionValue = _toQuaternion = toProp.quaternionValue.normalized;
                _toEulerAngles = _toQuaternion.eulerAngles;
            }

            bool3 fromChanged, toChanged;

            FromToFieldLayout("X", ref _fromEulerAngles.x, ref _toEulerAngles.x, out fromChanged.x, out toChanged.x,disableFrom);
            FromToFieldLayout("Y", ref _fromEulerAngles.y, ref _toEulerAngles.y, out fromChanged.y, out toChanged.y,disableFrom);
            FromToFieldLayout("Z", ref _fromEulerAngles.z, ref _toEulerAngles.z, out fromChanged.z, out toChanged.z,disableFrom);

            if (fromChanged.anyTrue) fromProp.quaternionValue = _fromQuaternion = Quaternion.Euler(_fromEulerAngles).normalized;
            if (toChanged.anyTrue) toProp.quaternionValue = _toQuaternion = Quaternion.Euler(_toEulerAngles).normalized;
        }

#endif // UNITY_EDITOR

    } // class TweenQuaternion<TTarget>

} // namespace UnityExtensions.Tween