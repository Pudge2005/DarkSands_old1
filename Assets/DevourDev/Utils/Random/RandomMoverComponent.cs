using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class RandomMoverComponent : MonoBehaviour
    {
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;

        [SerializeField] private Vector2 _speedRange = new(1f, 2f);
        [SerializeField] private Vector2 _stayingOnOnePosTimeRange = new(2f, 4f);

        private Vector3 _distanceLeft;
        private Vector3 _velocity;

        private float _regenerateDestPointCD;


        public Transform Min { get => _min; set => _min = value; }
        public Transform Max { get => _max; set => _max = value; }
        public Vector2 SpeedRange { get => _speedRange; set => _speedRange = value; }
        public Vector2 StayingOnOnePosTimeRange { get => _stayingOnOnePosTimeRange; set => _stayingOnOnePosTimeRange = value; }


        public void RegenerateDestPoint()
        {
            Vector3 dest;
            dest.x = UnityEngine.Random.Range(_min.position.x, _max.position.x);
            dest.y = UnityEngine.Random.Range(_min.position.y, _max.position.y);
            dest.z = _min.position.z;

            _distanceLeft = dest - transform.position;
            _velocity = _distanceLeft.normalized * UnityEngine.Random.Range(_speedRange.x, _speedRange.y);
        }


        private void Start()
        {
            RegenerateDestPoint();
        }


        private void Update()
        {
            CountDown();
            Move();
        }

        private void CountDown()
        {
            if (_regenerateDestPointCD < 0)
                return;

            if ((_regenerateDestPointCD -= Time.deltaTime) > 0)
                return;

            RegenerateDestPoint();
        }

        private void Move()
        {
            Vector3 movement = _velocity * Time.deltaTime;
            float sqrDist = movement.sqrMagnitude;

            if (sqrDist >= _distanceLeft.sqrMagnitude)
            {
                ChangePosition(_distanceLeft);
                ResetCD();
            }
            else
            {
                _distanceLeft -= movement;
                ChangePosition(movement);
            }
        }

        private void ResetCD()
        {
            _regenerateDestPointCD = UnityEngine.Random.Range
                (_stayingOnOnePosTimeRange.x, _stayingOnOnePosTimeRange.y);
        }

        private void ChangePosition(Vector3 delta)
        {
            transform.position += delta;
        }
    }
}
