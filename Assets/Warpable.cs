using UnityEngine;


internal interface IWarpable
{
    void WarpAround(IArea area);
}

public class Warpable : MonoBehaviour, IWarpable
{
    public void WarpAround(IArea area)
    {
        if (LeavingFromRight(area)) WarpTowards(new Vector2(area.Extents.x * -2, 0));
        else if (LeavingFromLeft(area)) WarpTowards(new Vector2(area.Extents.x * 2, 0));

        if (LeavingFromBottom(area)) WarpTowards(new Vector2(0, area.Extents.y * 2));
        else if (LeavingFromTop(area)) WarpTowards(new Vector2(0, area.Extents.y * -2));
    }

    private bool LeavingFromTop(IArea area) => transform.position.y >= area.Position.y + area.Extents.y;
    private bool LeavingFromBottom(IArea area) => transform.position.y <= area.Position.y - area.Extents.y;
    private bool LeavingFromLeft(IArea area) => transform.position.x <= area.Position.x - area.Extents.x;
    private bool LeavingFromRight(IArea area) => transform.position.x >= area.Position.x + area.Extents.x;

    private void WarpTowards(Vector2 edge)
    {
        transform.Translate(edge, Space.World);
    }
}
