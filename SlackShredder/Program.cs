using System;
using System.Collections.Generic;
using System.Threading;

using SlackShredder.SlackAPI;

namespace SlackShredder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(USAGE);
                Environment.Exit(1);
            }

            string token = args[0];

            AuthTestResult authResult = Api.TestAuth(token);

            if (!authResult.Ok)
            {
                Console.WriteLine(
                    "There was a problem during Slack authentication " + 
                    "(maybe the token is wrong?)");

                Environment.Exit(1);
            }

            Console.Write(
                "All files belonging to [{0}] (team [{1}]) are going to be deleted. " + 
                "Continue? [y/N]: ",
                authResult.User, authResult.Team);

            if (!UserAgreed(Console.ReadLine()))
            {
                Console.WriteLine("Cancelled.");
                Environment.Exit(0);
            }

            DeleteFiles(
                token,
                Api.ListFiles(token, authResult.UserId).Files);
        }

        static void DeleteFiles(string token, List<SlackFile> files)
        {
            for (int i = 0; i < files.Count; i++)
            {
                SlackFile currentFile = files[i];

                PrintDeleteProgress(currentFile, i + 1, files.Count);

                if (!DeleteFileWithRetries(token, currentFile.FileId))
                    PrintDeleteError(currentFile);
            }
        }

        static bool DeleteFileWithRetries(string token, string fileId)
        {
            int retries = 0;
            while (retries < MAX_RETRIES)
            {
                try
                {
                    Api.DeleteFile(token, fileId);
                    return true;
                }
                catch
                {
                    retries++;
                    Thread.Sleep(100);
                }
            }

            return false;
        }

        static void PrintDeleteProgress(
            SlackFile file, int processed, int total)
        {
            Console.WriteLine(
                "[{0} / {1}] Deleting '{2}'",
                processed, total, file.Name);
        }

        static void PrintDeleteError(SlackFile file)
        {
            Console.WriteLine(
                "Error deleting the file '{0}'",
                file.Name);
        }

        static bool UserAgreed(string answer)
        {
            if (string.IsNullOrEmpty(answer))
                return false;

            string trimmedAnswer = answer.Trim();

            return trimmedAnswer.Equals(
                "y", StringComparison.InvariantCultureIgnoreCase);
        }

        const int MAX_RETRIES = 3;

        const string USAGE =
            "Usage:\n" +
            "    SlackShredder.exe <SlackToken>\n" +
            "\n" +
            "You can get your Slack token here:\n" +
            "    https://api.slack.com/custom-integrations/legacy-tokens";
    }

    
}
