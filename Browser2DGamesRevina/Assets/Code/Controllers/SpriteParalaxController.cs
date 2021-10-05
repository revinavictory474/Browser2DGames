using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace PlatformerMVC
{
    public class SpriteParalaxController
    {
        private Transform _background;
        private Transform _mainBackground;
        private Transform _backgroundTwo;
        private Transform _mainBackgroundTwo;

        private float _leftBorder = -20.0f;
        private float _rightBorder = 20.0f;
        private float _relativeSpeedRate = 0.1f;
        
        private const float _COEF = 0.3f;

        public SpriteParalaxController(Transform back, Transform mainBackground, 
            Transform backTwo, Transform mainBackgroundTwo)
        {
            _background = back;
            _mainBackground = mainBackground;
            _backgroundTwo = backTwo;
            _mainBackgroundTwo = mainBackgroundTwo;
        }

        public void Update()
        {
            Move(_background, 1);
            Move(_mainBackground, 0.3f);
            Move(_backgroundTwo, 1);
            Move(_mainBackgroundTwo, 0.3f);
            
        }

        public void Move(Transform bg, float speed)
        {
            bg.position += Vector3.left * speed * _relativeSpeedRate;
            
            Vector3 position = bg.position;
            
            if (position.x <= _leftBorder)
            {
                bg.position = new Vector3(_rightBorder - (_leftBorder - position.x), position.y, position.z);
            }
                
            else if (bg.position.x >= _rightBorder)
            {
                bg.position = new Vector3(_leftBorder + (_rightBorder - position.x), position.y, position.z);
            }
        }
    }
}