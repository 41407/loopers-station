using UnityEngine;

public class Location : MonoBehaviour, ILocation
{
    public Vector3 Position => transform.position;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.attachedRigidbody.GetComponentInChildren<ISpaceWorker>().Enter(this);
    }
}

public interface ILocation
{
    Vector3 Position { get; }
}
