﻿//namespace Stravaix.StravaApi.Client
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Threading.Tasks;
//    using Stravaix.StravaApi.Activities;
//    using Stravaix.StravaApi.Api;
//    using Stravaix.StravaApi.Authentication;
//    using Stravaix.StravaApi.Clients;
//    using Stravaix.StravaApi.Common;
//    using Stravaix.StravaApi.Filters;
//    using Stravaix.StravaApi.Http;
//    using Stravaix.StravaApi.Segments;
//    using Stravaix.StravaApi.Strive.Model.Classifications;
//    using Stravaix.StravaApi.Strive.Model.Activities;
//    using Stravaix.StravaApi.Strive.Model.Segments;
//    using Stravaix.StravaApi.Strive.Representations;

//    using Gender = Stravaix.StravaApi.Athletes.Gender;

//    /// <summary>
//    /// Segments are specific sections of road. Athletes’ times are compared on these segments and leaderboards are created.
//    /// With this client you can get various data about segments.
//    /// </summary>
//    public class SegmentClient : BaseClient
//    {
//        /// <summary>
//        /// Initializes a new instance of the SegmentClient class.
//        /// </summary>
//        /// <param name="auth">IAuthentication object containing a valid Strava access token.</param>
//        public SegmentClient(IAuthentication auth) : base(auth) { }

//        #region Async

//        /// <summary>
//        /// Gets all the records of an athlete asynchronously.
//        /// </summary>
//        /// <param name="athleteId">The Strava athlete id.</param>
//        /// <returns>A list of segments where the athlete is the record holder.</returns>
//        public async Task<List<SegmentEffort>> GetRecordsAsync(String athleteId)
//        {
//            String getUrl = String.Format("{0}/{1}/koms?access_token={2}", Endpoints.Athlete, athleteId, Authentication.AccessToken);
//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets all the starred segments of the currently authenticated athlete asynchronously.
//        /// </summary>
//        /// <returns>A list of segments that are starred by the currently authenticated athlete.</returns>
//        public async Task<List<SegmentSummary>> GetStarredSegmentsAsync()
//        {
//            String getUrl = String.Format("{0}/?access_token={1}", Endpoints.Starred, Authentication.AccessToken);
//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<List<SegmentSummary>>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets all the starred segments of an Athlete.
//        /// </summary>
//        /// <returns>A list of segments that are starred by the athlete.</returns>
//        public async Task<List<SegmentSummary>> GetStarredSegmentsAsync(String athleteId)
//        {
//            String getUrl = String.Format("https://www.strava.com/api/v3/athletes/{0}/segments/starred/?access_token={1}", athleteId, Authentication.AccessToken);
//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<List<SegmentSummary>>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the full and unfiltered leaderboard of a Strava segment asynchronously.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <returns>The full and unfiltered leaderboard></returns>
//        public async Task<Leaderboard> GetFullSegmentLeaderboardAsync(String segmentId)
//        {
//            int page = 1;

//            //Create one dummy request to get the number of entries.
//            Leaderboard request = await GetSegmentLeaderboardAsync(segmentId, 1, 1);
//            int totalAthletes = request.EntryCount;

//            Leaderboard leaderboard = new Leaderboard
//            {
//                //EffortCount = request.EffortCount,
//                EntryCount = request.EntryCount
//            };

//            while ((page - 1) * 200 < totalAthletes)
//            {
//                Leaderboard l = await GetSegmentLeaderboardAsync(segmentId, page++, 200);
                
//                foreach (LeaderboardEntry entry in l.Entries)
//                {
//                    leaderboard.Entries.Add(entry);
//                }
//            }

//            return leaderboard;
//        }

//        /// <summary>
//        /// Gets the leaderboard of a Strava segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="page">The page.</param>
//        /// <param name="perPage">Defines how many entries will be loaded per page.</param>
//        /// <returns>The segment leaderboard</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, int page, int perPage)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter=overall&page={2}&per_page={3}&access_token={4}", 
//                Endpoints.Leaderboard, 
//                segmentId,
//                page,
//                perPage,
//                Authentication.AccessToken);
//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the gender-filtered leaderboard of a segment asynchronsouly. This method requires the currently authenticated 
//        /// athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// <returns>The leaderboard filtered by gender.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, Gender gender)
//        {
//            int page = 1;

