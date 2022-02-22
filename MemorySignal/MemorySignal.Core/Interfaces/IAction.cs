using System.ComponentModel.DataAnnotations;

namespace MemorySignal.Core.Interfaces;

public interface IAction<TIn, TOut>
{
    IReadOnlyCollection<ValidationResult> Errors { get; }
    string ErrorsFormatted { get; }

    bool HasErrors { get; }

    void ClearErrors();
    Task<TOut> Execute(TIn request);
}