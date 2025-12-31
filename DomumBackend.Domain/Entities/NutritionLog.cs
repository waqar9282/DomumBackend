using DomumBackend.Domain.Enums;

namespace DomumBackend.Domain.Entities
{
    /// <summary>
    /// Nutrition Log - Tracks dietary intake and nutrition planning
    /// </summary>
    public class NutritionLog : BaseEntity
    {
        public required string FacilityId { get; set; }
        public required string YoungPersonId { get; set; }
        public YoungPerson? YoungPerson { get; set; }

        // Log Entry Details
        public DateTime LogDate { get; set; }
        public required string MealType { get; set; } // Breakfast, Lunch, Dinner, Snack

        // Food & Nutrition Details
        public required string FoodDescription { get; set; } // What was eaten
        public string? Ingredients { get; set; } // Comma-separated list
        public decimal? PortionSize { get; set; } // grams or ml
        public string? Measurement { get; set; } // grams, ml, servings, pieces

        // Nutritional Information (per serving)
        public decimal? Calories { get; set; }
        public decimal? Protein { get; set; } // grams
        public decimal? Carbohydrates { get; set; } // grams
        public decimal? Fat { get; set; } // grams
        public decimal? Fiber { get; set; } // grams
        public decimal? Sugar { get; set; } // grams
        public decimal? Sodium { get; set; } // mg

        // Allergies & Restrictions
        public string? Allergens { get; set; } // Contains nuts, dairy, etc.
        public string? DietaryRestrictions { get; set; } // Vegetarian, vegan, halal, etc.
        public bool FollowsDietaryPlan { get; set; }

        // Observations
        public string? Appetite { get; set; } // Good, Poor, Moderate
        public string? EatingBehavior { get; set; } // Normal, rushed, picky, etc.
        public string? Preferences { get; set; } // Liked/disliked
        public string? DigestiveIssues { get; set; } // Any problems reported
        public string? Notes { get; set; } // Additional observations

        // Nutrition Plan
        public bool IsPartOfNutritionPlan { get; set; }
        public string? NutritionGoals { get; set; } // Weight gain, gain muscle, maintain, etc.
        public string? DietaryRecommendation { get; set; } // Staff notes on diet

        // Staff Recording
        public required string RecordedByUserId { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
