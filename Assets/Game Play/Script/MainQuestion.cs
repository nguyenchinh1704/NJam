using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainQuestion : MonoBehaviour
{
    [SerializeField] GameObject pQ1;
    public void Close()
    {
        Destroy(pQ1);
        
    }
}
