using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionCube : MonoBehaviour
{
    [SerializeField] GameObject t9;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedButton")
        {
            Destroy(t9);
        }
    }
}
