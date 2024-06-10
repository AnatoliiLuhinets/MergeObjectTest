using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace Environment
{
    public class MovableObject : BaseComponent, IDraggable
    {
        public event Action Dragged;
        
        private Vector3 _defaultPosition;
        private float _returnSpeed = 2f;
        private bool _isDragging;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        
        public void Drag(Vector3 newPosition)
        {
            transform.position = newPosition;
            Dragged?.Invoke();
        }

        public void SetDefaultPosition(Vector3 newPosition)
        {
            _defaultPosition = newPosition;
        }

        public void ReturnToStart()
        {
            ResetPosition(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask ResetPosition(CancellationToken token)
        {
            await UniTask.WaitWhile(() => 
            {
                transform.position = Vector3.Lerp(transform.position, _defaultPosition, Time.deltaTime * _returnSpeed);
                
                return Vector3.Distance(transform.position, _defaultPosition) > 0.01 && !token.IsCancellationRequested;
            }, cancellationToken: token);
            
            transform.position = _defaultPosition;
        }

        protected override void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            base.OnDestroy();
        }
    }
}
