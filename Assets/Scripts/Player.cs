using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _fireRate = 0.2f;
    [SerializeField]
    private int _lives = 3;
    private float _lastShotTime = 0;

    [SerializeField]
    private int _score = 0;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _shieldVisuals;
    [SerializeField]
    private GameObject[] _engineFires;

    // prefabs
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;

    // powerups
    private bool _isTripleShot = false;
    private bool _isBonusSpeed = false;
    private bool _isShieldActive = false;
    private float _bonusSpeed = 5.0f;
    private float _powerupDuration = 3.0f;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _laserSoundSource;

    void Start()
    {
        transform.position = new Vector3(0, -2, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _laserSoundSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Player, _spawnManager is null.");
        }
        if (_uiManager == null)
        {
            Debug.Log("Player, _uiManager is null.");
        }
        _shieldVisuals.SetActive(false);
        if (_laserSoundSource == null)
        {
            Debug.Log("Player, _laserSoundSource is null");
        }
        else _laserSoundSource.clip = _laserSoundClip;
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
        if (_isTripleShot == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        _lastShotTime = Time.time;

        _laserSoundSource.PlayOneShot(_laserSoundClip);
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalinput, 0);

        if (_isBonusSpeed == true)
        {
            transform.Translate(direction * (_speed + _bonusSpeed) * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

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
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisuals.SetActive(false);
            return;
        }
        _lives--;

        if (_lives == 2)
        {
            StartCoroutine(StartEngineFire(1));
        }
        if (_lives == 1)
        {
            StartCoroutine(StartEngineFire(0));
        }

        // if lives is less than 3 enable engine damage
        _uiManager.UpdateLives(_lives);

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShot = true;
        StartCoroutine(PowerupRoutine("Triple_Shot"));

    }
    public void ActivateSpeedBoost()
    {
        _isBonusSpeed = true;
        StartCoroutine(PowerupRoutine("Speed_Boost"));
    }

    public void ActivateShields()
    {
        _shieldVisuals.SetActive(true);
        _isShieldActive = true;
    }
    IEnumerator PowerupRoutine(string powerup)
    {
        yield return new WaitForSeconds(_powerupDuration);
        switch (powerup)
        {
            case "Triple_Shot":
                _isTripleShot = false;
                break;

            case "Speed_Boost":
                _isBonusSpeed = false;
                break;
            default:
                _isTripleShot = false;
                _isBonusSpeed = false;
                break;
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    IEnumerator StartEngineFire(int fireId)
    {
        yield return new WaitForSeconds(0.6f);
        _engineFires[fireId].SetActive(true);
    }
}
