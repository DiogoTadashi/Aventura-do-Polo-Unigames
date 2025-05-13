using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Mantém o objeto ao trocar de cena
    }

    void Start()
    {
        Resolution resolution = Screen.currentResolution;
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
        Debug.Log($"Resolução ajustada para: {resolution.width}x{resolution.height}");
    }
}