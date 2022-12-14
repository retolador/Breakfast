namespace Breakfast.ServiceErrors;

using ErrorOr;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast Not found");

        public static Error InvalidName => Error.NotFound(
            code: "Breakfast.InvalidName",
            description: $" Breakfast name must be at least {Models.Breakfast.MinNameLength} and not passs"+
            $" {Models.Breakfast.MaxNameLength} ");

         public static Error InvalidDescription => Error.NotFound(
            code: "Breakfast.InvalidDescription",
            description: $" Breakfast description must be at least {Models.Breakfast.MinDescriptionLength} and not passs"+
            $" {Models.Breakfast.MaxDescriptionLength}");
    }
}