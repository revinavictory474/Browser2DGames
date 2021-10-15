using UnityEngine;

namespace PlatformerMVC
{
    public class SpriteParalaxController
    {
        private Transform[] _backgrounds;
        private Transform _cameraTransform;

        private Vector3 _previousCamPos;
        
        private float[] _paralaxScales;
        private float _smoothing = 1.0f;
        private float _spriteWidth = 0f;


        public SpriteParalaxController(Transform camera, Transform[] backgrounds)
        {
            _backgrounds = backgrounds;
            _cameraTransform = camera;
            _previousCamPos = camera.position;

            _paralaxScales = new float[_backgrounds.Length];

            for(int i = 0; i < _backgrounds.Length; i++)
            {
                _paralaxScales[i] = _backgrounds[i].position.z * -1;
                Sprite sprite = _backgrounds[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                Texture2D texture = sprite.texture;
                _spriteWidth = texture.width / sprite.pixelsPerUnit; 
            }

        }

        public void Update()
        {
            Parallaxing();
        }

        public void LateUpdate()
        {
            ScrollingBackground();
        }

        public void Parallaxing()
        {
            for(int i = 0; i<_backgrounds.Length; i++)
            {
                float paralax = (_previousCamPos.x - _cameraTransform.position.x) * _paralaxScales[i];
                float backgroundTargetPositionX = _backgrounds[i].position.x + paralax;
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPositionX, _backgrounds[i].position.y, _backgrounds[i].position.z);
                _backgrounds[i].position = Vector3.Lerp(_backgrounds[i].position, backgroundTargetPos, _smoothing * Time.deltaTime);
            }

            _previousCamPos = _cameraTransform.position;
        }

        public void ScrollingBackground()
        {
            for (int i = 0; i < _backgrounds.Length; i++)
            {
                if (Mathf.Abs(_cameraTransform.position.x - _backgrounds[i].transform.position.x) >= _spriteWidth)
                {
                    float offsetPositionX = (_cameraTransform.position.x - _backgrounds[i].transform.position.x) % _spriteWidth;
                    _backgrounds[i].transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, _backgrounds[i].transform.position.y);
                }

            }
        }
    }
}