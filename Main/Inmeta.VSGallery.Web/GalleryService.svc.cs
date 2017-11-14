using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Galleries.Domain.Model;
using Inmeta.VSGallery.Model;
using VsGallery.WebServices;
using Release = Galleries.Domain.Model.Release;

namespace Inmeta.VSGallery.Web
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GalleryService : IVsIdeService
    {
        public GalleryService()
        {
            Database.SetInitializer(new GalleryInitializer());
        }
        public IdeCategory GetCategoryTree(
            Guid categoryId,
            int level,
            string projectType,
            string templateType,
            string[] skus,
            string[] subSkus,
            int[] templateGroupIds,
            int[] vsVersions,
            string cultureName)
        {
            return null;
        }

        public Task<IdeCategory> GetCategoryTreeAsync(
            Guid categoryId,
            int level,
            string projectType,
            string templateType,
            string[] skus,
            string[] subSkus,
            int[] templateGroupIds,
            int[] vsVersions,
            string cultureName)
        {
            return null;
        }

        public IdeCategory[] GetRootCategories(string cultureName)
        {
            return new[]
                   {
                       new IdeCategory { Title = "Controls" }, new IdeCategory {Title="Templates"},new IdeCategory {Title = "Tools"}
                   };
        }

        public Task<IdeCategory[]> GetRootCategoriesAsync(string cultureName)
        {
            return null;
        }

        public ReleaseQueryResult SearchReleases(
            string searchText,
            string whereClause,
            string orderByClause,
            int? locale,
            int? skip,
            int? take)
        {
            return null;
        }

        public Task<ReleaseQueryResult> SearchReleasesAsync(string searchText, string whereClause, string orderByClause, int? locale, int? skip, int? take)
        {
            return null;
        }

        public IdeCategory[] GetRootCategories2(Dictionary<string, string> requestContext)
        {
            return GetRootCategories(null);
        }

        public Task<IdeCategory[]> GetRootCategories2Async(Dictionary<string, string> requestContext)
        {
            return null;
        }

        public IdeCategory GetCategoryTree2(Guid categoryId, int level, Dictionary<string, string> requestContext)
        {
            return null;
        }

        public Task<IdeCategory> GetCategoryTree2Async(Guid categoryId, int level, Dictionary<string, string> requestContext)
        {
            return null;
        }

        public ReleaseQueryResult SearchReleases2(
            string searchText,
            string whereClause,
            string orderByClause,
            int? skip,
            int? take,
            Dictionary<string, string> requestContext)        
        {
            //Check for vsix specific search. VS does this when checking for updates. If we don't return only the matching vsix, the update functionality in VS doesn't work properly
            var vsixid = ParseVsixId(whereClause);

            var orderBy = OrderByEnum.Ranking;

            if (orderByClause.Contains("Rating"))
                orderBy = OrderByEnum.Rating;
            if (orderByClause.Contains("LastModified"))
                orderBy = OrderByEnum.LastModified;
            if (orderByClause.Contains("DownloadCount"))
                orderBy = OrderByEnum.DownloadCount;
            if (orderByClause.Contains("Name"))
                orderBy = OrderByEnum.Name;
            if (orderByClause.Contains("Author"))
                orderBy = OrderByEnum.Author;

            var orderByDirection = OrderByDirection.Desc;
            if( orderByClause.Contains(" asc"))
                orderByDirection = OrderByDirection.Asc;
            var result = new ReleaseQueryResult();

            using (var ctx = new GalleryContext())
            {
                IEnumerable<Model.Release> releases = null;

                if (!String.IsNullOrEmpty(vsixid))
                {
                    releases = ctx.ReleasesWithStuff.ToList().Where(r => r.Extension.VsixId == vsixid);
                }
                else if (orderBy == OrderByEnum.DownloadCount)
                {
                    if (orderByDirection == OrderByDirection.Desc)
                        releases = ctx.ReleasesWithStuff.OrderByDescending(r => r.DownloadCount);
                    else
                    {
                        releases = ctx.ReleasesWithStuff.OrderBy(r => r.DownloadCount);
                    }
                }
                else if (orderBy == OrderByEnum.Rating || orderBy == OrderByEnum.Ranking)
                {
                    if (orderByDirection == OrderByDirection.Desc)
                        releases = ctx.ReleasesWithStuff.ToList().OrderByDescending(r => r.GetAverageRating());
                    else
                    {
                        releases = ctx.ReleasesWithStuff.ToList().OrderBy(r => r.GetAverageRating());
                    }
                }
                else if (orderBy == OrderByEnum.Name)
                {
                    if (orderByDirection == OrderByDirection.Asc)
                        releases = ctx.ReleasesWithStuff.ToList().OrderByDescending(r => r.Extension.Name);
                    else
                    {
                        releases = ctx.ReleasesWithStuff.ToList().OrderBy(r => r.Extension.Name);
                    }
                }

                if (releases == null)
                    releases = ctx.ReleasesWithStuff;

                result.TotalCount = ctx.ReleasesWithStuff.Count();
                //We should find a way to get the base uri for the service, ugly hack ahead
                var host = OperationContext.Current.IncomingMessageHeaders.To.AbsoluteUri;
                var root = host.Replace("GalleryService.svc", "");
                result.Releases = releases.ToList().Select(r => new Release(r, root)).ToArray();
            }

            return result;
        }

        private static string ParseVsixId(string whereClause)
        {
            string vsixid = null;
            Match match = Regex.Match(whereClause, @"Project\.Metadata\['VsixID'\] = '(?<vsixid>.*?)'", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                vsixid = match.Groups["vsixid"].Value;
            }
            return vsixid;
        }

        public Task<ReleaseQueryResult> SearchReleases2Async(
            string searchText,
            string whereClause,
            string orderByClause,
            int? skip,
            int? take,
            Dictionary<string, string> requestContext)
        {
            return null;
        }

        public string[] GetCurrentVersionsForVsixList(string[] vsixIds, Dictionary<string, string> requestContext)
        {
            var currentVersions = new List<string>();

            using (var ctx = new GalleryContext())
            {
                foreach (var id in vsixIds)
                {
                    var extension = ctx.Extensions.FirstOrDefault(e => e.VsixId == id);
                    currentVersions.Add(extension != null ? extension.VsixVersion : null);
                }
                return currentVersions.ToArray();
            }
        }

        public Task<string[]> GetCurrentVersionsForVsixListAsync(string[] vsixIds, Dictionary<string, string> requestContext)
        {
            return null;
        }
    }
}
