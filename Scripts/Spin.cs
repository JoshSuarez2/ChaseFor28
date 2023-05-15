using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float damage;
    private void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().DamageTaken(damage);
        }
    }
}
