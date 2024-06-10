using System;
using Environment;
using UnityEngine;

namespace Core.Input
{
    public class InputManager : MonoBehaviour
    {
        public event Action<MovableObject, Vector3> Dragged;
        public event Action<MovableObject> Dropped;
        
        [SerializeField] private Camera _camera;

        private MovableObject _draggingObject;

        private void Update()
        {
            HandlePointerDown();
            Drag();
            Drop();
        }

        private void HandlePointerDown()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                
                if (hit.collider != null)
                {
                    _draggingObject = hit.transform.GetComponent<MovableObject>();
                }
            }
        }

        private void Drag()
        {
            if (_draggingObject != null && UnityEngine.Input.GetMouseButton(0))
            {
                var mousePosition = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                var newWorldPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                
                Dragged?.Invoke(_draggingObject, newWorldPosition);
            }
        }

        private void Drop()
        {
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if (_draggingObject != null)
                {
                    Dropped?.Invoke(_draggingObject);
                    _draggingObject = null;
                }
            }
        }
    }
}