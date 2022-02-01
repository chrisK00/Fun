using System.ComponentModel.DataAnnotations;

namespace ArkMultiSave.Helpers;
public static class Val
{
    public static Func<object, ValidationResult> UniqueName(IEnumerable<string> existingNames, string message = "Choose another name")
    {
        return input =>
        {
            if (input is null) return new ValidationResult(message);

            var name = input as string;

            if (string.IsNullOrWhiteSpace(name) || existingNames.Any(n => n.ToLower() == name.ToLower())) return new ValidationResult(message);

            return ValidationResult.Success;
        };
    }
}
