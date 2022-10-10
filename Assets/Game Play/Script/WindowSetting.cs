using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;

public class WindowSetting : MonoBehaviour
{
    [SerializeField] UIElement close;
    [SerializeField] UIElement show;

    public void closeButton()
    {
        close.close();
    }
    public void showUI()
    {
        show.show();
    }
}
