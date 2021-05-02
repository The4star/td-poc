using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamLogic : MonoBehaviour
{
    public GameObject streamPrefab;
    public PlayerMovement PlayerMovementScript;
    private Transform _firePoint;
    private GameObject _spawnedStream;
    private Animator _animator;

    private void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
    // Start is called before the first frame update
    void Start()
    {
        _spawnedStream = Instantiate(streamPrefab, _firePoint.position, Quaternion.identity) as GameObject;
        _animator = GetComponent<Animator>();
        DisableStream();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            EnableStream();
        }

        if(Input.GetButton("Fire1"))
        {
            UpdateStream();
        }
    }

    void LateUpdate()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            DisableStream();
        }
    }

    void EnableStream()
    {
        _spawnedStream.SetActive(true);
    }

    void UpdateStream()
    {
        if (_firePoint != null)
        {
            float playerX = _animator.GetFloat("moveX");
            float playerY = _animator.GetFloat("moveY");
            _spawnedStream.transform.position = _firePoint.transform.position;

            if (playerX == -1)    
            {
                _spawnedStream.transform.eulerAngles =  new Vector3(0 , 0, 180);
            } else if(playerX == 1)
            {
                _spawnedStream.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (playerY == -1)
            {
                _spawnedStream.transform.eulerAngles = new Vector3(0, 0, -90);
            } else if (playerY == 1)
            {
                _spawnedStream.transform.eulerAngles = new Vector3(0, 0, 90);
            }

        }
    }

    void DisableStream()
    {
        _spawnedStream.SetActive(false);
    }

}
