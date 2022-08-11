using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _powerupContainer;

    [SerializeField]
    private GameObject[] _powerups;

    private bool _isPlayerAlive = true;
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_isPlayerAlive == true)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(0, 10), Quaternion.identity); // make sure it's off the screen.
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_isPlayerAlive == true)
        {
            int randomPowerup = Random.Range(0, 3); // This will be changed later!
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], new Vector3(0, 10), Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerAlive = false;
    }
}