//            //Create one dummy request to get the number of entries.
//            Leaderboard request = GetSegmentLeaderboard(segmentId, gender, 1, 1);
//            int totalAthletes = request.EntryCount;

//            Leaderboard leaderboard = new Leaderboard
//            {
//                //EffortCount = request.EffortCount,
//                EntryCount = request.EntryCount
//            };

//            while ((page - 1) * 200 < totalAthletes)
//            {
//                Leaderboard l = await GetSegmentLeaderboardAsync(segmentId, gender, page++, 200);

//                foreach (LeaderboardEntry entry in l.Entries)
//                {
//                    leaderboard.Entries.Add(entry);
//                }
//            }

//            return leaderboard;
//        }

//        /// <summary>
//        /// Gets the gender-filtered leaderboard of a segment. This method requires the currently authenticated 
//        /// athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// <param name="page">The result page.</param>
//        /// <param name="perPage">Efforts shown per page.</param>
//        /// <returns>The leaderboard filtered by gender.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, Gender gender, int page, int perPage)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?gender={2}&page={3}&per_page={4}&access_token={5}",
//                Endpoints.Leaderboard,
//                segmentId,
//                gender.ToString().Substring(0, 1),
//                page,
//                perPage,
//                Authentication.AccessToken
//                );

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the leaderboard filtered by time.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="filter">The time filter.</param>
//        /// <returns>A time filtered leaderboard.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, TimeFilter filter)
//        {
//            int page = 1;

//            //Create one dummy request to get the number of entries.
//            Leaderboard request = await GetSegmentLeaderboardAsync(segmentId, filter, 1, 1);
//            int totalAthletes = request.EntryCount;

//            Leaderboard leaderboard = new Leaderboard
//            {
//                //EffortCount = request.EffortCount,
//                EntryCount = request.EntryCount
//            };

//            while ((page - 1) * 200 < totalAthletes)
//            {
//                Leaderboard l = await GetSegmentLeaderboardAsync(segmentId, filter, page++, 200);

//                foreach (LeaderboardEntry entry in l.Entries)
//                {
//                    leaderboard.Entries.Add(entry);
//                }
//            }

//            return leaderboard;
//        }

//        /// <summary>
//        /// Gets the leaderboard filtered by time.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="filter">The time filter.</param>
//        /// <param name="page">The result page.</param>
//        /// <param name="perPage">Entries per page.</param>
//        /// <returns>A time filtered leaderboard.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, TimeFilter filter, int page, int perPage)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?date_range={2}&page={3}&per_page={4}&access_token={5}",
//                Endpoints.Leaderboard,
//                segmentId,
//                UrlHelper.TimeFilterToString(filter),
//                page,
//                perPage,
//                Authentication.AccessToken
//                );

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the leaderboard filtered by time and gender
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="filter">The time filter.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// <param name="page">The result page.</param>
//        /// <param name="perPage">Entries per page.</param>
//        /// <returns>A time filtered leaderboard.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, TimeFilter filter, Gender gender, int page, int perPage)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?date_range={2}&gender={3}&page={4}&per_page={5}&access_token={6}",
//                Endpoints.Leaderboard,
//                segmentId,
//                UrlHelper.TimeFilterToString(filter),
//                gender.ToString().Substring(0, 1),
//                page,
//                perPage,
//                Authentication.AccessToken
//                );

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the gender-filtered and age-filtered leaderboard of a segment asynchronsouly. This method requires the currently authenticated 
//        /// athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// /// <param name="age">The age range used to filter the leaderboard.</param>
//        /// <returns>The leaderboard filtered by gender and age.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, Gender gender, AgeGroup age)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?gender={2}&age_group={3}&filter=age_group&access_token={4}",
//                Endpoints.Leaderboard,
//                segmentId,
//                gender.ToString().Substring(0, 1),
//                UrlHelper.AgeGroupToString(age),
//                Authentication.AccessToken
//                );

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the gender-filtered and weight-class filtered leaderboard of a segment asynchronsouly. This method requires the currently 
//        /// authenticated  athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// /// <param name="weight">The weight class used to filter the leaderboard.</param>
//        /// <returns>The leaderboard filtered by gender and weight class.</returns>
//        public async Task<Leaderboard> GetSegmentLeaderboardAsync(String segmentId, Gender gender, WeightClass weight)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?gender={2}&weight_class={3}&filter=weight_class&access_token={4}",
//                Endpoints.Leaderboard,
//                segmentId,
//                gender.ToString().Substring(0, 1),
//                UrlHelper.WeightClassToString(weight),
//                Authentication.AccessToken
//                );

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the number of entries of a segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <returns>The total number of entries of the specified Strava segment.</returns>
//        public async Task<int> GetSegmentEntryCountAsync(String segmentId)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", Endpoints.Leaderboard, segmentId, Authentication.AccessToken);
//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));
//            Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(json);

