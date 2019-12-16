﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Pixeval.Data.ViewModel;
using Pixeval.Data.Web.Delegation;
using Pixeval.Data.Web.Request;
using Pixeval.Data.Web.Response;
using Refit;

namespace Pixeval.Core
{
    public class PixivHelper
    {
        public static async Task<Illustration> IllustrationInfo(string id)
        {
            IllustResponse.Response response;
            try
            {
                response = (await HttpClientFactory.PublicApiService.GetSingle(id)).ToResponse[0];
            }
            catch (ApiException)
            {
                return null;
            }

            var illust = new Illustration
            {
                Bookmark = (int) (response.Stats.FavoritedCount.Private + response.Stats.FavoritedCount.Public),
                Id = response.Id.ToString(),
                IsLiked = response.FavoriteId != 0,
                IsManga = response.IsManga,
                IsUgoira = response.Type == "ugoira",
                Origin = response.ImageUrls.Large,
                Tags = response.Tags.ToArray(),
                Thumbnail = response.ImageUrls.Px480Mw ?? response.ImageUrls.Medium,
                Title = response.Title,
                Type = Illustration.IllustType.Parse(response),
                UserName = response.User.Name,
                UserId = response.User.Id.ToString()
            };

            if (illust.IsManga)
                illust.MangaMetadata = response.Metadata.Pages.Select(p =>
                {
                    var page = (Illustration) illust.Clone();
                    page.IsManga = false;
                    page.Origin = p.ImageUrls.Large;
                    return page;
                }).ToArray();

            return illust;
        }

        public static async Task<int> GetUploadPagesCount(string uid)
        {
            return (int) (await HttpClientFactory.PublicApiService.GetUploads(uid, new UploadsRequest {Page = 1, PerPage = 1}))
                .UploadPagination
                .Pages;
        }

        public static async Task<int> GetQueryPagesCount(string tag)
        {
            var total = (double) (await HttpClientFactory.PublicApiService.QueryWorks(new QueryWorksRequest {Tag = tag, Offset = 1, PerPage = 1}))
                        .QueryPagination
                        .Pages / 300;

            return (int) Math.Ceiling(total);
        }
    }
}