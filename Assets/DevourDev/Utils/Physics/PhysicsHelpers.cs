using System;
using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public static class PhysicsHelpers
    {
        private const int _bufferSize = 4096;

        private static readonly Collider[] _buffer = new Collider[_bufferSize];
        private static readonly ReadOnlyMemory<Collider> _memory = new(_buffer);


        public static ReadOnlyMemory<Collider> OverlapSphere(Vector3 center, float radius)
        {
            var count = Physics.OverlapSphereNonAlloc(center, radius, _buffer);
            return _memory[..count];
        }
    }
}