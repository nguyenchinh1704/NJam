using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    [SerializeField] GameObject destroy;
    public void ChestDestroy()
    {
        Destroy(destroy);
    }


}
