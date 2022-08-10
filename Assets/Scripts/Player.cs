using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _lastShotTime = 0;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is NULL.");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && _lastShotTime + _fireRate < Time.time)
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        Vector3 offset = new Vector3(0, 0.9f);
        Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        _lastShotTime = Time.time;
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalinput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(
            transform.position.x, 
            Mathf.Clamp(transform.position.y, -4f, 0),
            transform.position.z);

        if (transform.position.x <= -11.2 || transform.position.x >= 11.2)
        {
            transform.position = new Vector3(-(transform.position.x), transform.position.y, 0);
        }
    }

    public void DamagePlayer()
    {
        _lives--;

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
