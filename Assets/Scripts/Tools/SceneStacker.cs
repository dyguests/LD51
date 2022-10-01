using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Tools
{
    public static class SceneStacker
    {
        private static readonly Stack<string> SceneStack = new();

        public static void InitScene(string scene)
        {
            SceneStack.Push(scene);
        }

        public static void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
            SceneStack.Push(scene);
        }

        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneStack.Peek());
        }

        public static void BackScene()
        {
            if (SceneStack.Count <= 1)
            {
                return;
            }

            SceneStack.Pop();
            SceneManager.LoadScene(SceneStack.Peek());
        }
    }
}