//            return leaderboard.EntryCount;
//        }

//        /// <summary>
//        /// Gets the number of efforts of a segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <returns>The total number of efforts of the specified Strava segment.</returns>
//        public async Task<int> GetSegmentEffortCountAsync(String segmentId)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", Endpoints.Leaderboard, segmentId, Authentication.AccessToken);
//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));
//            Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(json);

//            return leaderboard.EffortCount;
//        }

//        /// <summary>
//        /// Returns an array of up to ten segments.
//        /// </summary>
//        /// <param name="southWest">The south western border of the boundary.</param>
//        /// <param name="northEast">The north eastern border of the boundary.</param>
//        /// <returns>Up to ten segments within the boundary box. When there are more than ten segments, the ten most 
//        /// popular ones will be returned.</returns>
//        public async Task<ExplorerResult> ExploreSegmentsAsync(Coordinate southWest, Coordinate northEast)
//        {
//            String getUrl = String.Format("{0}/explore?bounds={1}&access_token={2}",
//                Endpoints.Leaderboard,
//                UrlHelper.BoundariesToString(southWest,northEast),
//                Authentication.AccessToken);

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<ExplorerResult>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Returns an array of up to ten segments.
//        /// </summary>
//        /// <param name="southWest">The south western border of the boundary.</param>
//        /// <param name="northEast">The north eastern border of the boundary.</param>
//        /// <param name="minCat">The min category 0-5, lower is harder.</param>
//        /// <param name="maxCat">The max category 0-5, lower is harder.</param>
//        /// <returns>Up to ten segments within the boundary box. When there are more than ten segments, the ten most 
//        /// popular ones will be returned.</returns>
//        public async Task<ExplorerResult> ExploreSegmentsAsync(Coordinate southWest, Coordinate northEast, int minCat, int maxCat)
//        {
//            String getUrl = String.Format("{0}/explore?bounds={1}&min_cat={2}&max_cat={3}&access_token={4}",
//                Endpoints.Leaderboard,
//                UrlHelper.BoundariesToString(southWest,northEast),
//                minCat,
//                maxCat,
//                Authentication.AccessToken);

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<ExplorerResult>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets a detailed representation of the specified segment id.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <returns>A detailed representation of the segment.</returns>
//        public async Task<Segment> GetSegmentAsync(String segmentId)
//        {
//            String getUrl = String.Format("{0}/{1}?access_token={2}",
//                Endpoints.Leaderboard,
//                segmentId,
//                Authentication.AccessToken);

//            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

//            return Unmarshaller<Segment>.Unmarshal(json);
//        }

//        #endregion

//        #region Sync

//        /// <summary>
//        /// Gets all the records of an athlete.
//        /// </summary>
//        /// <param name="athleteId">The Strava athlete id.</param>
//        /// <returns>A list of segments where the athlete is the record holder.</returns>
//        public List<SegmentEffort> GetRecords(String athleteId)
//        {
//            String getUrl = String.Format("{0}/{1}/koms?access_token={2}", Endpoints.Athlete, athleteId, Authentication.AccessToken);
//            String json = WebRequest.SendGet(new Uri(getUrl));

//            return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets all the starred segments of an Athlete.
//        /// </summary>
//        /// <returns>A list of segments that are starred by the athlete.</returns>
//        public List<SegmentSummary> GetStarredSegments(String athleteId)
//        {
//            String getUrl = String.Format("https://www.strava.com/api/v3/athletes/{0}/segments/starred/?access_token={1}", athleteId, Authentication.AccessToken);
//            String json = WebRequest.SendGet(new Uri(getUrl));

//            return Unmarshaller<List<SegmentSummary>>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets all the starred segments of the currently authenticated athlete.
//        /// </summary>
//        /// <returns>A list of segments that are starred by the currently authenticated athlete.</returns>
//        public List<SegmentSummary> GetStarredSegments()
//        {
//            String getUrl = String.Format("{0}/?access_token={1}", Endpoints.Starred, Authentication.AccessToken);
//            String json = WebRequest.SendGet(new Uri(getUrl));

