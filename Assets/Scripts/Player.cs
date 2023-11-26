using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector3 _horizontalDirection;
    private Vector3 _verticalDirection;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        HideAndLockCursor();
    }

    private void Update()
    {
        HandlePlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
}
