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
    private GameObject _powerupPrefab;

    [SerializeField]
    private GameObject _speedpowerupPrefab;

    private bool _stopSpawning = false;

    void Start()
    {
        //Allows us to perform timer-controlled operations.
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnPowerUpSpeedRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postoSpwan = new Vector3(Random.Range(-7f, 7f),7 ,0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postoSpwan, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        } 
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postoSpwan = new Vector3(Random.Range(-7f, 7f), 7, 0);
            GameObject newPowerUP = Instantiate(_powerupPrefab, postoSpwan, Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
        }
    }

    IEnumerator SpawnPowerUpSpeedRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postoSpwan = new Vector3(Random.Range(-7f, 7f), 7, 0);
            GameObject newPowerUP = Instantiate(_speedpowerupPrefab, postoSpwan, Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
        }
    }


    public void OnPlayerDEath()
    {
        _stopSpawning = true;
    }
}
