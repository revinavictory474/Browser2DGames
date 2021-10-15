using UnityEngine;

namespace PlatformerMVC
{
    public class BulletController 
    {
        private Vector3 _velocity;
        private LevelObjectView _view;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            ActiveTrail(false);
            Active(false);
        }

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
        }

        public void ActiveTrail(bool val)
        {
            for (int i = 0; i < _view._trailRenderer.Length; i++)
            {
                _view._trailRenderer[i].emitting = val;

                if (val == false)
                {
                    _view._trailRenderer[i].Clear();
                }
            }
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            float _angle = Vector3.Angle(Vector3.down, _velocity);
            Vector3 _axis = Vector3.Cross(Vector3.down, _velocity);
            _view.transform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        public void Trow(Vector3 position, Vector3 velocity)
        {
            Active(true);
            ActiveTrail(true);
            SetVelocity(velocity);

            _view.transform.position = position;
            _view._rigidbody2D.velocity = Vector2.zero;
            _view._rigidbody2D.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}