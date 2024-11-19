using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class LevelGrid {

    private List<Food> foodList = new List<Food>();
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void Setup(Snake snake) {
        this.snake = snake;

        SpawnFood();
    }

    private void SpawnFood()
    {
        int targetFoodCount = 3; // Desired number of food items on the screen

        // Only spawn new food if we have fewer than the target count
        while (foodList.Count < targetFoodCount)
        {
            Vector2Int foodPosition;

            // Generate a random position and ensure it doesn't overlap with existing food or the snake
            bool positionIsOccupied;
            do
            {
                positionIsOccupied = false;
                foodPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

                // Check if this position is occupied by any existing food item
                foreach (var food in foodList)
                {
                    if (food.GetPosition() == foodPosition)
                    {
                        positionIsOccupied = true;
                        break;
                    }
                }

                // Check if this position is occupied by any part of the snake
                if (!positionIsOccupied && snake.GetFullSnakeGridPositionList().Contains(foodPosition))
                {
                    positionIsOccupied = true;
                }

            } while (positionIsOccupied); // Repeat if position is occupied

            // Randomly decide which type of food to spawn
            float randomValue = Random.value;
            Food newFood;
            if (randomValue < 0.7f)
            {
                newFood = new NormalFood(foodPosition); // 70% chance for normal food
            }
            else if (randomValue < 0.9f)
            {
                newFood = new BonusFood(foodPosition);  // 20% chance for bonus food
            }
            else
            {
                newFood = new PoisonFood(foodPosition); // 10% chance for poison food
            }

            foodList.Add(newFood); // Add the new food item to the list
        }
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        foreach (var food in foodList)
        {
            if (snakeGridPosition == food.GetPosition())
            {
                food.DestroyFood(); // Only destroy the eaten food
                foodList.Remove(food); // Remove eaten food from the list

                // Apply the effect based on food type
                if (food is BonusFood)
                {
                    Score.AddScore(200); // Extra points for bonus food
                }
                else if (food is PoisonFood)
                {
                    snake.SetGameOver(); // Stop snake movement for poisoned food
                    GameHandler.SnakeDied();
                    return true; // End game immediately
                }
                else
                {
                    Score.AddScore(100); // Regular score for normal food
                }

                // Spawn new food if needed (to maintain the desired number of food items)
                SpawnFood();
                return true;
            }
        }
        return false;
    }
    // Update method to check food timers and remove expired food
    public void Update(float deltaTime)
    {
        for (int i = foodList.Count - 1; i >= 0; i--)
        {
            bool foodExpired = foodList[i].UpdateLifetime(deltaTime);
            if (foodExpired)
            {
                foodList.RemoveAt(i);
                SpawnFood(); // Ensure the total number of food items remains the same
            }
        }
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        if (gridPosition.x < 0) {
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1) {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0) {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1) {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
