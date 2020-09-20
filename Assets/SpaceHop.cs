using UnityEngine;

public interface ISteerable
{
    void GoUp();
    void GoDown();
    void GoLeft();
    void GoRight();
}

public class SpaceHop : MonoBehaviour, ISteerable
{
    private Vector2 Velocity { get; set; }

    public void GoUp() => Hop(Vector2.up);

    public void GoDown() => Hop(Vector2.down);

    public void GoLeft() => Hop(Vector2.left);

    public void GoRight() => Hop(Vector2.right);

    private void Hop(Vector2 direction)
    {
        Move(direction);
        Look(direction);
    }

    private void Move(Vector2 direction) => Velocity += direction;
    private void Look(Vector2 direction) => transform.rotation = Quaternion.LookRotation(direction, Vector3.back);

    private void Update()
    {
        ApplyVelocity();
        DampenVelocity();
    }

    private void DampenVelocity() => Velocity *= 0.5f;

    private void ApplyVelocity() => transform.Translate(Velocity, Space.World);
}
