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

using System;
using System.Linq;

using Rock.Data;

namespace Rock.Model
{
    /// <summary>
    /// Page Service class
    /// </summary>
    public partial class PageService : Service<Page>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageService"/> class
        /// </summary>
        /// <param name="context">The context.</param>
        public PageService(RockContext context) : base(context)
        {
        }

        /// <summary>
        /// Determines whether this instance can delete the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>
        ///   <c>true</c> if this instance can delete the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanDelete( Page item, out string errorMessage )
        {
            errorMessage = string.Empty;

            if ( new Service<ConnectionType>( Context ).Queryable().Any( a => a.ConnectionRequestDetailPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, ConnectionType.FriendlyTypeName );
                return false;
            }

            if ( new Service<Page>( Context ).Queryable().Any( a => a.ParentPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} contains one or more child {1}.", Page.FriendlyTypeName, Page.FriendlyTypeName.Pluralize().ToLower() );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.ChangePasswordPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.CommunicationPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.DefaultPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.LoginPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.MobilePageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.PageNotFoundPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }

            if ( new Service<Site>( Context ).Queryable().Any( a => a.RegistrationPageId == item.Id ) )
            {
                errorMessage = string.Format( "This {0} is assigned to a {1}.", Page.FriendlyTypeName, Site.FriendlyTypeName );
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Generated Extension Methods
    /// </summary>
    public static partial class PageExtensionMethods
    {
        /// <summary>
        /// Clones this Page object to a new Page object
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="deepCopy">if set to <c>true</c> a deep copy is made. If false, only the basic entity properties are copied.</param>
        /// <returns></returns>
        public static Page Clone( this Page source, bool deepCopy )
        {
            if (deepCopy)
            {
                return source.Clone() as Page;
            }
            else
            {
                var target = new Page();
                target.CopyPropertiesFrom( source );
                return target;
            }
        }

        /// <summary>
        /// Clones this Page object to a new Page object with default values for the properties in the Entity and Model base classes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static Page CloneWithoutIdentity( this Page source )
        {
            var target = new Page();
            target.CopyPropertiesFrom( source );

            target.Id = 0;
            target.Guid = Guid.NewGuid();
            target.ForeignKey = null;
            target.ForeignId = null;
            target.ForeignGuid = null;
            target.CreatedByPersonAliasId = null;
            target.CreatedDateTime = RockDateTime.Now;
            target.ModifiedByPersonAliasId = null;
            target.ModifiedDateTime = RockDateTime.Now;

            return target;
        }

        /// <summary>
        /// Copies the properties from another Page object to this Page object
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void CopyPropertiesFrom( this Page target, Page source )
        {
            target.Id = source.Id;
            #pragma warning disable 612, 618
            target.AdditionalSettings = source.AdditionalSettings;
            #pragma warning restore 612, 618
            target.AdditionalSettingsJson = source.AdditionalSettingsJson;
            target.AllowIndexing = source.AllowIndexing;
            target.BodyCssClass = source.BodyCssClass;
            target.BotGuardianLevel = source.BotGuardianLevel;
            target.BreadCrumbDisplayIcon = source.BreadCrumbDisplayIcon;
            target.BreadCrumbDisplayName = source.BreadCrumbDisplayName;
            target.BrowserTitle = source.BrowserTitle;
            target.CacheControlHeaderSettings = source.CacheControlHeaderSettings;
            target.Description = source.Description;
            target.DisplayInNavWhen = source.DisplayInNavWhen;
            target.EnableViewState = source.EnableViewState;
            target.ForeignGuid = source.ForeignGuid;
            target.ForeignKey = source.ForeignKey;
            target.HeaderContent = source.HeaderContent;
            target.IconBinaryFileId = source.IconBinaryFileId;
            target.IconCssClass = source.IconCssClass;
            target.IncludeAdminFooter = source.IncludeAdminFooter;
            target.InternalName = source.InternalName;
            target.IsSystem = source.IsSystem;
            target.KeyWords = source.KeyWords;
            target.LayoutId = source.LayoutId;
            target.MedianPageLoadTimeDurationSeconds = source.MedianPageLoadTimeDurationSeconds;
            target.MenuDisplayChildPages = source.MenuDisplayChildPages;
            target.MenuDisplayDescription = source.MenuDisplayDescription;
            target.MenuDisplayIcon = source.MenuDisplayIcon;
            target.Order = source.Order;
            target.PageDisplayBreadCrumb = source.PageDisplayBreadCrumb;
            target.PageDisplayDescription = source.PageDisplayDescription;
            target.PageDisplayIcon = source.PageDisplayIcon;
            target.PageDisplayTitle = source.PageDisplayTitle;
            target.PageTitle = source.PageTitle;
            target.ParentPageId = source.ParentPageId;
            #pragma warning disable 612, 618
            target.RateLimitPeriod = source.RateLimitPeriod;
            #pragma warning restore 612, 618
            target.RateLimitPeriodDurationSeconds = source.RateLimitPeriodDurationSeconds;
            target.RateLimitRequestPerPeriod = source.RateLimitRequestPerPeriod;
            target.RequiresEncryption = source.RequiresEncryption;
            target.CreatedDateTime = source.CreatedDateTime;
            target.ModifiedDateTime = source.ModifiedDateTime;
            target.CreatedByPersonAliasId = source.CreatedByPersonAliasId;
            target.ModifiedByPersonAliasId = source.ModifiedByPersonAliasId;
            target.Guid = source.Guid;
            target.ForeignId = source.ForeignId;

        }
    }
}
