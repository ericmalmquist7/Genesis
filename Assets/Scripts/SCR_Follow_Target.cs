using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Follow_Target : MonoBehaviour
{
    private GameObject target;
    public Vector3 positionOffset;
    public float speed;

    public bool teleportOnMaxDistance;
    public float maxDistance;

    private void Start()
    {
        target = SCR_Scene_Manager.playerReference;
    }

    private void FixedUpdate()
    {
        Vector3 targetLocation = target.transform.position + positionOffset;
        if (teleportOnMaxDistance && (targetLocation - transform.position).magnitude > maxDistance)
        {
            transform.position = targetLocation;
        }
        else
        {
            Vector3 velocity = (targetLocation - transform.position) * speed;
            transform.position += velocity;

        }
    }

}
