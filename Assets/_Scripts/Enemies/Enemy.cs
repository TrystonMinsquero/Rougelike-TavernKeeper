using Items;
using Misc;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Health))]
    public abstract class Enemy : MonoBehaviour
    {
        public static string Tag => "Enemy";

        public int damage;

        public DropTable DropTable;

        [SerializeField] protected Transform target;

        private Health _health;
        protected Rigidbody2D _rb;
        protected float PlayerDistance => target == null ? -1 : (target.position - transform.position).magnitude;
        protected Vector2 PlayerDirection => target == null ? Vector2.positiveInfinity : (Vector2)(target.position - transform.position).normalized;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        protected virtual void OnDamaged(float damageAmount)
        {
            Debug.Log($"Took {damageAmount} Damage");
        }
        
        protected virtual void OnDied()
        {
            Debug.Log("Yo, I died. Bruh");
            foreach (var drop in DropTable.GetDrop())
            {
                ItemPickup.CreateInstance(drop.item, transform.position);
            }
            Destroy(this.gameObject);
        }

        private void OnEnable()
        {
            _health.OnDamaged += OnDamaged;
            _health.OnDied += OnDied;
        }

        private void OnDisable()
        {
            _health.OnDamaged -= OnDamaged;
            _health.OnDied -= OnDied;
        }
        
    }
}