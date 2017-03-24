using System.Collections.Generic;

using Newtonsoft.Json;

namespace SlackShredder.SlackAPI
{
    internal class AuthTestResult
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    internal class FileListResult
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("files")]
        public List<SlackFile> Files { get; set; }
    }

    internal class SlackFile
    {
        [JsonProperty("id")]
        public string FileId { get; set; }

        [JsonProperty("created")]
        public long Created;

        [JsonProperty("timestamp")]
        public long Timestamp;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("mimetype")]
        public string MimeType;

        [JsonProperty("filetype")]
        public string FileType;

        [JsonProperty("pretty_type")]
        public string PrettyType;

        [JsonProperty("user")]
        public string UserId;

        [JsonProperty("editable")]
        public bool IsEditable { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("is_external")]
        public bool IsExternal { get; set; }

        [JsonProperty("external_type")]
        public string ExternalType { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("public_url_shared")]
        public string PublicUrlShared { get; set; }
    }

    internal class DeleteFileResult
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
    }
}
