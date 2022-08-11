using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 8.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-10.3f, 9.3f);
            transform.position = new Vector3(randomX, 8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.DamagePlayer();
            }

            Destroy(this.gameObject);
            return;
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null) _player.AddScore(1);
            Destroy(this.gameObject);
            return;
        }
    }
}
