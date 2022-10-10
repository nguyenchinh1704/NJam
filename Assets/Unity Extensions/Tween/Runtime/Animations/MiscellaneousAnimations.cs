﻿using UnityEngine;
using System;
using UnityEngine.Events;
using UnityExtensions.Paths;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityExtensions.Tween
{
    [Serializable, TweenAnimation("Miscellaneous/Sprite Render", "Sprite Render")]
    public class TweenSpriteRender : TweenFloat<SpriteRenderer>
    {
        public float criticalValue = 0.5f;
        public Sprite spriteFrom;
        public Sprite sprite;
        protected Sprite cacheSprite;
        public override float current
        {
            get => (!target || target.sprite == sprite) ? (criticalValue + 0.5f) : (criticalValue - 0.5f);
            set
            {
                if (target) target.sprite = (value >= criticalValue)  ?sprite :  cacheSprite;
            }
        }

        public override void onEnterAnimation()
        {
            base.onEnterAnimation();
            if(sprite)
                 cacheSprite = runtimeFrom ? target.sprite : spriteFrom;
        }

#if UNITY_EDITOR
        public override void Reset(TweenPlayer player)
        {
            criticalValue = 0.5f;
            base.Reset(player);
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(criticalValue)));
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(sprite)));
            if (!runtimeFrom)
            {
                EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(spriteFrom)));
            }
            base.OnPropertiesGUI(player, property);
        }
#endif
    } 
    [Serializable, TweenAnimation("Miscellaneous/Behaviour Enabled", "Behaviour Enabled")]
    public class TweenBehaviourEnabled : TweenFloat<Behaviour>
    {
        public float criticalValue = 0.5f;

        public override float current
        {
            get => (!target || target.enabled) ? (criticalValue + 0.5f) : (criticalValue - 0.5f);
            set { if (target) target.enabled = target.enabled ? (value >= criticalValue) : (value > criticalValue); }
        }

#if UNITY_EDITOR
        public override void Reset(TweenPlayer player)
        {
            criticalValue = 0.5f;
            base.Reset(player);
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(criticalValue)));
            base.OnPropertiesGUI(player, property);
        }
#endif
    } // TweenBehaviourEnabled
    
    [Serializable, TweenAnimation("Miscellaneous/UnityEvent", "UnityEvent")]
    public class TweenUnityEvent : TweenFloat<GameObject>
    {
        public UnityEvent onEvent;
        public override void onEnterAnimation()
        {
            base.onEnterAnimation();
        }

        public override float current
        {
            get => 0;
            set
            {
                if (value >= 0.8)
                {
                    if (Application.isPlaying)
                    {
                        onEvent?.Invoke();
                    }
                    else
                    {
                        Debug.Log("Raise Some Event");
                    }
                }
            }
        }

#if UNITY_EDITOR
        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(onEvent)));
            base.OnPropertiesGUI(player, property);
        }
#endif
    } // TweenBehaviourEnabled


    [Serializable, TweenAnimation("Miscellaneous/GameObject Active", "GameObject Active")]
    public class TweenGameObjectActive : TweenFloat<GameObject>
    {
        public float criticalValue = 0.5f;

        public override float current
        {
            get => (!target || target.activeSelf) ? (criticalValue + 0.5f) : (criticalValue - 0.5f);
            set { if (target) target.SetActive(target.activeSelf ? (value >= criticalValue) : (value > criticalValue)); }
        }

#if UNITY_EDITOR
        public override void Reset(TweenPlayer player)
        {
            criticalValue = 0.5f;
            base.Reset(player);
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(criticalValue)));
            base.OnPropertiesGUI(player, property);
        }
#endif
    } // TweenGameObjectActive



    [Serializable, TweenAnimation("Miscellaneous/Particle System Playing", "Particle System Playing")]
    public class TweenParticleSystemPlaying : TweenFloat<ParticleSystem>
    {
        public bool withChildren;
        public float criticalValue = 0.5f;

        public override float current
        {
            get => (!target || target.isPlaying) ? (criticalValue + 0.5f) : (criticalValue - 0.5f);
            set
            {
                if (target)
                {
                    if (target.isPlaying)
                    {
                        if (value < criticalValue) target.Stop(withChildren, ParticleSystemStopBehavior.StopEmittingAndClear);
                    }
                    else
                    {
                        if (value > criticalValue) target.Play(withChildren);
                    }
                }
            }
        }

#if UNITY_EDITOR
        public override void Reset(TweenPlayer player)
        {
            withChildren = true;
            criticalValue = 0.5f;
            base.Reset(player);
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(withChildren)));
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(criticalValue)));
            base.OnPropertiesGUI(player, property);
        }
#endif
    } // TweenGameObjectActive


    [Serializable, TweenAnimation("Miscellaneous/Position Along Path", "Position Along Path")]
    public class TweenPositionAlongPath : TweenFloat<MoveAlongPath>
    {
        public bool normalizedMode;

        public override float current
        {
            get => target ? (normalizedMode ? (target.path ? target.distance / target.path.length : 0f) : target.distance) : 0;
            set { if (target) target.distance = normalizedMode ? (target.path ? value * target.path.length : 0f) : value; }
        }

#if UNITY_EDITOR
        public override void Reset(TweenPlayer player)
        {
            normalizedMode = false;
            base.Reset(player);
        }

        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative(nameof(normalizedMode)));
            base.OnPropertiesGUI(player, property);
        }
#endif
    }


    [Serializable, TweenAnimation("Miscellaneous/Sub Player Normalized Time", "Sub Player Normalized Time")]
    public class TweenSubPlayerNormalizedTime : TweenFloat<TweenPlayer>
    {
        public override float current
        {
            get => target ? target.normalizedTime : 0;
            set { if (target) target.normalizedTime = Mathf.Clamp(value,0,1); }
        }

#if UNITY_EDITOR

        public override void Reset(TweenPlayer player)
        {
            base.Reset(player);
            From = 0;
            to = 1;
            target = null;
        }

        public override void OnValidate(TweenPlayer player)
        {
            if (target == player)
            {
                target = null;
                Debug.LogError("A TweenPlayer can not be a sub-player of itself!");
            }
        }

#endif
    }

} // namespace UnityExtensions.Tween