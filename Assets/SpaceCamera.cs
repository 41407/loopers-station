using UnityEngine;
using Zenject;

public class SpaceCamera : MonoBehaviour, ISpaceCamera
{
    [Inject] private ITimeController Time { get; }

    [SerializeField] private int transitionStepCount = 15;
    private int TransitionStepCount => transitionStepCount;
    private float TransitionStepDistance { get; set; }

    private IArea CurrentArea { get; set; }
    private Vector2 TargetPosition { get; set; }

    public void MoveTo(IArea area)
    {
        if (CurrentArea != area)
        {
            CurrentArea = area;
            TargetPosition = area.Position;
            var distance = Vector2.Distance(transform.position, TargetPosition);
            TransitionStepDistance = Mathf.Max(0.1f, distance / TransitionStepCount);
        }
    }

    private void LateUpdate()
    {
        if (Vector2.Distance(transform.position, TargetPosition) >= TransitionStepDistance)
        {
            Time.Pause();
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, TransitionStepDistance);
            Debug.Log(transform.position + " AND: " + TargetPosition);
        }
        else
        {
            Time.Resume();
        }
    }
}

internal interface ISpaceCamera
{
    void MoveTo(IArea area);
}