//            return Unmarshaller<List<SegmentSummary>>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the full and unfiltered leaderboard of a Strava segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <returns>The full and unfiltered leaderboard.</returns>
//        public Leaderboard GetFullSegmentLeaderboard(String segmentId)
//        {
//            int page = 1;

//            //Create one dummy request to get the number of entries.
//            Leaderboard request = GetSegmentLeaderboard(segmentId, 1, 1);
//            int totalAthletes = request.EntryCount;

//            Leaderboard leaderboard = new Leaderboard
//            {
//                EffortCount = request.EffortCount,
//                EntryCount = request.EntryCount
//            };

//            while ((page - 1) * 200 < totalAthletes)
//            {
//                Leaderboard l = GetSegmentLeaderboard(segmentId, page++, 200);

//                foreach (LeaderboardEntry entry in l.Entries)
//                {
//                    leaderboard.Entries.Add(entry);
//                }
//            }

//            return leaderboard;
//        }

//        /// <summary>
//        /// Gets the leaderboard of a Strava segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="page">The page.</param>
//        /// <param name="perPage">Defines how many entries will be loaded per page.</param>
//        /// <returns>The segment leaderboard</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, int page, int perPage)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter=overall&page={2}&per_page={3}&access_token={4}",
//                Endpoints.Leaderboard,
//                segmentId,
//                page,
//                perPage,
//                Authentication.AccessToken);

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the gender-filtered leaderboard of a segment. This method requires the currently authenticated 
//        /// athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// <returns>The leaderboard filtered by gender.</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, Gender gender)
//        {
//            int page = 1;

//            //Create one dummy request to get the number of entries.
//            Leaderboard request = GetSegmentLeaderboard(segmentId, gender, 1, 1);
//            int totalAthletes = request.EntryCount;

//            Leaderboard leaderboard = new Leaderboard
//            {
//            //TODO kommentert ut denne...    //EffortCount = request.EffortCount,
//                EntryCount = request.EntryCount
//            };

//            while ((page - 1) * 200 < totalAthletes)
//            {
//                Leaderboard l = GetSegmentLeaderboard(segmentId, gender, page++, 200);

//                foreach (LeaderboardEntry entry in l.Entries)
//                {
//                    leaderboard.Entries.Add(entry);
//                }
//            }

//            return leaderboard;
//        }

//        /// <summary>
//        /// Gets the gender-filtered leaderboard of a segment. This method requires the currently authenticated 
//        /// athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// <param name="page">The result page.</param>
//        /// <param name="perPage">Efforts shown per page.</param>
//        /// <returns>The leaderboard filtered by gender.</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, Gender gender, int page, int perPage)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?gender={2}&page={3}&per_page={4}&access_token={5}",
//                Endpoints.Leaderboard,
//                segmentId,
//                gender.ToString().Substring(0, 1),
//                page,
//                perPage,
//                Authentication.AccessToken
//                );

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the leaderboard filtered by time and gender
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="filter">The time filter.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// <param name="page">The result page.</param>
//        /// <param name="perPage">Entries per page.</param>
//        /// <returns>A time filtered leaderboard.</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, TimeFilter filter, Gender gender, int page,
//            int perPage)
//        {
//            String getUrl =
//                String.Format("{0}/{1}/leaderboard?date_range={2}&gender={3}&page={4}&per_page={5}&access_token={6}",
//                    Endpoints.Leaderboard,
//                    segmentId,
//                    UrlHelper.TimeFilterToString(filter),
//                    gender.ToString().Substring(0, 1),
//                    page,
//                    perPage,
//                    Authentication.AccessToken
//                    );

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }


