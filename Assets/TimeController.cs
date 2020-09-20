using UnityEngine;

internal interface ITimeController
{
    float UnscaledDeltaTime { get; }
    void Pause();
    void Resume();
}

internal class TimeController : ITimeController
{
    public float UnscaledDeltaTime => Time.unscaledDeltaTime;

    public void Pause() => Time.timeScale = 0f;
    public void Resume() => Time.timeScale = 1f;
}
