using UnityEngine;

namespace Custom.Utils {

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

        public class Outline {
            public float size = 1f;
            public Color color = Color.black;
        }

        public World_Bar(Transform parent, Vector3 localPosition, Vector3 localScale, Color? backgroundColor, Color barColor, float sizeRatio, int sortingOrder, Outline outline = null) {
            SetupParent(parent, localPosition);
            if (outline != null) SetupOutline(outline, localScale, sortingOrder - 1);
            if (backgroundColor != null) SetupBackground((Color)backgroundColor, localScale, sortingOrder);
            SetupBar(barColor, localScale, sortingOrder + 1);
            SetSize(sizeRatio);
        }
    }
    
}