using System;
using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace SlackShredder.SlackAPI
{
    internal static class Api
    {
        internal static AuthTestResult TestAuth(string token)
        {
            return GetSlackResponse<AuthTestResult>(AUTH_TEST_URI, token);
        }

        internal static FileListResult ListFiles(string token, string userId)
        {
            return GetSlackResponse<FileListResult>(FILES_LIST_URI, token, userId);
        }

        internal static DeleteFileResult DeleteFile(string token, string fileId)
        {
            return GetSlackResponse<DeleteFileResult>(FILES_DELETE_URI, token, fileId);
        }

        static T GetSlackResponse<T>(string endpoint, params object[] args)
        {
            try
            {
                return DeserializeWebResponse<T>(GetWebResponse(endpoint, args));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(
                    "There was a problem connecting with Slack: {0}",
                    e.Message);

                return default(T);
            }
        }

        static WebResponse GetWebResponse(string baseUri, params object[] args)
        {
            Uri uri = new Uri(string.Format(baseUri, args));
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Timeout = TIMEOUT;
            return webRequest.GetResponse();
        }

        static T DeserializeWebResponse<T>(WebResponse response)
        {
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string rawResponse = stream.ReadToEnd();

                return string.IsNullOrEmpty(rawResponse)
                    ? default(T)
                    : JsonConvert.DeserializeObject<T>(rawResponse);
            }
        }

        const int TIMEOUT = 5000;

        const string API_BASE_URI = "https://slack.com/api";

        const string AUTH_TEST_URI = API_BASE_URI + "/auth.test?token={0}";
        const string FILES_LIST_URI = API_BASE_URI + "/files.list?token={0}&user={1}";
        const string FILES_DELETE_URI = API_BASE_URI + "/files.delete?token={0}&file={1}";
    }
}
