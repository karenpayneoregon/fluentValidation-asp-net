﻿// ReSharper disable InconsistentNaming

// ReSharper disable MethodSupportsCancellation
namespace NotesRazorApp.Classes
{
    /// <summary>
    /// Provides a mechanism for logging Entity Framework Core (EF Core) database context activities to a file.
    /// </summary>
    /// <remarks>
    /// This class is intended for development purposes only and should not be used in production environments.
    /// It supports both asynchronous and synchronous logging of messages to a specified file.
    /// </remarks>
    public class DbContextToFileLogger
    {
        private readonly string _fileName;
        private static readonly SemaphoreSlim _gate = new(1, 1);

        public DbContextToFileLogger(string fileName)
        {
            _fileName = fileName;
        }

        public DbContextToFileLogger()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory; // OK for local dev
            var folder = Path.Combine(baseDir, "LogFiles",
                $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
            _fileName = Path.Combine(folder, "EF_Log.txt");
        }

        public async Task LogAsync(string message, CancellationToken ct = default)
        {
            // Make sure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(_fileName)!);

            await _gate.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                // Single open, append mode, allow others to read/write if they also opt in
                await using var fs = new FileStream(
                    _fileName,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.ReadWrite,     // <-- important
                    bufferSize: 4096,
                    FileOptions.Asynchronous | FileOptions.WriteThrough);

                await using var writer = new StreamWriter(fs);
                await writer.WriteLineAsync(message.AsMemory(), ct);
                await writer.WriteLineAsync(new string('-', 40).AsMemory(), ct);
                await writer.FlushAsync();
            }
            finally
            {
                _gate.Release();
            }
        }

        // Optional sync shim if you must keep the old signature
        public void Log(string message) => LogAsync(message).GetAwaiter().GetResult();
    }
}
