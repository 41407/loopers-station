using UnityEngine;

public class Warpable : MonoBehaviour, IWarpable
{
    public void WarpAround(IArea area)
    {
        Debug.Log("menäss");
        if (transform.position.x >= area.Position.x + area.Extents.x)
        {
            Debug.Log("OIkeelta ules :)");
            transform.Translate(new Vector2(area.Extents.x * -2, 0), Space.World);
        }
        else if (transform.position.x <= area.Position.x - area.Extents.x)
        {
            Debug.Log("vas ules :)");
            transform.Translate(new Vector2(area.Extents.x * 2, 0), Space.World);
        }

        if (transform.position.y <= area.Position.y - area.Extents.y)
        {
            Debug.Log("alhaalt ules :)");
            transform.Translate(new Vector2(0, area.Extents.y * 2), Space.World);
        }
        else if (transform.position.y >= area.Position.y + area.Extents.y)
        {
            Debug.Log("ylhäält ules :)");
            transform.Translate(new Vector2(0, area.Extents.y * -2), Space.World);
        }
    }
}

internal interface IWarpable
{
    void WarpAround(IArea area);
}
