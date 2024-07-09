using UnityEngine;

public interface IItems
{
    string Name { get; }
    Sprite Sprite { get; }
    int Cost { get; }
}
