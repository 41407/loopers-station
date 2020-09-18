using System.Collections.Generic;
using UnityEngine;

internal interface IKeybindings
{
    IReadOnlyList<KeyCode> Up { get; }
    IReadOnlyList<KeyCode> Down { get; }
    IReadOnlyList<KeyCode> Left { get; }
    IReadOnlyList<KeyCode> Right { get; }
    IReadOnlyList<KeyCode> AcceptOffer { get; }
}

internal class DefaultKeybindings : IKeybindings
{
    public IReadOnlyList<KeyCode> Up => new[] {KeyCode.W, KeyCode.UpArrow};
    public IReadOnlyList<KeyCode> Down => new[] {KeyCode.S, KeyCode.DownArrow};
    public IReadOnlyList<KeyCode> Left => new[] {KeyCode.A, KeyCode.LeftArrow};
    public IReadOnlyList<KeyCode> Right => new[] {KeyCode.D, KeyCode.RightArrow};
    public IReadOnlyList<KeyCode> AcceptOffer => new[] {KeyCode.X};
}
