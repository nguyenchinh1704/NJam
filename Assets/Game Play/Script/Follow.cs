using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Transform target;
    Vector3 startingDistance;
    // Start is called before the first frame update
    void Start()
    {
        target = Player.instance.transform;
        startingDistance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowCharacter();
    }
    void FollowCharacter()
    {
        transform.position = new Vector3(target.position.x + startingDistance.x, startingDistance.y, target.position.z + startingDistance.z);
    }
}
