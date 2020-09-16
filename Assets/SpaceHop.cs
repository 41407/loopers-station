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
    public void GoUp() => Hop(Vector2.up);

    public void GoDown() => Hop(Vector2.down);

    public void GoLeft() => Hop(Vector2.left);

    public void GoRight() => Hop(Vector2.right);

    private void Hop(Vector2 direction)
    {
        Move(direction);
        Look(direction);
    }

    private void Move(Vector2 direction) => transform.Translate(direction, Space.World);
    private void Look(Vector2 direction) => transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
}
