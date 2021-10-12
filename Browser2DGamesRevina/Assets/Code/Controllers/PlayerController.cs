using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private float _speed = 3.0f;
        private float _animationSpeed = 10.0f;
        private float _speedJump = 9.0f;
        private float _movingTreshold = 0.1f;
        private float _jumpTreshold = 1.0f;

        private float _gravity = -9.8f;
        private float _yVelosity;
        private float _groundLevel = 0;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1,1,1);

        private float _xAxisInput;
        private bool _isJump;
        private bool _isMoving;

        private LevelObjectView _view;
        private SpriteAnimatorController _spriteAnimator;

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator)
        {
            _view = player;
            _spriteAnimator = animator;
            _spriteAnimator.StartAnimation(_view._spriteRenderer, AnimState.Idle, true, _animationSpeed);
        }

        private void MoveTowards()
        {
            _view._transform.position += Vector3.right * (Time.deltaTime * _speed * (_xAxisInput < 0 ? -1 : 1));
            _view._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        private bool IsGrounded()
        {
            return _view._transform.position.y <= _groundLevel + float.Epsilon && _yVelosity <= 0;
        }

        public void Update()
        {
            _spriteAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");

            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;
            _isJump = Input.GetAxis("Vertical") > 0;

            if(_isMoving)
            {
                MoveTowards();
            }

            if(IsGrounded())
            {
                _spriteAnimator.StartAnimation(_view._spriteRenderer, 
                    _isMoving? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if(_isJump && _yVelosity <= 0)
                {
                    _yVelosity = _speedJump;
                }
                else if(_yVelosity < 0 )
                {
                    _yVelosity = float.Epsilon;
                    _view._transform.position = _view._transform.position.Change(y: _groundLevel);
                }
            }
            else
            {
                _yVelosity += _gravity * Time.deltaTime;

                if(Mathf.Abs(_yVelosity) > _jumpTreshold)
                {
                    _spriteAnimator.StartAnimation(_view._spriteRenderer,
                     AnimState.Jump, true, _animationSpeed);
                }
                _view._transform.position += Vector3.up * (_yVelosity * Time.deltaTime);
            }
        }
    }
}