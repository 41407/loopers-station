using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpacePilotInput : MonoBehaviour
{
    [Inject] private IKeybindings Keys { get; }
    [Inject] private ISteerable Ship { get; }

    private void Update()
    {
        if (PilotSteersUp()) Ship.GoUp();
        if (PilotSteersDown()) Ship.GoDown();
        if (PilotSteersLeft()) Ship.GoLeft();
        if (PilotSteersRight()) Ship.GoRight();
    }

    private bool PilotSteersUp() => PlayerPressesAnyOf(Keys.Up);
    private bool PilotSteersDown() => PlayerPressesAnyOf(Keys.Down);
    private bool PilotSteersLeft() => PlayerPressesAnyOf(Keys.Left);
    private bool PilotSteersRight() => PlayerPressesAnyOf(Keys.Right);

    private static bool PlayerPressesAnyOf(IReadOnlyList<KeyCode> boundKeys) => boundKeys.Any(Input.GetKeyDown);
}
