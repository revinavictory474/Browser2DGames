using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private float _speed = 150.0f;
        private float _animationSpeed = 10.0f;
        private float _speedJump = 9.0f;
        private float _movingTreshold = 0.1f;
        private float _jumpTreshold = 1.0f;

        private float _yVelocity;
        private float _xVelocity;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1,1,1);

        private float _xAxisInput;
        private bool _isJump;
        private bool _isMoving;

        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;
        private readonly ContactPooler _contactPooler;

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Idle, true, _animationSpeed);
            _contactPooler = new ContactPooler(_view._collider);
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _speed * (_xAxisInput < 0 ? -1 : 1);
            _view._rigidbody2D.velocity = _view._rigidbody2D.velocity.Change(x: _xVelocity);
            _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public void Update()
        {
            _spriteAnimator.Update();
            _contactPooler.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;
            _isJump = Input.GetAxis("Vertical") > 0;

            if(_isMoving)
            {
                MoveTowards();
            }

            if(_contactPooler.IsGrounded)
            {
                _spriteAnimator.StartAnimation(_view._spriteRenderer, 
                    _isMoving? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if(_isJump && Mathf.Abs(_view._rigidbody2D.velocity.y) <= _jumpTreshold)
                {
                    _view._rigidbody2D.AddForce(Vector2.up * _speedJump, ForceMode2D.Impulse);
                }
            }
            else
            {
                if(Mathf.Abs(_view._rigidbody2D.velocity.y) > _jumpTreshold)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer,
                     AnimState.Jump, true, _animationSpeed);
                }
            }
        }
    }
}