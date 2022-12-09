using DevourDev.Unity.Utils;
using UnityEngine;

namespace DevourDev.Unity.Utils.Shake
{
    public sealed class Shaker : MonoBehaviour
    {
        [SerializeField, InspectorName("Target Transform")] private Transform _tr;
        [SerializeField] private ShakeCurve2D _shakeCurve;
        [SerializeField] private Vector2Int _returningRate = new(2, 5);


        private readonly System.Random _rnd = new();

        private Vector3 _initOffset;
        private int _shakesBeforeReturn;
        private Vector3 _velocity;
        private float _timeBeforeRecalculate;




        public float CurveTime { get; set; }
        

        public void SetCurve(ShakeCurve2D curve)
        {
            _shakeCurve = curve;
        }


        public void ResetPosition()
        {
            transform.localPosition = _initOffset;
        }


        private void Awake()
        {
            _initOffset = _tr.localPosition;
        }

        private void Update()
        {
            CurveTime = (float)System.Math.Abs(System.Math.Sin(Time.timeAsDouble));

            float deltaTime = Time.deltaTime;
            _tr.localPosition += _velocity * deltaTime;

            if ((_timeBeforeRecalculate -= deltaTime) > 0)
                return;

            Recalculate();
        }

        private void Recalculate()
        {
            _shakeCurve.Evaluate(CurveTime, out var ampl, out var freq);
            _timeBeforeRecalculate = 1f / freq;
            Vector2 pointOnCircle;

            if (--_shakesBeforeReturn > 0)
            {
                pointOnCircle = RandomHelpers.PointOnCircle(_rnd);
                pointOnCircle.Scale(ampl);
            }
            else
            {
                _shakesBeforeReturn = UnityEngine.Random.Range(_returningRate.x, _returningRate.y);
                pointOnCircle = _initOffset - transform.localPosition;
            }

            _velocity = pointOnCircle * freq;
        }
    }

}