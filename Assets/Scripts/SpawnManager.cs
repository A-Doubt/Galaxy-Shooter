using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _isPlayerAlive = true;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game object every 5 seconds
    // create a corouting of type IEnumerator - yield events
    // 
    IEnumerator SpawnRoutine()
    {
        while (_isPlayerAlive == true)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(0, 10), Quaternion.identity); // make sure it's off the screen.
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3);
        }
    }

    public void OnPlayerDeath()
    {
        _isPlayerAlive = false;
    }
}
