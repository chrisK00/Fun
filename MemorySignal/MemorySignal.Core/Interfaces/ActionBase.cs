using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Core.Interfaces;

public abstract class ActionBase<TIn, TOut> : IAction<TIn, TOut>
{
    protected List<ValidationResult> _errors = new();

    public IReadOnlyCollection<ValidationResult> Errors => _errors;
    public bool HasErrors => Errors.Count > 0;

    public string ErrorsFormatted
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var error in Errors)
            {
                    sb.AppendLine(error.ErrorMessage);
            }

            return sb.ToString();
        }
    }

    public void ClearErrors() => _errors.Clear();

    public abstract Task<TOut> Execute(TIn request);

    protected void AddError(string message)
    {
        _errors.Add(new ValidationResult(message));
    }

    protected void AddError(string property, string message)
    {
        _errors.Add(new ValidationResult(message, new[] { property }));
    }

    /// <summary>
    /// Validates any properties with data annotations
    /// </summary>
    /// <param name="request"></param>
    /// <returns>If <paramref name="request"/> is valid</returns>
    protected bool Validate(TIn request)
    {
        var context = new ValidationContext(request);
        return Validator.TryValidateObject(request, context, _errors, true);
    }
}