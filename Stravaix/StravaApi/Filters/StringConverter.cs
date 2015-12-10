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

namespace Stravaix.StravaApi.Filters
{
    using System;
    using Stravaix.StravaApi.Strive.Model.Classifications;

    /// <summary>
    /// Converts various Filters to a string that can be used as a parameter in a Strava request.
    /// </summary>
    public static class StringConverter
    {
        /// <summary>
        /// Converts the time filter to the appropriate string.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The appropriate string for the specified filter.</returns>
        public static String TimeFilterToString(TimeFilter filter)
        {
            switch (filter)
            {
                case TimeFilter.ThisMonth:
                    return "this_month";
                case TimeFilter.ThisWeek:
                    return "this_week";
                case TimeFilter.ThisYear:
                    return "this_year";
                case TimeFilter.Today:
                    return "today";

                default:
                    return String.Empty;

            }
        }

        /// <summary>
        /// Converts the gender filter object to the appropriate string.
        /// </summary>
        /// <param name="gender">The gender filter.</param>
        /// <returns>The appropriate string for the specified filter.</returns>
        public static String GenderFilterToString(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "M";
                case Gender.Female:
                    return "F";

                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Converts the age filter object to the appropriate string.
        /// </summary>
        /// <param name="age">The age filter.</param>
        /// <returns>The appropriate string for the specified filter.</returns>
        public static String AgeFilterToString(AgeGroup age)
        {
            switch (age)
            {
                case AgeGroup.ZeroToTwentyFour:
                    return "0-24";
                case AgeGroup.TwentyFiveToThirtyFour:
                    return "age=25-34";
                case AgeGroup.ThirtyFiveToFortyFour:
                    return "35-44";
                case AgeGroup.FortyFiveToFiftyFour:
                    return "45-54";
                case AgeGroup.FiftyFiveToSixtyFour:
                    return "55-64";
                case AgeGroup.SixtyFivePlus:
                    return "65_plus";

                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Converts the weight filter object to the appropriate string.
        /// </summary>
        /// <param name="weight">The weight class.</param>
        /// <returns>The appropriate string for the specified weight class.</returns>
        public static String WeightFilterToString(WeightGroup weight)
        {
            switch (weight)
            {
                case WeightGroup.ZeroToOneHundredAndTwentyFourPounds:
                    return "0-54";
                case WeightGroup.HundredAndTwentyFivePoundsToHundredAndFortyNinePounds:
                    return "55-64";
                case WeightGroup.HundredAndFiftyPoundsToHundredAndSixtyFourPounds:
                    return "65-74";
                case WeightGroup.HundredAndSixtyFivePoundsToHundredAndSeventyNinePounds:
                    return "75-84";
                case WeightGroup.HundredAndEightyPoundsToHundredAndNinetyNinePounds:
                    return "85-94";
                case WeightGroup.TwoHundredPlusPounds:
                    return "95_plus";

                default:
                    return String.Empty;
            }
        }
    }
}
