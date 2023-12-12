using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _powerUpDuration;
    [SerializeField]
    private int _health;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Transform _respawnPoint;

    private Camera _mainCamera;
    private Coroutine _powerUpCoroutine;
    private Rigidbody _rigidbody;

    private Vector3 _horizontalDirection;
    private Vector3 _verticalDirection;

    private bool _isPowerUpActive;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        HideAndLockCursor();
        UpdateUI();
    }

    private void Update()
    {
        HandlePlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    public void PickPowerUp()
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }

        _powerUpCoroutine = StartCoroutine(StartPowerUp_Coroutine());
    }

    public void Dead()
    {
        _health -= 1;

        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            SceneManager.LoadScene("LoseScreen");
        }

        UpdateUI();
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandlePlayerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _horizontalDirection = horizontal * _mainCamera.transform.right;
        _verticalDirection = vertical * _mainCamera.transform.forward;
        _horizontalDirection.y = 0f;
        _verticalDirection.y = 0f;
    }

    private void MovePlayer()
    {
        Vector3 movementDirection = _horizontalDirection + _verticalDirection;
        _rigidbody.velocity = _speed * Time.fixedDeltaTime * movementDirection;
    }

    private void UpdateUI()
    {
        _healthText.text = $"Health: {_health}";
    }

    private IEnumerator StartPowerUp_Coroutine()
    {
        _isPowerUpActive = true;
        OnPowerUpStart?.Invoke();

        yield return new WaitForSeconds(_powerUpDuration);

        _isPowerUpActive = false;
        OnPowerUpStop?.Invoke();
    }
}
