using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneFollow : MonoBehaviour
{
    Transform targett;
    Vector3 startingDistance;
    // Start is called before the first frame update
    void Start()
    {
        targett = Player.instance.transform;
        startingDistance = transform.position - targett.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowBall();
    }
    void FollowBall()
    {
        transform.position = new Vector3(targett.position.x + startingDistance.x, startingDistance.y, targett.position.z + startingDistance.z);
    }
}
