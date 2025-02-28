﻿// <copyright>
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;

using Rock;
using Rock.Attribute;
using Rock.Data;
using Rock.Model;
using Rock.Security;
using Rock.Web.Cache;
using Rock.Web.UI;
using Rock.Web.UI.Controls;

namespace RockWeb.Blocks.Finance
{
    /// <summary>
    /// Block for viewing list of financial gateways
    /// </summary>
    [DisplayName( "Gateway List" )]
    [Category( "Finance" )]
    [Description( "Block for viewing list of financial gateways." )]
    [LinkedPage( "Detail Page" )]
    [Rock.SystemGuid.BlockTypeGuid( "32E89BAE-C085-40B3-B872-B62E25A62BDB" )]
    public partial class GatewayList : RockBlock, ICustomGridColumns
    {
        #region Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            bool canEdit = IsUserAuthorized( Authorization.EDIT );

            rGridGateway.DataKeyNames = new string[] { "Id" };
            rGridGateway.Actions.ShowAdd = canEdit;
            rGridGateway.Actions.AddClick += rGridGateway_Add;
            rGridGateway.GridRebind += rGridGateways_GridRebind;
            rGridGateway.IsDeleteEnabled = canEdit;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                BindGrid();
            }

            base.OnLoad( e );
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Add event of the rGridGateway control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void rGridGateway_Add( object sender, EventArgs e )
        {
            var parms = new Dictionary<string, string>();
            parms.Add( "GatewayId", "0" );
            NavigateToLinkedPage( "DetailPage", parms );
        }

        /// <summary>
        /// Handles the Edit event of the rGridGateway control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowEventArgs"/> instance containing the event data.</param>
        protected void rGridGateway_Edit( object sender, RowEventArgs e )
        {
            var parms = new Dictionary<string, string>();
            parms.Add( "GatewayId", e.RowKeyValue.ToString() );
            NavigateToLinkedPage( "DetailPage", parms );
        }

        /// <summary>
        /// Handles the Delete event of the rGridGateway control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RowEventArgs"/> instance containing the event data.</param>
        protected void rGridGateway_Delete( object sender, RowEventArgs e )
        {
            var rockContext = new RockContext();
            var gatewayService = new FinancialGatewayService( rockContext );
            var gateway = gatewayService.Get( e.RowKeyId );
            if ( gateway != null )
            {
                string errorMessage;
                if ( !gatewayService.CanDelete( gateway, out errorMessage ) )
                {
                    mdGridWarning.Show( errorMessage, ModalAlertType.Information );
                    return;
                }

                gatewayService.Delete( gateway );
                rockContext.SaveChanges();
            }

            BindGrid();
        }

        /// <summary>
        /// Handles the GridRebind event of the rGridGateway control.
        /// </summary>
        /// <param name="sendder">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void rGridGateways_GridRebind( object sender, EventArgs e )
        {
            BindGrid();
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Binds the Gateway list grid.
        /// </summary>
        private void BindGrid()
        {
            using ( var rockContext = new RockContext() )
            {
                var qry = new FinancialGatewayService( rockContext )
                    .Queryable( "EntityType" ).AsNoTracking();

                SortProperty sortProperty = rGridGateway.SortProperty;
                if ( sortProperty != null )
                {
                    qry = qry.Sort( sortProperty );
                }
                else
                {
                    qry = qry.OrderBy( g => g.Name );
                }

                var gateways = qry.ToList();
                nbInactiveWarning.Visible = gateways.Where( g => g.IsActive == false ).Any();

                rGridGateway.DataSource = gateways;
                rGridGateway.DataBind();
            }
        }

        protected string GetComponentDisplayName( object entityTypeObject )
        {
            var entityType = entityTypeObject as EntityType;
            if ( entityType != null )
            {
                var name = string.Empty;
                var gatewayEntityType = EntityTypeCache.Get( entityType.Guid );
                var type = gatewayEntityType.GetEntityType();
                if ( type != null )
                {
                    name = Rock.Reflection.GetDisplayName( type );
                }

                // If it has a DisplayName, use it as is
                if ( !string.IsNullOrWhiteSpace( name ) )
                {
                    return name;
                }
                else
                {
                    // Otherwise use the previous logic with SplitCase on the ComponentName
                    name = Rock.Financial.GatewayContainer.GetComponentName( entityType.Name ).ToStringSafe().SplitCase();
                    return !string.IsNullOrWhiteSpace( name ) ? name : "<span class='label label-danger'>not found</span>";
                }
            }

            return string.Empty;
        }

        #endregion
    }
}