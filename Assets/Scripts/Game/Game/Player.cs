using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    private float _speedPowerup = 2f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
 
    private bool _isTripleLaserActive = false;
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shiledVisualizer;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();


        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null!");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null!");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
            
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
        //Mathf.Clamp allows setting min and max values ​​so we can easily draw the boundaries.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        
        if(_isSpeedPowerupActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * _speedPowerup * Time.deltaTime);
        }

        if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        else if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {              
        _canFire = Time.time * _fireRate;
        if (_isTripleLaserActive == true)
        {
            Instantiate(_tripleLaserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shiledVisualizer.SetActive(false);
            return;
        }
        
        
        //All these writing styles serve the same function.
        //_lives --;
        //_lives = _lives - 1;
        _lives --;

        _uiManager.UpdateLives(_lives);

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDEath(); 
            Destroy(this.gameObject);
        }
    }

    public void TripleLaserActive()
    {
        _isTripleLaserActive = true;
        StartCoroutine(TripleLaserPowerDownRoutine());
    }

    IEnumerator TripleLaserPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _isTripleLaserActive = false;
    }

    public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedPowerUPPowerDownRoutine());
    }

    IEnumerator SpeedPowerUPPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _isSpeedPowerupActive = false;
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        _shiledVisualizer.SetActive(true);
    }

    public void AddScore(int point)
    {
        _score += point;
        _uiManager.UpdateScore(_score);
    }
}



