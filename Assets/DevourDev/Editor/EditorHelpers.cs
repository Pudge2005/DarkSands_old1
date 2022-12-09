using System;
using System.Collections.Generic;
using System.Reflection;

namespace DevourDev.Unity.Editor.Utils
{
    public static class EditorHelpers
    {
#if UNITY_EDITOR
        private static readonly Type[] _typesBuffer = new Type[1024 * 8];
        private static readonly ReadOnlyMemory<Type> _typesMemBuffer = new(_typesBuffer);


        public static ReadOnlyMemory<Type> GetInheritedTypes(Type type)
        {
            return GetInheritedTypes(type, true);
        }

        public static ReadOnlyMemory<Type> GetInheritedTypes(Type type, bool includeBase)
        {
            return GetInheritedTypes(type, includeBase, AppDomain.CurrentDomain.GetAssemblies());
        }

        public static ReadOnlyMemory<Type> GetInheritedTypes(Type type, bool includeBase, Assembly[] assemblies)
        {
            int typesCount = 0;

            if (includeBase)
                _typesBuffer[typesCount++] = type;

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsSubclassOf(type))
                        _typesBuffer[typesCount++] = t;
                }
            }

            return _typesMemBuffer[..typesCount];
        }


        public static int FindAssetsOfType<T>(ICollection<T> collection) where T : UnityEngine.Object
        {
            int count = 0;

            string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T)}");

            foreach (var guid in guids)
            {
                collection.Add(GuidToAsset<T>(guid));
                ++count;
            }

            return count;
        }

        public static TAsset GuidToAsset<TAsset>(string guid) where TAsset : UnityEngine.Object
        {
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            return (TAsset)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(TAsset));
        }


        public static int FindAssetsOfTypeIncludingSubclasses<T>(ICollection<T> collection) where T : UnityEngine.Object
        {
            int count = 0;

            var types = GetInheritedTypes(typeof(T), true).Span;

            foreach (var t in types)
            {
                var findAssetsGenericMethod = typeof(EditorHelpers).GetMethod(nameof(FindAssetsOfType),
                    BindingFlags.Static | BindingFlags.Public)
                    .MakeGenericMethod(new Type[] { t });

                count += (int)findAssetsGenericMethod.Invoke(null, new object[] { collection });
            }

            return count;
        }
#endif
    }
}
