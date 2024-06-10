using UnityEngine;

namespace Environment
{
    public class TriggerController : BaseComponent
    {
        [SerializeField] private float _searchRadius = 0.1f;
        
        private FoldingObjectController _cachedFoldingObject;
        private MovableObject _movableObject;
        private LayerMask _layerMask;
        
        public FoldingObjectController GetCachedObject()
        {
            return _cachedFoldingObject;
        }
        
        protected override void Awake()
        {
            base.Awake();
            _layerMask = 1 << gameObject.layer;

            _movableObject = ComponentsKeeper.GetFirstComponentOfType<MovableObject>(false);

            _movableObject.Dragged += CheckForObject;
        }

        private void CheckForObject()
        {
            var hits = Physics2D.CircleCastAll(transform.position, _searchRadius, Vector2.zero, 0, _layerMask);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject != gameObject)
                {
                    var hitObject = hit.collider.GetComponent<FoldingObjectController>();

                    if (hitObject != null && _cachedFoldingObject != hitObject)
                    {
                        _cachedFoldingObject = hitObject;
                    }
                    else if (hitObject == null && _cachedFoldingObject != null)
                    {
                        _cachedFoldingObject = null;
                    }
                }
            }
        }

        protected override void OnDestroy()
        {
            _movableObject.Dragged -= CheckForObject;
            
            base.OnDestroy();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}