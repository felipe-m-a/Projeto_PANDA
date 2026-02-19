using UnityEngine;

namespace Project.Scripts.Adventure.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private float forwardOffset;
        [SerializeField] private float radius = 1f;
        [SerializeField] private LayerMask interactableLayers;

        private readonly Collider[] _buffer = new Collider[32];

        [HideInInspector] public IInteractable focused;

        private void Update()
        {
            var nearest = FindNearestInteractable();
            UpdateFocus(nearest);
        }

        private void OnDrawGizmos()
        {
            var center = transform.position + transform.forward * forwardOffset;
            Gizmos.DrawWireSphere(center, radius);
        }

        private IInteractable FindNearestInteractable()
        {
            var center = transform.position + transform.forward * forwardOffset;
            var count = Physics.OverlapSphereNonAlloc(center, radius, _buffer, interactableLayers,
                QueryTriggerInteraction.Collide);
            IInteractable nearest = null;
            var minDistance = float.MaxValue;

            for (var i = 0; i < count; i++)
            {
                var item = _buffer[i];
                if (item == null) continue;
                var interactable = item.GetComponent<IInteractable>();
                if (interactable == null || !interactable.CanInteract()) continue;
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = interactable;
                }
            }

            return nearest;
        }

        private void UpdateFocus(IInteractable nearest)
        {
            if (ReferenceEquals(focused, nearest)) return;
            focused?.LoseFocus();
            focused = nearest;
            focused?.GainFocus();
        }
    }
}