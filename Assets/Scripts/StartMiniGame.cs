using DTT.MinigameMemory;
using Naninovel;
using UnityEngine;

[CommandAlias("startMiniGame")]
public class StartMiniGame : Command
{
    [ParameterAlias("settingsPath"), RequiredParameter]
    public StringParameter SettingsPath;

    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var miniGameService = Engine.GetService<MiniGameService>();

        // ��������� ���������. ����� ������ ������� ��������� �� nani
        var settings = Resources.Load<MemoryGameSettings>(SettingsPath);

        if (settings == null)
        {
            Debug.LogError($"�� ������� ��������� ��������� ����-���� �� ����: {SettingsPath}");
            return;
        }

        await miniGameService.StartGameAsync(settings);
    }
}
