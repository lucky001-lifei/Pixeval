﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Pixeval.Objects;

namespace Pixeval.Data.Web.Response
{
    public class UserNavResponse
    {
        [JsonProperty("user_previews")]
        public List<UserPreview> UserPreviews { get; set; }

        [JsonProperty("next_url")]
        public string NextUrl { get; set; }

        public override string ToString()
        {
            return this.ToJson();
        }

        public class UserPreview
        {
            [JsonProperty("user")]
            public User User { get; set; }

            [JsonProperty("illusts")]
            public List<Illust> Illusts { get; set; }

            [JsonProperty("is_muted")]
            public bool IsMuted { get; set; }
        }

        public class Illust
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("image_urls")]
            public ImageUrls ImageUrl { get; set; }

            [JsonProperty("tools")]
            public string[] Tools { get; set; }

            [JsonProperty("page_count")]
            public long PageCount { get; set; }
        }

        public class ImageUrls
        {
            [JsonProperty("square_medium")]
            public string SquareMedium { get; set; }

            [JsonProperty("medium")]
            public string Medium { get; set; }

            [JsonProperty("large")]
            public string Large { get; set; }

            [JsonProperty("original", NullValueHandling = NullValueHandling.Ignore)]
            public string Original { get; set; }
        }

        public class User
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("account")]
            public string Account { get; set; }

            [JsonProperty("profile_image_urls")]
            public ProfileImageUrls ProfileImageUrls { get; set; }

            [JsonProperty("is_followed")]
            public bool IsFollowed { get; set; }
        }

        public class ProfileImageUrls
        {
            [JsonProperty("medium")]
            public string Medium { get; set; }
        }
    }
}