using UnityEngine;

namespace SaveSystem.GameStart
{
    public class GameStart : MonoBehaviour
    {
        private void OnEnable() => SaveAndLoadService.Current.InvokeGameStarted(true);
        private void OnDestroy() => SaveAndLoadService.Current?.InvokeGameStarted(false);
        private void OnDisable() => SaveAndLoadService.Current?.InvokeGameStarted(false);
        private void OnApplicationQuit() => SaveAndLoadService.Current?.InvokeGameStarted(false);
    }
}