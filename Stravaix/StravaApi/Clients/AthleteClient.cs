﻿#region Copyright (C) 2014 Sascha Simon

//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.
//
//  Visit the official homepage at http://www.sascha-simon.com

#endregion

namespace Stravaix.StravaApi.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Stravaix.StravaApi.Api;
    using Stravaix.StravaApi.Athletes;
    using Stravaix.StravaApi.Authentication;
    using System.Net.Http;

    using Stravaix.StravaApi.Common;
    using Stravaix.StravaApi.Http;
    using Stravaix.StravaApi.Strive.Representations;

    /// <summary>
    /// Used to receive information about an athlete from Strava.
    /// </summary>
    public class AthleteClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the AthleteClient class.
        /// </summary>
        /// <param name="auth">The IAuthentication object containing a valid access token.</param>
        public AthleteClient(IAuthentication auth) : base(auth) { }

        #region Async

        /// <summary>
        /// Asynchronously receives the currently authenticated athlete.
        /// </summary>
        /// <returns>The currently authenticated athlete.</returns>
        public async Task<Athlete> GetAthleteAsync()
        {
            String getUrl = String.Format("{0}?access_token={1}", new Endpoints().Athlete, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<Athlete>(json);
        }

        /// <summary>
        /// Asynchronously receives the currently authenticated athlete.
        /// </summary>
        /// <param name="athleteId">The Strava Id of the athlete.</param>
        /// <returns>The AthleteSummary object of the athlete.</returns>
        public async Task<AthleteSummary> GetAthleteAsync(String athleteId)
        {
            String getUrl = String.Format("{0}/{1}?access_token={2}", new Endpoints().Athletes, athleteId, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<AthleteSummary>(json);
        }

        /// <summary>
        /// Gets the friends of the currently authenticated athlete.
        /// </summary>
        /// <returns>A list of the friends of the currently authenticated athlete.</returns>
        public async Task<List<AthleteSummary>> GetFriendsAsync()
        {
            String getUrl = String.Format("{0}/friends?access_token={1}", new Endpoints().Athlete, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Gets a list of friends of an athlete.
        /// </summary>
        /// <param name="athleteId">The Strava athlete id.</param>
        /// <returns>The list of friends of the athlete.</returns>
        public async Task<List<AthleteSummary>> GetFriendsAsync(String athleteId)
        {
            String getUrl = String.Format("{0}/friends?access_token={1}", new Endpoints().Athlete, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Gets all the followers of the currently authenticated athlete.
        /// </summary>
        /// <returns>A list of athletes that follow the currently authenticated athlete.</returns>
        public async Task<List<AthleteSummary>> GetFollowersAsync()
        {
            String getUrl = String.Format("{0}?access_token={1}", new Endpoints().Follower, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Get a list of athletes that follow an athlete.
        /// </summary>
        /// <param name="athleteId">The Strava athlete id.</param>
        /// <returns>A list of athletes that follow the specified athlete.</returns>
        public async Task<List<AthleteSummary>> GetFollowersAsync(String athleteId)
        {
            String getUrl = String.Format("{0}/{1}/followers?access_token={2}", new Endpoints().Followers, athleteId, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Get a list of athletes that both you and the specified athlete are following.
        /// </summary>
        /// <param name="athleteId">The Strava athlete id.</param>
        /// <returns>A list of athletes that both you and the specified athlete are following.</returns>
        public async Task<List<AthleteSummary>> GetBothFollowingAsync(String athleteId)
        {
            String getUrl = String.Format("{0}/{1}/both-following?access_token={2}", new Endpoints().Followers, athleteId, this.Authentication.AccessToken);
            String json = await WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Updates the specified parameter of an athlete.
        /// </summary>
        /// <param name="parameter">The parameter that is being updated.</param>
        /// <param name="value">The value to update to.</param>
        /// <returns>Athlete object of the currently authenticated athlete with the updated parameter.</returns>
        public async Task<Athlete> UpdateAthleteAsync(AthleteParameter parameter, String value)
        {
            String putUrl = String.Empty;

            switch (parameter)
            {
                case AthleteParameter.City:
                    putUrl = String.Format("{0}?city={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
                case AthleteParameter.Country:
                    putUrl = String.Format("{0}?country={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
                case AthleteParameter.State:
                    putUrl = String.Format("{0}?state={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
                case AthleteParameter.Weight:
                    putUrl = String.Format("{0}?weight={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
            }

            String json = await WebRequest.SendPutAsync(new Uri(putUrl));

            return Unmarshaller.Unmarshal<Athlete>(json);
        }

        /// <summary>
        /// Updates the sex of the currently authenticated athlete.
        /// </summary>
        /// <param name="gender">The gender to update to.</param>
        /// <returns>The currently authenticated athlete.</returns>
        public async Task<Athlete> UpdateAthleteSex(Gender gender)
        {
            String putUrl = String.Format("{0}?sex={1}&access_token={2}", new Endpoints().Athlete, gender.ToString().Substring(0, 1), this.Authentication.AccessToken);
            String json = await WebRequest.SendPutAsync(new Uri(putUrl));

            return Unmarshaller.Unmarshal<Athlete>(json);
        }

        #endregion

        #region Sync

        /// <summary>
        /// Receives the currently authenticated athlete.
        /// </summary>
        /// <returns>The currently authenticated athlete.</returns>
        public Athlete GetAthlete()
        {
            String getUrl = String.Format("{0}?access_token={1}", new Endpoints().Athlete, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller.Unmarshal<Athlete>(json);
        }

        /// <summary>
        /// Receives a Strava athlete.
        /// </summary>
        /// <param name="athleteId">The Strava Id of the athlete.</param>
        /// <returns>The AthleteSummary object of the athlete.</returns>
        public AthleteSummary GetAthlete(String athleteId)
        {
            String getUrl = String.Format("{0}/{1}?access_token={2}", new Endpoints().Athletes, athleteId, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller.Unmarshal<AthleteSummary>(json);
        }

        /// <summary>
        /// Gets the friends of the currently authenticated athlete.
        /// </summary>
        /// <returns>A list of the friends of the currently authenticated athlete.</returns>
        public List<AthleteSummary> GetFriends()
        {
            String getUrl = String.Format("{0}/friends?access_token={1}", new Endpoints().Athlete, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));
            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Gets a list of friends of an athlete.
        /// </summary>
        /// <param name="athleteId">The Strava athlete id.</param>
        /// <returns>The list of friends of the athlete.</returns>
        public List<AthleteSummary> GetFriends(String athleteId)
        {
            String getUrl = String.Format("{0}/friends?access_token={1}", new Endpoints().Athlete, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Gets all the followers of the currently authenticated athlete.
        /// </summary>
        /// <returns>A list of athletes that follow the currently authenticated athlete.</returns>
        public List<AthleteSummary> GetFollowers()
        {
            String getUrl = String.Format("{0}?access_token={1}", new Endpoints().Follower, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));
            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Get a list of athletes that follow an athlete.
        /// </summary>
        /// <param name="athleteId">The Strava athlete id.</param>
        /// <returns>A list of athletes that follow the specified athlete.</returns>
        public List<AthleteSummary> GetFollowers(String athleteId)
        {
            String getUrl = String.Format("{0}/{1}/followers?access_token={2}", new Endpoints().Followers, athleteId, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Get a list of athletes that both you and the specified athlete are following.
        /// </summary>
        /// <param name="athleteId">The Strava athlete id.</param>
        /// <returns>A list of athletes that both you and the specified athlete are following.</returns>
        public List<AthleteSummary> GetBothFollowing(String athleteId)
        {
            String getUrl = String.Format("{0}/{1}/both-following?access_token={2}", new Endpoints().Followers, athleteId, this.Authentication.AccessToken);
            String json = WebRequest.SendGet(new Uri(getUrl));

            return Unmarshaller.Unmarshal<List<AthleteSummary>>(json);
        }

        /// <summary>
        /// Updates the specified parameter of an athlete.
        /// </summary>
        /// <param name="parameter">The parameter that is being updated.</param>
        /// <param name="value">The value to update to.</param>
        /// <returns>Athlete object of the currently authenticated athlete with the updated parameter.</returns>
        public Athlete UpdateAthlete(AthleteParameter parameter, String value)
        {
            String putUrl = String.Empty;

            switch (parameter)
            {
                case AthleteParameter.City:
                    putUrl = String.Format("{0}?city={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
                case AthleteParameter.Country:
                    putUrl = String.Format("{0}?country={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
                case AthleteParameter.State:
                    putUrl = String.Format("{0}?state={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
                case AthleteParameter.Weight:
                    putUrl = String.Format("{0}?weight={1}&access_token={2}", new Endpoints().Athlete, value, this.Authentication.AccessToken);
                    break;
            }

            String json = WebRequest.SendPut(new Uri(putUrl));

            return Unmarshaller.Unmarshal<Athlete>(json);
        }

        #endregion
    }
}
