#define ENABLE_ARRAY_POOLING

using System;
using System.Buffers;
using System.Collections.Generic;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class FpsManager : MonoBehaviour
    {
        //todo: rename interface (to argue somehow not extending from IHandler BloodTrail)
        public interface IFpsHandler
        {
            /// <summary>
            /// Telling Handler to do something about
            /// CurrentPerformance != TargetPerformance (rounded~).
            /// In case, curPerf is higher than target - we can
            /// enable some effects/increase textures resolutions;
            /// in other case - we can do wise versa
            /// </summary>
            /// <param name="targetPerformanceOffset">
            /// example: we want our game to go 60 frames
            /// per second (1000 / 60 = 16 ms).
            /// If current AVG FPS (not frametime) is
            /// 30 fps - it is 1/2 of target performance,
            /// so <paramref name="targetPerformanceOffset"/>
            /// is 30f/60f = 0.5f.
            /// </param>
            /// <returns> true if Handler thinks, he have
            /// done enough to figure out performance offset
            /// (so we can stop "TryHandle" chain</returns>
            bool TryHandle(float targetPerformanceOffset);
        }


        private sealed class FpsCounter
        {
            private const int _arrLengthThreshold = 100_000;
            private float _avgFrameTime = -1;

            private int _framesBufferSize;

            private float[] _framesBuffer;
            private int _nextFrameTimeIndex;
#if ENABLE_ARRAY_POOLING
            private ArrayPool<float> _arrPool = ArrayPool<float>.Shared;
#endif


            public FpsCounter(int framesBufferSize = 500)
            {
                _framesBuffer = Array.Empty<float>();
                SetFramesBufferSize(framesBufferSize);
            }


            public float LastAverageFrameTime => _avgFrameTime;


            public event System.Action<FpsCounter, float> OnAverageFrameTimeCalculated;


            public int FramesBufferSize
            {
                get => _framesBufferSize;
                set => SetFramesBufferSize(value);
            }


            public void RegisterFrameTime(float frametime)
            {
                _framesBuffer[_nextFrameTimeIndex] = frametime;

                if (++_nextFrameTimeIndex == _framesBufferSize)
                {
                    _nextFrameTimeIndex = 0;
                    var avgFrameTime = CalcAvg(_framesBuffer, _framesBufferSize);
                    _avgFrameTime = avgFrameTime;
                    OnAverageFrameTimeCalculated?.Invoke(this, _avgFrameTime);
                }
            }


            private static float CalcAvg(float[] arr, int c)
            {
                float sum = 0;

                for (int i = -1; ++i < c; sum += arr[i]) { }

                return sum / c;
            }

            public float CalculateAverageFrameRateImmediate()
            {
                if (_framesBufferSize > 0)
                {
                    if (_nextFrameTimeIndex > 0)
                    {
                        var avgFrameTime = CalcAvg(_framesBuffer, _nextFrameTimeIndex);
                        return 1000f / avgFrameTime;
                    }

                    return _avgFrameTime;
                }

                return -1f;
            }


            private void SetFramesBufferSize(int value)
            {

                if (_framesBufferSize == value)
                    return;

                _framesBufferSize = value;

#if ENABLE_ARRAY_POOLING
                var curLen = _framesBuffer.Length;

                if (curLen == value)
                    return;

                if (curLen > value)
                {
                    if (curLen > _arrLengthThreshold && value < _arrLengthThreshold)
                    {
                        _arrPool.Return(_framesBuffer, false);
                    }
                    else
                    {
                        return;
                    }
                }

                _framesBuffer = _arrPool.Rent(value);
#else

                if (_framesBuffer.Length < value)
                {
                    _framesBuffer = new float[value]; //tmp solution
                }
#endif

                _nextFrameTimeIndex = 0;

            }
        }


        [SerializeField] private int _defaultFramesBufferSize = 500;
        [SerializeField, Min(0f)] private float _performanceOffsetError = 0.2f;

        private FpsCounter _fpsCounter;
        private List<IFpsHandler> _performanceOffsetHandlers;
        private int _targetFrameRate;


        public int TargetFrameRate
        {
            get => _targetFrameRate;
            set => SetTargetFrameRate(value);
        }


        /// <summary>
        /// fps, offset detected
        /// </summary>
        public event System.Action<float, bool> OnDataUpdated;


        private void Awake()
        {
            _fpsCounter = new(_defaultFramesBufferSize);
            _performanceOffsetHandlers = new();
            _fpsCounter.OnAverageFrameTimeCalculated += HandleAvgFrameTimeCalculated;
        }


        private void Update()
        {
            _fpsCounter.RegisterFrameTime(Time.unscaledDeltaTime);
        }


        public void AddFpsHandler(IFpsHandler handler)
        {
            _performanceOffsetHandlers.Add(handler);
        }

        public void RemoveFpsHandler(IFpsHandler handler)
        {
            _ = _performanceOffsetHandlers.Remove(handler);
        }


        private void HandleAvgFrameTimeCalculated(FpsCounter sender, float avgFrameTime)
        {
            float avgFps = 1f / avgFrameTime;

            float perfOffset = 1f - avgFps / _targetFrameRate;
            bool offsetNeedsHandling = Math.Abs(perfOffset) > _performanceOffsetError;

            OnDataUpdated?.Invoke(avgFps, offsetNeedsHandling);

            if (offsetNeedsHandling)
            {
                foreach (var handler in _performanceOffsetHandlers)
                {
                    if (handler.TryHandle(perfOffset))
                        break;
                }
            }
        }

        private void SetTargetFrameRate(int value)
        {
            _targetFrameRate = value;
            Application.targetFrameRate = value;
        }
    }
}