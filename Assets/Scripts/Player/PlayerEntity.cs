using Core.Movement.Controller;
using Core.Movement.Data;
using StatsSystem;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider2D;

        private Animator _anim;
        private Jumper _jumper;

        private HorizontalMover _horizontalMover;


        [SerializeField] private HorizontalMovementData _horizontalMovementData;
        [SerializeField] private JumperData _jumpData;


        [SerializeField] private DirectionalCameraPair _cameras;
        
        public void Initialize(IStatValueGiver statValueGiver)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<BoxCollider2D>();
            _anim = GetComponent<Animator>();
            _horizontalMover = new HorizontalMover(_rigidbody, _horizontalMovementData, statValueGiver);
            _jumper = new Jumper(_rigidbody, _jumpData);
        }

        private void Update()
        {
            if(_jumper.IsJumping)
                _jumper.UpdateJump();
            
            UpdateCameras();
            _anim.SetBool("IsRunning", _horizontalMover.IsMoving);
            _anim.SetBool("IsGrounded", _jumper.IsGrounded());
        }
        

        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionalCamera)
            {
                cameraPair.Value.enabled = cameraPair.Key == _horizontalMover.Direction;
            }
        }

        public void MoveHorizontally(float direction) => _horizontalMover.MoveHorizontally(direction);

        public void Jump()
        {
            _anim.SetTrigger("Jump");
            _jumper.Jump();
        }

        public void Fire()
        {
            _anim.SetBool("IsShooting", true);
        }
        public void StopFire()
        {
            _anim.SetBool("IsShooting", false);
        }
    }
}
