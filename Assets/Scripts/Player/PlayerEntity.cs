using System;
using Core.Enums;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider2D;
        private bool _isJumping;

        private Animator _anim;
        private bool _isRunning;
        
        
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;

        [Header("Jump")] 
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;
        [SerializeField] private LayerMask _jumpableGround;

        [SerializeField] private DirectionalCameraPair _cameras;
        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<BoxCollider2D>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if(_isJumping)
                UpdateJump();

            _anim.SetBool("IsRunning", _isRunning);
            _anim.SetBool("IsGrounded", IsGrounded());
            Debug.Log(IsGrounded());
        }

        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
            _isRunning = direction != 0;
            
            
        }
        public void Jump()
        {
            if(!IsGrounded())
                return;
            _anim.SetTrigger("Jump");
            _rigidbody.velocity = new Vector2(0, _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
        }
        public void Fire()
        {
            _anim.SetBool("IsShooting", true);
        }
        public void StopFire()
        {
            _anim.SetBool("IsShooting", false);
        }

        private void SetDirection(float direction)
        {
            if ((_direction == Direction.Right && direction < 0) || (_direction == Direction.Left && direction > 0))
            {
                Flip();
            }
            
        }

        private void Flip()
        {
            transform.Rotate(0,180,0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            foreach (var cameraPair in _cameras.DirectionalCamera)
                cameraPair.Value.enabled = cameraPair.Key == _direction;
        }

        private void UpdateJump()
        {
            if (_rigidbody.velocity.y <0 )
            {
                ResetJump();
            }
        }
        
        
        private void ResetJump()
        {
            _isJumping = false;
        }
        private bool IsGrounded()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1.5f);
 
            
            return hits.Length > 1;
        }
    }
    

}
