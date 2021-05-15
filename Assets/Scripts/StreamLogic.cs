using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamLogic : MonoBehaviour
{
    public GameObject stream;
    public GameObject streamBlaster;

    private Transform _firePoint;
    private Transform _beamPoint;
    private LineRenderer _streamLine;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private RaycastHit2D _hitInfo;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _streamLine = stream.GetComponentInChildren<LineRenderer>();
        _beamPoint = stream.transform.Find("BeamPoint");
        _firePoint = transform.Find("FirePoint");
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DisableStream();
    }

    // Update is called once per frame
    void Update()
    {
        if (streamBlaster && streamBlaster.activeSelf)
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
    }

    void LateUpdate()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            DisableStream();
        }
        if (_hitInfo)
        {
            if (_hitInfo.collider.CompareTag("EnemyA"))
            {
                _beamPoint.position = _firePoint.transform.position;
                _streamLine.SetPosition(0, _firePoint.transform.position);
                _streamLine.SetPosition(1, _hitInfo.collider.transform.position);
                _playerMovement.enemyInStream = true;
            }
        }
    }

    void EnableStream()
    {
        stream.SetActive(true);
    }

    void UpdateStream()
    {
        if (_firePoint != null && !_hitInfo.collider || _hitInfo.collider && _hitInfo.collider.CompareTag("Player"))
        {
            _beamPoint.position = _firePoint.transform.position;
            _streamLine.SetPosition(0, _firePoint.transform.position);
            float playerX = _animator.GetFloat("moveX");
            float playerY = _animator.GetFloat("moveY");            

            if (playerX == -1 && playerY == 0)    
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x - 1, _firePoint.transform.position.y, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x - 1, _firePoint.transform.position.y));
            }
            else if (playerX == -1 && playerY == -1)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x - 1, _firePoint.transform.position.y - .5f, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x - 1, _firePoint.transform.position.y - .5f));
            }
            else if (playerX == -1 && playerY == 1)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x - 1, _firePoint.transform.position.y + .5f, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x - 1, _firePoint.transform.position.y + .5f));
            }
            else if(playerX == 1 && playerY == 0)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x + 1, _firePoint.transform.position.y, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x + 1, _firePoint.transform.position.y));
            }
            else if (playerX == 1 && playerY == 1)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x + 1, _firePoint.transform.position.y + .5f, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x + 1, _firePoint.transform.position.y + .5f));
            }
            else if (playerX == 1 && playerY == -1)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x + 1, _firePoint.transform.position.y - .5f, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x + 1, _firePoint.transform.position.y - .5f));
            }
            else if (playerX == 0 && playerY == -1)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x, _firePoint.transform.position.y - 1, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x, _firePoint.transform.position.y - 1));
            } else if (playerX == 0 && playerY == 1)
            {
                _streamLine.SetPosition(1, new Vector3(_firePoint.transform.position.x, _firePoint.transform.position.y + 1, _streamLine.GetPosition(1).z));
                _hitInfo = Physics2D.Raycast(_firePoint.position, new Vector2(_firePoint.transform.position.x, _firePoint.transform.position.y + 1));
            }

        }
    }

    void DisableStream()
    {
        stream.SetActive(false);
        _playerMovement.enemyInStream = false;
        _hitInfo = new RaycastHit2D();
    }
}
