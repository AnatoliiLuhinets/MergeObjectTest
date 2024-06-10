using System;
using UnityEngine;

namespace Interfaces
{
    public interface IDraggable
    {
        event Action Dragged;
        void Drag(Vector3 newPosition);
    }
}
