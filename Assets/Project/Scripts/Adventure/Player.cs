using Project.Scripts.Adventure.InteractionSystem;
using UnityEngine;

namespace Project.Scripts.Adventure
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float rotationSpeed = 8f;

        [SerializeField] private InputReader inputReader;

        private Animator _animator;
        private CharacterController _characterController;

        private Vector3 _currentMovement;
        private int _interactHash;
        private Interactor _interactor;
        private bool _isTryingToMove;

        private int _isWalkingHash;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _interactor = GetComponent<Interactor>();
        }

        private void Start()
        {
            _isWalkingHash = Animator.StringToHash("IsWalking");
            _interactHash = Animator.StringToHash("Interact");
        }

        private void Update()
        {
            HandleGravity();
            HandleRotation();
            HandleAnimation();

            _characterController.Move(_currentMovement * Time.deltaTime);
        }

        private void OnEnable()
        {
            inputReader.MoveEvent += OnMove;
            inputReader.InteractEvent += OnInteract;
        }

        private void OnDisable()
        {
            inputReader.MoveEvent -= OnMove;
            inputReader.InteractEvent -= OnInteract;
        }

        private void OnMove(Vector2 input)
        {
            _currentMovement.x = input.x * moveSpeed;
            _currentMovement.z = input.y * moveSpeed;
            _isTryingToMove = input != Vector2.zero;
        }

        private void OnInteract()
        {
            if (!_isTryingToMove && _interactor.focused != null)
            {
                _animator.SetTrigger(_interactHash);
                _interactor.focused.Interact();
            }
        }

        private void HandleAnimation()
        {
            var isWalking = _animator.GetBool(_isWalkingHash);

            if (_isTryingToMove && !isWalking) _animator.SetBool(_isWalkingHash, true);
            else if (!_isTryingToMove && isWalking) _animator.SetBool(_isWalkingHash, false);
        }

        private void HandleRotation()
        {
            var positionToLookAt = Vector3.zero;
            positionToLookAt.x = _currentMovement.x;
            positionToLookAt.z = _currentMovement.z;

            var currentRotation = transform.rotation;

            if (_isTryingToMove)
            {
                var targetRotation = Quaternion.LookRotation(positionToLookAt);
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void HandleGravity()
        {
            if (_characterController.isGrounded)
                _currentMovement.y = -0.5f;
            else
                _currentMovement.y += -9.8f;
        }
    }
}