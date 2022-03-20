using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace LetsCode.Customs;

public sealed class ReduceConsoleLogFormatter : ConsoleFormatter, IDisposable
{
    private readonly IDisposable _optionsReloadToken;
    private ReduceConsoleLogOptions _formatterOptions;

    public ReduceConsoleLogFormatter(IOptionsMonitor<ReduceConsoleLogOptions> options)
        // Case insensitive
        : base("reduceConsoleLog") =>
        (_optionsReloadToken, _formatterOptions) =
            (options.OnChange(ReloadLoggerOptions), options.CurrentValue);

    private void ReloadLoggerOptions(ReduceConsoleLogOptions options) =>
        _formatterOptions = options;

    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider scopeProvider,
        TextWriter textWriter)
    {
        String message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        String nowFormatted = DateTime.Now.ToString(_formatterOptions.TimestampFormat);

        textWriter.WriteLine($"{nowFormatted} - {message}");
    }

    public void Dispose() => _optionsReloadToken?.Dispose();
}

public class ReduceConsoleLogOptions : ConsoleFormatterOptions
{
}

public static class ReduceConsoleLogFormatterExtensions
{
    public static ILoggingBuilder AddReduceFormatter(this ILoggingBuilder builder, Action<ReduceConsoleLogOptions> configure) =>
        builder
            .AddConsole(options => options.FormatterName = "ReduceConsoleLog")
            .AddConsoleFormatter<ReduceConsoleLogFormatter, ReduceConsoleLogOptions>(configure);
}