//        /// <summary>
//        /// Gets the gender-filtered and age-filtered leaderboard of a segment. This method requires the currently authenticated 
//        /// athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// /// <param name="age">The age range used to filter the leaderboard.</param>
//        /// <returns>The leaderboard filtered by gender and age.</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, Gender gender, AgeGroup age)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?gender={2}&age_group={3}&filter=age_group&access_token={4}",
//                Endpoints.Leaderboard,
//                segmentId,
//                gender.ToString().Substring(0, 1),
//                UrlHelper.AgeGroupToString(age),
//                Authentication.AccessToken
//                );

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the gender-filtered and weight-class filtered leaderboard of a segment. This method requires the currently 
//        /// authenticated  athlete to have a Strava premium account.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment Id.</param>
//        /// <param name="gender">The gender used to filter the leaderboard.</param>
//        /// /// <param name="weight">The weight class used to filter the leaderboard.</param>
//        /// <returns>The leaderboard filtered by gender and weight class.</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, Gender gender, WeightClass weight)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?gender={2}&weight_class={3}&filter=weight_class&access_token={4}",
//                Endpoints.Leaderboard,
//                segmentId,
//                gender.ToString().Substring(0, 1),
//                UrlHelper.WeightClassToString(weight),
//                Authentication.AccessToken
//                );

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets the number of entries of a segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <returns>The total number of entries of the specified Strava segment.</returns>
//        public int GetSegmentEntryCount(String segmentId)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", Endpoints.Leaderboard, segmentId, Authentication.AccessToken);
//            String json = WebRequest.SendGet(new Uri(getUrl));
//            Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(json);

//            return leaderboard.EntryCount;
//        }

//        /// <summary>
//        /// Gets the number of efforts of a segment.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <returns>The total number of efforts of the specified Strava segment.</returns>
//        public int GetSegmentEffortCount(String segmentId)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", Endpoints.Leaderboard, segmentId, Authentication.AccessToken);
//            String json = WebRequest.SendGet(new Uri(getUrl));
//            Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(json);

//            return leaderboard.EffortCount;
//        }

//        /// <summary>
//        /// Gets the leaderboard filtered by time.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <param name="filter">The time filter.</param>
//        /// <returns>A time filtered leaderboard.</returns>
//        public Leaderboard GetSegmentLeaderboard(String segmentId, TimeFilter filter)
//        {
//            String getUrl = String.Format("{0}/{1}/leaderboard?filter={2}&access_token={3}",
//                Endpoints.Leaderboard,
//                segmentId,
//                UrlHelper.TimeFilterToString(filter),
//                Authentication.AccessToken
//                );

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Leaderboard>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Returns an array of up to ten segments.
//        /// </summary>
//        /// <param name="southWest">The south western border of the boundary.</param>
//        /// <param name="northEast">The north eastern border of the boundary.</param>
//        /// <returns>Up to ten segments within the boundary box. When there are more than ten segments, the ten most 
//        /// popular ones will be returned.</returns>
//        public ExplorerResult ExploreSegments(Coordinate southWest, Coordinate northEast)
//        {
//            String getUrl = String.Format("{0}/explore?bounds={1}&access_token={2}",
//                Endpoints.Leaderboard,
//                UrlHelper.BoundariesToString(southWest,northEast),
//                Authentication.AccessToken);

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<ExplorerResult>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Returns an array of up to ten segments.
//        /// </summary>
//        /// <param name="southWest">The south western border of the boundary.</param>
//        /// <param name="northEast">The north eastern border of the boundary.</param>
//        /// <param name="minCat">The min category 0-5, lower is harder.</param>
//        /// <param name="maxCat">The max category 0-5, lower is harder.</param>
//        /// <returns>Up to ten segments within the boundary box. When there are more than ten segments, the ten most 
//        /// popular ones will be returned.</returns>
//        public ExplorerResult ExploreSegments(Coordinate southWest, Coordinate northEast, int minCat, int maxCat)
//        {
//            String getUrl = String.Format("{0}/explore?bounds={1}&min_cat={2}&max_cat={3}&access_token={4}",
//                Endpoints.Leaderboard,
//                UrlHelper.BoundariesToString(southWest,northEast),
//                minCat,
//                maxCat,
//                Authentication.AccessToken);

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<ExplorerResult>.Unmarshal(json);
//        }

//        /// <summary>
//        /// Gets a detailed representation of the specified segment id.
//        /// </summary>
//        /// <param name="segmentId">The Strava segment id.</param>
//        /// <returns>A detailed representation of the segment.</returns>
//        public Segment GetSegment(String segmentId)
//        {
//            String getUrl = String.Format("{0}/{1}?access_token={2}",
//                Endpoints.Leaderboard,
//                segmentId,
//                Authentication.AccessToken);

//            String json = WebRequest.SendGet(new Uri(getUrl));
//            return Unmarshaller<Segment>.Unmarshal(json);
//        }
        
//        #endregion
//    }
//}
