
using System;
/*using Sirenix.OdinInspector;*/
using UnityEngine;
using UnityExtensions.Tween;
using UnityEngine.Events;

namespace EazyEngine.UI
{
    public interface IBackBehaviour
    {
        void OnBack();
        int GetIndex();
        bool BlockHere();
    }
    public class UIElement : MonoBehaviour, ISerializationCallbackReceiver, IBackBehaviour
    {


        public TweenPlayer localTweenShow;
        public UnityEvent _onShowUnityEvent, _onHideUnityEvent;
        public System.Action onShowEvent, onHideEvent;

        private void OnEnable()
        {
            onShowEvent?.Invoke();
            _onShowUnityEvent?.Invoke();
        }

        private void OnDisable()
        {
            onHideEvent?.Invoke();
            _onHideUnityEvent?.Invoke();
        }

        public void showRelative()
        {
            show(false);
        }
        public void hideRelative()
        {
            close(false);
        }

        public void ShowBool(bool active)
        {
            if (active)
            {
                show();
            }
            else
            {
                close();
            }
        }
        public void show()
        {
            show(false);
        }
        public void show(bool imediately)
        {
            if (!imediately)
            {
                var o = gameObject;
                if (!o.activeSelf)
                    o.SetActive(true);
                if (localTweenShow)
                {
                    localTweenShow.ForcePlayRuntime();
                }
            }
            else
            {
                var o = gameObject;
                if (!o.activeSelf)
                    o.SetActive(true);
                if (localTweenShow)
                {
                    localTweenShow.ForcePlayRuntime();
                    localTweenShow.Sample(1);
                }
            }
        }
        public void close()
        {
            if (localTweenShow)
            {
                localTweenShow.ForcePlayBackRuntime();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        public void close(bool imediately)
        {
            if (imediately)
            {
                if (localTweenShow)
                {
                    localTweenShow.Stop();
                    localTweenShow.normalizedTime = 0;
                    localTweenShow._onBackArrived.Invoke();
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }


        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (localTweenShow)
            {
                localTweenShow.onBackArrived += () =>
                {
                    gameObject.SetActive(false);
                };
            }

        }

        public bool RegisterOnBack;
       /* [ShowIf("RegisterOnBack")]*/
        public int indexLayer;
        public void OnBack()
        {
            if (RegisterOnBack)
            {
                close();
            }
        }

        public int GetIndex()
        {
            return indexLayer;
        }

        public bool BlockHere()
        {
            return RegisterOnBack;
        }
    }
}

