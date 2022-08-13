using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private Player _player;
    private Animator _animator;
    [SerializeField] AudioSource _explosionSoundSource;


    void Start()
    {
        _explosionSoundSource = GetComponent<AudioSource>();

        if (_explosionSoundSource == null)
        {
            Debug.Log("Enemy, _explosionSoundSource is null");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 8.0f);

        _animator = GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("_player is null");
        }
        if (_animator == null)
        {
            Debug.LogError("_animator is null");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            float randomX = Random.Range(-10.3f, 9.3f);
            transform.position = new Vector3(randomX, 10f);
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

            _animator.SetTrigger("OnEnemyDestroy");

            _explosionSoundSource.Play();

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(1);
            }

            _animator.SetTrigger("OnEnemyDestroy");

            _explosionSoundSource.Play();

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }
}
