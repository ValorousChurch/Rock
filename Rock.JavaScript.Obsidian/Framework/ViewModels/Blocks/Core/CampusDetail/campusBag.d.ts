//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//

import { CampusScheduleBag } from "@Obsidian/ViewModels/Blocks/Core/CampusDetail/campusScheduleBag";
import { CampusTopicBag } from "@Obsidian/ViewModels/Blocks/Core/CampusDetail/campusTopicBag";
import { ListItemBag } from "@Obsidian/ViewModels/Utility/listItemBag";
import { PublicAttributeBag } from "@Obsidian/ViewModels/Utility/publicAttributeBag";

/**
 * Class CampusBag.
 * Implements the Rock.ViewModels.Utility.EntityBagBase
 */
export type CampusBag = {
    /** Gets or sets the attributes. */
    attributes?: Record<string, PublicAttributeBag> | null;

    /** Gets or sets the attribute values. */
    attributeValues?: Record<string, string> | null;

    /** Gets or sets the campus schedules. */
    campusSchedules?: CampusScheduleBag[] | null;

    /** Gets or sets the campus status value. */
    campusStatusValue?: ListItemBag | null;

    /** Gets or sets the campus topics. The Campus Topic is a Defined Value. */
    campusTopics?: CampusTopicBag[] | null;

    /** Gets or sets the campus type value. */
    campusTypeValue?: ListItemBag | null;

    /** Gets or sets the description. */
    description?: string | null;

    /** Gets or sets the identifier key of this entity. */
    idKey?: string | null;

    /** Gets or sets a value indicating whether this instance is active. */
    isActive?: boolean | null;

    /** Gets or sets a value indicating whether this instance is system. */
    isSystem: boolean;

    /** Gets or sets the leader person alias. */
    leaderPersonAlias?: ListItemBag | null;

    /** Gets or sets the location. */
    location?: ListItemBag | null;

    /** Gets or sets the name. */
    name?: string | null;

    /** Gets or sets the phone number. */
    phoneNumber?: string | null;

    /** Get or sets the Country Code of the Phone Number */
    phoneNumberCountryCode?: string | null;

    /** Gets or sets the service times. */
    serviceTimes?: ListItemBag[] | null;

    /** Gets or sets the short code. */
    shortCode?: string | null;

    /** Gets or sets the time zone identifier. */
    timeZoneId?: string | null;

    /** Gets or sets the URL. */
    url?: string | null;
};
