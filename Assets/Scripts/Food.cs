using UnityEngine;

// Base class representing a general Food item
public abstract class Food
{
    protected Vector2Int position;
    protected GameObject foodGameObject;
    protected float lifetime; // Lifetime in seconds

    public Food(Vector2Int position)
    {
        this.position = position;
        this.lifetime = 5f; // Set a default lifetime of 5 seconds
        InitializeFoodObject();
    }

    // Abstract method to be implemented by each food type to set unique appearance and properties
    protected abstract void InitializeFoodObject();

    // Method to destroy the food object when it is eaten by the snake
    public void DestroyFood()
    {
        if (foodGameObject != null)
        {
            Object.Destroy(foodGameObject);
        }
    }

    // Getter to retrieve the position of the food
    public Vector2Int GetPosition()
    {
        return position;
    }
    // Update the lifetime and check if it has expired
    public bool UpdateLifetime(float deltaTime)
    {
        lifetime -= deltaTime;
        if (lifetime <= 0)
        {
            DestroyFood();
            return true; // Returns true if the food should be removed
        }
        return false;
    }
}

// Derived class for normal food with standard score and appearance
public class NormalFood : Food
{
    public NormalFood(Vector2Int position) : base(position) { }

    protected override void InitializeFoodObject()
    {
        foodGameObject = new GameObject("NormalFood", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(position.x, position.y);
        foodGameObject.transform.localScale = new Vector3(GameAssets.i.foodScale, GameAssets.i.foodScale, 1f);
    }
}

// Derived class for bonus food with a larger appearance and increased score reward
public class BonusFood : Food
{
    public BonusFood(Vector2Int position) : base(position) { }

    protected override void InitializeFoodObject()
    {
        foodGameObject = new GameObject("BonusFood", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.bonusFood; // Set different sprite if desired
        foodGameObject.transform.position = new Vector3(position.x, position.y);
        foodGameObject.transform.localScale = new Vector3(GameAssets.i.foodScale * 1.5f, GameAssets.i.foodScale * 1.5f, 1f); // Larger size for bonus
    }
}

// Derived class for poison food that may reduce score or end the game
public class PoisonFood : Food
{
    public PoisonFood(Vector2Int position) : base(position) { }

    protected override void InitializeFoodObject()
    {
        foodGameObject = new GameObject("PoisonFood", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.posionedFood; // Use a unique sprite if desired
        foodGameObject.transform.position = new Vector3(position.x, position.y);
        foodGameObject.transform.localScale = new Vector3(GameAssets.i.foodScale * 1.2f, GameAssets.i.foodScale * 1.2f, 1f); // Slightly larger for visual difference
    }
}
