using DTT.MinigameMemory;
using Naninovel;
using UnityEngine;

[InitializeAtRuntime]
public class MiniGameService : IEngineService
{
    private MemoryGameManager memoryGameManager;

    private UniTaskCompletionSource<MemoryGameResults> gameCompletionSource;

    public async UniTask InitializeServiceAsync()
    {
        memoryGameManager = Object.FindFirstObjectByType<MemoryGameManager>(); //очень сомнительно, но Naninovel не позволяет на прямую инжектить. Если критично то свой DI или инициализировать в другом месте

        if (memoryGameManager == null)
        {
            Debug.LogError("MemoryGameManager не найден на сцене!");
            return;
        }

        memoryGameManager.Finish += OnGameFinished;
        Debug.Log("MiniGameService успешно инициализирован.");
        //await
    }

    public void ResetService()
    {
        Debug.Log("Сброс сервиса мини-игры.");
        gameCompletionSource = null;
        //memoryGameManager.Restart();
    }

    public void DestroyService()
    {
        if (memoryGameManager != null)
            memoryGameManager.Finish -= OnGameFinished;
    }

    public async UniTask StartGameAsync(MemoryGameSettings settings)
    {
        if (memoryGameManager == null)
        {
            Debug.LogError("MemoryGameManager не инициализирован!");
            return;
        }

        gameCompletionSource = new UniTaskCompletionSource<MemoryGameResults>();

        memoryGameManager.StartGame(settings);

        Debug.Log("Мини-игра началась!");

        MemoryGameResults result = await gameCompletionSource.Task;

        Debug.Log($"Мини-игра завершена! Результат: {result}");
    }

    private void OnGameFinished(MemoryGameResults results)
    {
        //Логируем результат
        gameCompletionSource.TrySetResult(results);
    }
}
