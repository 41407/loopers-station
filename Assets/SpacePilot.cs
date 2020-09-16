using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpacePilot : MonoBehaviour
{
    [Inject] private IKeybindings Keybindings { get; }
    [Inject] private ISteerable Ship { get; }

    private void Update()
    {
        if (PilotSteersUp()) Ship.GoUp();
        if (PilotSteersDown()) Ship.GoDown();
        if (PilotSteersLeft()) Ship.GoLeft();
        if (PilotSteersRight()) Ship.GoRight();
    }

    private bool PilotSteersUp() => PlayerPresses(Keybindings.Up);
    private bool PilotSteersDown() => PlayerPresses(Keybindings.Down);
    private bool PilotSteersLeft() => PlayerPresses(Keybindings.Left);
    private bool PilotSteersRight() => PlayerPresses(Keybindings.Right);

    private static bool PlayerPresses(IEnumerable<KeyCode> boundKeys) => boundKeys.Any(Input.GetKeyDown);
}
