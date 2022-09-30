using UnityEngine;

public class App : MonoBehaviour
{
    private static App sInstance;

    public static App Instance => sInstance;

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
    }
}