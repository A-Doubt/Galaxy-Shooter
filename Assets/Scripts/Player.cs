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

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
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
        Vector3 offset = new Vector3(0, 0.75f);
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
}
