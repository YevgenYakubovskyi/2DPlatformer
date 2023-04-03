using System;
using Core.Enums;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        
        private Rigidbody2D _rigitbody;
        private BoxCollider2D _collider2D;
        private bool _isJumping;
        
        
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
            _rigitbody = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if(_isJumping)
                UpdateJump();
        }

        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigitbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigitbody.velocity = velocity;
        }
        public void MoveVertically(float direction)
        {
            if(_isJumping)
                return;
        }

        public void Jump()
        {
            if(!IsGrounded())
                return;
            _rigitbody.velocity = new Vector2(0, _jumpForce);
            // _rigitbody.AddForce(Vector2.up * _jumpForce);
            _rigitbody.gravityScale = _gravityScale;
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
            {
                
            }
        }

        private void UpdateJump()
        {
            if (_rigitbody.velocity.y <0 )
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
            return Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
        }
    }
    

}
