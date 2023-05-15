using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    //Follow the player
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    
}
