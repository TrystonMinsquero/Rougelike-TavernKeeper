using UnityEditor.U2D.Path;
using UnityEngine;

namespace Items
{
    public abstract class Pickup : MonoBehaviour
    {
        public float crawlSpeed;
        public float crawlRange;

        public static T CreateInstance<T>(Vector3 position, Sprite sprite) where T : MonoBehaviour
        {
            var gameObject = new GameObject($"{typeof(T).Name}", typeof(T), typeof(SpriteRenderer), typeof(BoxCollider2D))
            {
                transform =
                {
                    position = position
                } 
            };
            var sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = sprite;
            sr.sortingLayerName = "Items";
            var col = gameObject.GetComponent<BoxCollider2D>();
            col.isTrigger = true;

            return gameObject.GetComponent<T>();
        }
    }
}