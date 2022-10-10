using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityExtensions.Tween
{
    [System.Serializable]
    public abstract class TweenVector2<TTarget> : TweenFromTo<Vector2, TTarget> where TTarget : Object
    {
        public bool2 toggle;

        public override void Interpolate(float factor)
        {
            if (toggle.anyTrue)
            {
                var t = toggle.allTrue ? default : current;

                if (toggle.x) t.x = (destiny.x - From.x) * factor + From.x;
                if (toggle.y) t.y = (destiny.y - From.y) * factor + From.y;

                current = t;
            }
        }

        public override Vector2 Addictive(Vector2 a, Vector2 b)
        {
            return a + b;
        }

#if UNITY_EDITOR

        public override void Reset(TweenPlayer player)
        {
            base.Reset(player);
            toggle = default;
        }


        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            base.OnPropertiesGUI(player, property);

            var (runtimeFromProp,fromProp, toProp) = GetFromToProperties(property);
            EditorGUILayout.PropertyField(runtimeFromProp);
            var disableFrom = runtimeFromProp.boolValue;
            var toggleProp = property.FindPropertyRelative(nameof(toggle));

            FromToFieldLayout("X",
                fromProp.FindPropertyRelative(nameof(Vector2.x)),
                toProp.FindPropertyRelative(nameof(Vector2.x)),
                toggleProp.FindPropertyRelative(nameof(bool2.x)),disableFrom);
            FromToFieldLayout("Y",
                fromProp.FindPropertyRelative(nameof(Vector2.y)),
                toProp.FindPropertyRelative(nameof(Vector2.y)),
                toggleProp.FindPropertyRelative(nameof(bool2.y)),disableFrom);
        }

#endif // UNITY_EDITOR

    } // class TweenVector2<TTarget>

} // namespace UnityExtensions.Tween