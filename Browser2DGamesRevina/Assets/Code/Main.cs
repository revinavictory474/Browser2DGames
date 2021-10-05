using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{


    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private int _animationSpeed = 10;

        private SpriteAnimatorController _playerAnimator;
        
        private SpriteParalaxController _paralaxController;
        [SerializeField] private Transform _backgroundTransform;
        [SerializeField] private Transform _mainBackgroundTransform;
        [SerializeField] private Transform _backgroundTransformTwo;
        [SerializeField] private Transform _mainBackgroundTransformTwo;
        
        
        void Start()
        {
            _paralaxController = new SpriteParalaxController(_backgroundTransform, 
                _mainBackgroundTransform, _backgroundTransformTwo, _mainBackgroundTransformTwo);
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, _animationSpeed);
            }
            
        }

        
        void Update()
        {
            _playerAnimator.Update();
            _paralaxController.Update();
        }
    }
}