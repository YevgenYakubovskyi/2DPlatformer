using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumperData _jumperData;
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _transform;

        public bool IsJumping { get; private set; }

        public Jumper(Rigidbody2D rigidbody, JumperData jumperData)
        {
            _rigidbody = rigidbody;
            _jumperData = jumperData;
            _transform = rigidbody.transform;
        }

        public void Jump()
        {
            if (!IsGrounded())
                return;
            
            
            
            IsJumping = true;
            _rigidbody.velocity = new Vector2(0, _jumperData.JumpForce);
            _rigidbody.gravityScale = _jumperData.GravityScale;
        }

        public void UpdateJump()
        {
            if (_rigidbody.velocity.y <0 )
            {
                ResetJump();
            }
        }

        public bool IsGrounded()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(_transform.position, Vector2.down, 1.5f);
            return hits.Length > 1;
        }

        private void ResetJump()
        {
            IsJumping = false;
        }
    }
}