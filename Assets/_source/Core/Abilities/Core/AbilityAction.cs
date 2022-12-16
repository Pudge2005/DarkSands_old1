using System;
using System.Threading;
using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AbilityAction<TContext> : ScriptableObject
    {
        public abstract void Act(TContext context);
    }


    public class Chain<TContext>
    {
        
    }



    public static class AsyncHelpers
    {
        private static CancellationTokenSource _cts;


        public static CancellationToken Token => _cts.Token;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            ReInitCts();
            ReSubscribeToEvent();
        }


        private static void ReInitCts()
        {
            if (_cts != null)
            {
                try
                {
                    _cts.Dispose();
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"async helpers error: {ex}");
                }
                finally
                {
                    _cts = null;
                }
            }

            _cts = new();
        }
        
        private static void ReSubscribeToEvent()
        {
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= HandleSceneUnloaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += HandleSceneUnloaded;

            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= HandleSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private static void HandleSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
        {
            CancelActiveTasks();
        }

        private static void CancelActiveTasks()
        {
            _cts.Cancel();
        }

        private static void HandleSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            ReInitCts();
        }
    }
}
