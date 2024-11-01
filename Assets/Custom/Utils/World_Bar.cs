using UnityEngine;
namespace Custom.Utils{
    
    /* Bar in the World, great for quickly making a health bar
    */
    public class World_Bar {
        
        private GameObject gameObject;
        private Transform transform;
        private Transform background;
        private Transform bar;

        public static int GetSortingOrder(Vector3 position, int offset, int baseSortingOrder = 5000) {
            return (int)(baseSortingOrder - position.y) + offset;
        }
}