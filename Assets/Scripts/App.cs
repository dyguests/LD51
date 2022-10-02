using Cores.Scenes.Games.Entities;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    private static App sInstance;

    public static App Instance => sInstance;

    public Map map;

    void Awake()
    {
        if (sInstance == null)
        {
            sInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneStacker.InitScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackClick(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            Debug.Log("OnBackClick");
            SceneStacker.BackScene();
        }
    }
}