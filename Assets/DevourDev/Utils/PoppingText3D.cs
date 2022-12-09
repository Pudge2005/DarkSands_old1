using TMPro;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public class PoppingText3D : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _textMesh;
        [SerializeField] private float _lifetimeInitial = 2f;
        [SerializeField] private float _speedInitial = 1.5f;
        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private AnimationCurve _transparencyCurve;
        [SerializeField] private AnimationCurve _scaleCurve;
        [SerializeField] private Vector3 _direction = Vector3.up;

        private float _lifetimeTotal;
        private float _lifetimeLeft;

        private float _speed;
        private Vector3 _initialScale;

        public void Init(float lifetimeMultiplier = 1f, float speedMultiplier = 1f)
        {
            _lifetimeLeft = _lifetimeTotal = _lifetimeInitial * lifetimeMultiplier;
            _speed = _speedInitial * speedMultiplier;
            _initialScale = transform.localScale;
            Evaluate(0);
        }


        public void SetText(string text) => _textMesh.text = text;
        public void SetColor(Color color) => _textMesh.color = color;
        public void SetFontSize(float size) => _textMesh.fontSize = size;

        public void AllowFontAutoSizing(bool allow) => _textMesh.enableAutoSizing = allow;


        private void Awake()
        {
            _lifetimeLeft = _lifetimeInitial;
        }


        private void Update()
        {
            _lifetimeLeft -= Time.deltaTime;

            if(_lifetimeLeft > 0)
            {
                float t = Mathf.InverseLerp(_lifetimeTotal, 0, _lifetimeLeft);
                Evaluate(t);
                return;
            }

            //return to pool

            Destroy(gameObject);
        }

        private void Evaluate(float t)
        {
            float speed = _speed * _speedCurve.Evaluate(t);
            transform.position += _direction * speed * Time.deltaTime;
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = _initialScale * scale;
            Color color = _textMesh.color;
            color.a = _transparencyCurve.Evaluate(t);

            _textMesh.color = color;
        }
    }
}
