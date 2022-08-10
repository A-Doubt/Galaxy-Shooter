using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-8, 8), 8f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 8f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if other is a player
        Debug.Log($"other.tag: {other.tag}, this: {this}");
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().DamagePlayer();
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
