using UnityEngine;

internal interface ITargetMarker
{
    void GoTo(ILocation targetLocation);
}

public class TargetMarker : MonoBehaviour, ITargetMarker
{
    public void GoTo(ILocation targetLocation)
    {
        transform.position = targetLocation.Position;
    }
}
