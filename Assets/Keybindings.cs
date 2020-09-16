using System.Collections.Generic;
using UnityEngine;

internal interface IKeybindings
{
    IEnumerable<KeyCode> Up { get; }
    IEnumerable<KeyCode> Down { get; }
    IEnumerable<KeyCode> Left { get; }
    IEnumerable<KeyCode> Right { get; }
}

internal class DefaultKeybindings : IKeybindings
{
    public IEnumerable<KeyCode> Up => new[] {KeyCode.W, KeyCode.UpArrow};
    public IEnumerable<KeyCode> Down => new[] {KeyCode.S, KeyCode.DownArrow};
    public IEnumerable<KeyCode> Left => new[] {KeyCode.A, KeyCode.LeftArrow};
    public IEnumerable<KeyCode> Right => new[] {KeyCode.D, KeyCode.RightArrow};
}
