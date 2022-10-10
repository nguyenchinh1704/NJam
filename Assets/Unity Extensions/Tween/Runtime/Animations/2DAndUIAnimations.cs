#define USE_TEXT_MESH_PRO
#define USE_UGUI

using UnityEngine;
using System;

#if USE_TEXT_MESH_PRO
using TMPro;
#endif

#if USE_UGUI
/*using Spine.Unity;*/
using UnityEngine.UI;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace UnityExtensions.Tween
{
#if USE_UGUI

    [Serializable, TweenAnimation("2D and UI/Material Image Float", "Material Image Float")]
    public class TweenMaterialFloat: TweenFloat<Image>
    {
        public string _fieldName;
        public bool _isCloneMat;
#if UNITY_EDITOR
        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            base.OnPropertiesGUI(player, property);
            UnityEditor.EditorGUILayout.PropertyField(property.FindPropertyRelative("_fieldName"));
            UnityEditor.EditorGUILayout.PropertyField(property.FindPropertyRelative("_isCloneMat"));
        }
#endif

        protected Material cacheMat;
        public override float current
        {
            get
            {
                if (!cacheMat)
                {
                    cacheMat = target.material;
                    if (_isCloneMat && !target.material.name.Contains("Instanced"))
                    {
                        cacheMat =  new Material(target.material);
                        cacheMat.name += "Instanced";
                    }

                    if (_isCloneMat)
                    {
                        target.material = cacheMat;
                    }
                 
                }

                if (cacheMat)
                {
                    return cacheMat.GetFloat(_fieldName);
                }

                return 0;
            }
            set {  
                if (!cacheMat)
                {
                    cacheMat = target.material;
                    if (_isCloneMat && !target.material.name.Contains("Instanced"))
                    {
                        cacheMat =  new Material(target.material);
                        cacheMat.name += "Instanced";
                    }

                    if (_isCloneMat)
                    {
                        target.material = cacheMat;
                    }
                 
                }

                if (cacheMat)
                {
                    cacheMat.SetFloat(_fieldName,value);
                }
                
            }
        }

    }
    
    
   /* [Serializable, TweenAnimation("2D and UI/Material Skeleton Float", "Material Skeleton Float")]*/
    /*public class TweenSkeletonMaterialFloat: TweenFloat<SkeletonGraphic>
    {
        public string _fieldName;
        public bool _isCloneMat;
#if UNITY_EDITOR
        protected override void OnPropertiesGUI(TweenPlayer player, SerializedProperty property)
        {
            base.OnPropertiesGUI(player, property);
            UnityEditor.EditorGUILayout.PropertyField(property.FindPropertyRelative("_fieldName"));
            UnityEditor.EditorGUILayout.PropertyField(property.FindPropertyRelative("_isCloneMat"));
        }
#endif

        protected Material cacheMat;
        public override float current
        {
            get
            {
                if (!cacheMat)
                {
                    cacheMat = target.material;
                    if (_isCloneMat && !target.material.name.Contains("Instanced"))
                    {
                        cacheMat =  new Material(target.material);
                        cacheMat.name += "Instanced";
                    }

                    if (_isCloneMat)
                    {
                        target.material = cacheMat;
                    }
                 
                }

                if (cacheMat)
                {
                    return cacheMat.GetFloat(_fieldName);
                }

                return 0;
            }
            set {  
                if (!cacheMat)
                {
                    cacheMat = target.material;
                    if (_isCloneMat && !target.material.name.Contains("Instanced"))
                    {
                        cacheMat =  new Material(target.material);
                        cacheMat.name += "Instanced";
                    }

                    if (_isCloneMat)
                    {
                        target.material = cacheMat;
                    }
                 
                }

                if (cacheMat)
                {
                    cacheMat.SetFloat(_fieldName,value);
                }
                
            }
        }

    }*/
    [Serializable, TweenAnimation("2D and UI/Canvas Group Alpha", "Canvas Group Alpha")]
    public class TweenCanvasGroupAlpha : TweenFloat<CanvasGroup>
    {
        public override float current
        {
            get => target ? target.alpha : 1f;
            set { if (target) target.alpha = value; }
        }

    }

    [Serializable, TweenAnimation("2D and UI/Graphic Color", "Graphic Color")]
    public class TweenGraphicColor : TweenColor<Graphic>
    {
        public override Color current
        {
            get => target ? target.color : Color.white;
            set
            {
                if (target)
                {
                    target.color = value;
                }
            }
        }

    }
    // [Serializable, TweenAnimation("2D and UI/Group Color", "Group Color")]
    // public class TweenGroupColor : TweenColor<GroupColorUI>
    // {
    //     public override Color current
    //     {
    //         get => target ? target.Color : Color.white;
    //         set { if (target) target.Color = value; }
    //     }
    //
    // }
    [Serializable, TweenAnimation("2D and UI/Prefer Width", "Prefer Width")]
    public class TweenPreferWidth: TweenFloat<LayoutElement>
    {
        public override float current
        {
            get => target ? target.preferredWidth : 0;
            set { if (target) target.preferredWidth = value; }
        }

    }
    [Serializable, TweenAnimation("2D and UI/Prefer Height", "Prefer Height")]
    public class TweenPreferHeight: TweenFloat<LayoutElement>
    {
        public override float current
        {
            get => target ? target.preferredHeight : 0;
            set { if (target) target.preferredHeight = value; }
        }

    }
    [Serializable, TweenAnimation("2D and UI/TMP Color", "TMP Color")]
    public class TweenTMPColor : TweenColor<TextMeshPro>
    {
        public override Color current
        {
            get => target ? target.color : Color.white;
            set { if (target) target.color = value; }
        }
    }

    [Serializable, TweenAnimation("2D and UI/Image Fill Amount", "Image Fill Amount")]
    public class TweenImageFillAmount : TweenFloat<Image>
    {
        public override float current
        {
            get => target ? target.fillAmount : 1;
            set { if (target) target.fillAmount = value; }
        }
    }


    [Serializable, TweenAnimation("2D and UI/Grid Layout Group Cell Size", "Grid Layout Group Cell Size")]
    public class TweenGridLayoutGroupCellSize : TweenVector2<GridLayoutGroup>
    {
        public override Vector2 current
        {
            get => target ? target.cellSize : default;
            set { if (target) target.cellSize = value; }
        }
    }

    [Serializable, TweenAnimation("2D and UI/Grid Layout Group Spacing", "Grid Layout Group Spacing")]
    public class TweenGridLayoutGroupSpacing : TweenVector2<GridLayoutGroup>
    {
        public override Vector2 current
        {
            get => target ? target.spacing : default;
            set { if (target) target.spacing = value; }
        }
    }

#endif

    [Serializable, TweenAnimation("2D and UI/Sprite Color", "Sprite Color")]
    public class TweenSpriteColor : TweenColor<SpriteRenderer>
    {
        public override Color current
        {
            get => target ? target.color : Color.white;
            set { if (target) target.color = value; }
        }
    }
    [Serializable, TweenAnimation("2D and UI/Sliced SpriteRender", "Sliced SpriteRender")]
    public class TweenSpriteRenderSliceSize: TweenVector2<SpriteRenderer>
    {
        public override Vector2 current
        {
            get => target ? target.size : default;
            set { if (target) target.size = value; }
        }
    }

#if USE_TEXT_MESH_PRO

    [Serializable, TweenAnimation("2D and UI/Text Mesh Pro Font Size", "Text Mesh Pro Font Size")]
    public class TextMeshProFontSize : TweenFloat<TMP_Text>
    {
        public override float current
        {
            get => target ? target.fontSize : 1f;
            set { if (target) target.fontSize = value; }
        }
    }

#endif

} // namespace UnityExtensions.Tween