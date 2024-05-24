using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 30;
    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded;

    public Animator _animator;

    public void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool _isSprinting = Input.GetKey(KeyCode.LeftShift);

        float speed = _isSprinting ? runSpeed : walkSpeed;

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 moveDirection = transform.TransformDirection(inputDirection);

        _isGrounded = _controller.isGrounded;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpForce * 2 * gravity);
        }

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }

        _velocity.x = moveDirection.x * speed;
        _velocity.z = moveDirection.z * speed;
        _velocity.y -= gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);

        //Animations
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!_isSprinting)
            {
                _animator.SetBool("Run", false);
                _animator.SetBool("Walk", true);
            }
            else
            {
                _animator.SetBool("Run", true);
            }
        }
        else
        {
            _animator.SetBool("Run", false);
            _animator.SetBool("Walk", false);
        }
    }
}
