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

import { ThemeFontAwesomeWeight } from "@Obsidian/Enums/Cms/themeFontAwesomeWeight";
import { ThemeIconSet } from "@Obsidian/Enums/Cms/themeIconSet";
import { ThemeFieldBag } from "@Obsidian/ViewModels/Blocks/Cms/ThemeDetail/themeFieldBag";
import { PublicAttributeBag } from "@Obsidian/ViewModels/Utility/publicAttributeBag";

/** The details about a theme being viewed or edited. */
export type ThemeBag = {
    /**
     * Any additional weights to be included in the theme. This will allow
     * weight specific icons such as fas fa-star or fal fa-star.
     */
    additionalFontAwesomeWeights?: ThemeFontAwesomeWeight[] | null;

    /** Gets or sets the attributes. */
    attributes?: Record<string, PublicAttributeBag> | null;

    /** Gets or sets the attribute values. */
    attributeValues?: Record<string, string> | null;

    /**
     * Specifies which icon sets are supported by the theme. This cannot
     * be edited.
     */
    availableIconSets: ThemeIconSet;

    /** The CSS overrides that were manually entered by the person. */
    customOverrides?: string | null;

    /**
     * The default FontAwesome icon weight to include in the theme. This
     * will allow uses such as fa fa-star.
     */
    defaultFontAwesomeWeight: ThemeFontAwesomeWeight;

    /** Gets or sets the description of the theme, this cannot be edited. */
    description?: string | null;

    /** Specifies which icon sets are enabled for the theme. */
    enabledIconSets: ThemeIconSet;

    /** The fields that should be displayed when editing the theme. */
    fields?: ThemeFieldBag[] | null;

    /** Gets or sets the identifier key of this entity. */
    idKey?: string | null;

    /** Gets or sets a flag indicating if this Rock.Model.Theme is a part of the Rock core system/framework. This property is required. */
    isSystem: boolean;

    /** Gets or sets the name of the theme, this cannot be edited. */
    name?: string | null;

    /** The purpose of the theme, this cannot be edited. */
    purpose?: string | null;

    /** The values of the CSS variables. */
    variableValues?: Record<string, string> | null;
};
