using UnityEngine;
using System;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using UnityExtensions.Editor;
#endif

namespace UnityExtensions.Tween
{
    interface ITweenFromTo
    {
        void SwapFromWithTo();
    }

    interface ITweenUnmanaged
    {
        void LetFromEqualCurrent();
        void LetToEqualCurrent();
    }

    interface ITweenFromToWithTarget
    {
        UnityEngine.Object target { get; }
        void LetCurrentEqualFrom();
        void LetCurrentEqualTo();
    }
    


    [Serializable]
    public abstract class TweenFromTo<T> : TweenAnimation, ITweenFromTo
    {
        public bool runtimeFrom = false;
       
        public T from;
        public T to;
        protected T cacheFrom;

        public virtual T destiny => to;
        public T From
        {
            get => @from;
            set => @from = value;
        }

        public void SwapFromWithTo()
        {
            RuntimeUtilities.Swap(ref from, ref to);
        }

       
#if UNITY_EDITOR

        public override void Reset(TweenPlayer player)
        {
            base.Reset(player);
            From = default;
            to = default;
        }

        protected override void CreateOptionsMenu(GenericMenu menu, TweenPlayer player, int index)
        {
            base.CreateOptionsMenu(menu, player, index);

            menu.AddSeparator(string.Empty);

            menu.AddItem(new GUIContent("Swap 'From' with 'To'"), false, () =>
            {
                Undo.RecordObject(player, "Swap 'From' with 'To'");
                SwapFromWithTo();
            });
        }
        
        protected (SerializedProperty, SerializedProperty,SerializedProperty) GetFromToProperties(SerializedProperty property)
        {
            return
            (
                property.FindPropertyRelative(nameof(runtimeFrom)),
                property.FindPropertyRelative(nameof(from)),
                property.FindPropertyRelative(nameof(to))
            );
        }

#endif // UNITY_EDITOR

    }// class TweenFromTo<T>


    public abstract class TweenUnmanaged<T> : TweenFromTo<T>, ITweenUnmanaged where T : unmanaged
    {
        public bool relative = false;

        public override T destiny => relative ? Addictive(From , to) : to;
        public abstract T Addictive(T a, T b);
        /// <summary>
        /// 当前状态
        /// </summary>
        public abstract T current { get; set; }

        public override void onEnterAnimation()
        {
            base.onEnterAnimation();
            cacheFrom = From;
            if(runtimeFrom){
                LetFromEqualCurrent();
            }
        }

        public void LetFromEqualCurrent()
        {
            From = current;
        }

        public void LetToEqualCurrent()
        {
            to = current;
        }

#if UNITY_EDITOR

        T _temp;

        public override void Reset(TweenPlayer player)
        {
            base.Reset(player);
            From = to = current;
        }

        public override void RecordState()
        {
            _temp = current;
        }

        public override void RestoreState()
        {
            if (cacheSample < 0)
            {
                cacheFrom = From;
            }
            cacheSample = 0;
            From = cacheFrom;
            current = _temp;
        }

        protected override void CreateOptionsMenu(GenericMenu menu, TweenPlayer player, int index)
        {
            base.CreateOptionsMenu(menu, player, index);

            menu.AddItem(new GUIContent("Let 'From' Equal 'Current'"), false, () =>
            {
                Undo.RecordObject(player, "Let 'From' Equal 'Current'");
                LetFromEqualCurrent();
            });

            menu.AddItem(new GUIContent("Let 'To' Equal 'Current'"), false, () =>
            {
                Undo.RecordObject(player, "Let 'To' Equal 'Current'");
                LetToEqualCurrent();
            });
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(relative)));
        }

#endif // UNITY_EDITOR

    } // class TweenUnmanaged<T>


    [Serializable]
    public abstract class TweenFromTo<TValue, TTarget> : TweenUnmanaged<TValue>, ITweenFromToWithTarget
        where TValue : unmanaged
        where TTarget : UnityEngine.Object
    {
        public TTarget target;

        public override void setTarget(Object otarget)
        {
            if (otarget.GetType() == typeof(TTarget) || typeof(TTarget).IsInstanceOfType(otarget))
            {
                this.target = (TTarget) otarget;
            }else if ((otarget is Component || otarget is GameObject))
            {
                target =(TTarget) (Object)(otarget is Component
                    ? ((Component) otarget).gameObject.GetComponent(typeof(TTarget))
                    : ((GameObject) otarget).GetComponent(typeof(TTarget)));
            }
        }

        UnityEngine.Object ITweenFromToWithTarget.target => target;

        public void LetCurrentEqualFrom()
        {
            Interpolate(0f);    // supports toggles
        }

        public void LetCurrentEqualTo()
        {
            Interpolate(1f);    // supports toggles
        }

#if UNITY_EDITOR

        TTarget _originalTarget;

        public override void Reset(TweenPlayer player)
        {
            player.TryGetComponent(out target);
            base.Reset(player);
        }

        public override void RecordState()
        {
            _originalTarget = target;
            base.RecordState();
        }

        public override void RestoreState()
        {
            var currentTarget = target;
            target = _originalTarget;
            base.RestoreState();
            target = currentTarget;
        }

        protected override void CreateOptionsMenu(GenericMenu menu, TweenPlayer player, int index)
        {
            base.CreateOptionsMenu(menu, player, index);

            menu.AddItem(new GUIContent("Let 'Current' Equal 'From'"), () =>
            {
                Undo.RecordObject(target, "Let 'Current' Equal 'From'");
                LetCurrentEqualFrom();
            }, !target);

            menu.AddItem(new GUIContent("Let 'Current' Equal 'To'"), () =>
            {
                Undo.RecordObject(target, "Let 'Current' Equal 'To'");
                LetCurrentEqualTo();
            }, !target);
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            using (DisabledScope.New(player.playing))
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(target)));
            }
            base.OnPropertiesGUI(player,property);
        }

#endif // UNITY_EDITOR

    } // class TweenFromTo<TValue, TTarget>

} // namespace UnityExtensions.Tween