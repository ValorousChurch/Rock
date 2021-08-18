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
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

using Rock.Communication;
using Rock.Web.Cache;

namespace Rock.Web.UI.Controls
{
    /// <summary>
    /// Field used to save and display an email address with optional confirmation
    /// </summary>
    /// <seealso cref="System.Web.UI.WebControls.CompositeControl" />
    /// <seealso cref="Rock.Web.UI.Controls.IRockControl" />
    [ToolboxData( "<{0}:EmailBox runat=server></{0}:EmailBox>" )]
    public class EmailBox : CompositeControl, IRockControl
    {
        #region Configuration

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EmailBox"/> will require confirmation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if confirmation is required; otherwise, <c>false</c>.
        /// </value>
        public bool EnableConfirmation
        {
            get
            {
                return this.ViewState["EnableConfirmation"] as bool? ?? false;
            }

            set
            {
                this.ViewState["EnableConfirmation"] = ConfirmationAllowed() ? value : false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple email addresses (comma-delimited)
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow multiple]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowMultiple
        {
            get
            {
                return this.ViewState["AllowMultiple"] as bool? ?? false;
            }

            set
            {
                this.ViewState["AllowMultiple"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow lava
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow lava]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowLava
        {
            get
            {
                return this.ViewState["AllowLava"] as bool? ?? false;
            }

            set
            {
                this.ViewState["AllowLava"] = value;
            }
        }

        #endregion

        #region IRockControl Implementation

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>
        /// The label text.
        /// </value>
        [Bindable( true )]
        [Category( "Appearance" )]
        [DefaultValue( "" )]
        [Description( "The text for the label." )]
        public string Label
        {
            get
            {
                return ViewState["Label"] as string ?? string.Empty;
            }

            set
            {
                ViewState["Label"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the help text.
        /// </summary>
        /// <value>
        /// The help text.
        /// </value>
        [Bindable( true )]
        [Category( "Appearance" )]
        [DefaultValue( "" )]
        [Description( "The help block." )]
        public string Help
        {
            get
            {
                return HelpBlock != null ? HelpBlock.Text : string.Empty;
            }

            set
            {
                if ( HelpBlock != null )
                {
                    HelpBlock.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the warning text.
        /// </summary>
        /// <value>
        /// The warning text.
        /// </value>
        [Bindable( true )]
        [Category( "Appearance" )]
        [DefaultValue( "" )]
        [Description( "The warning block." )]
        public string Warning
        {
            get
            {
                return WarningBlock != null ? WarningBlock.Text : string.Empty;
            }

            set
            {
                if ( WarningBlock != null )
                {
                    WarningBlock.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        [Bindable( true )]
        [Category( "Behavior" )]
        [DefaultValue( "false" )]
        [Description( "Is the value required?" )]
        public bool Required
        {
            get
            {
                return ViewState["Required"] as bool? ?? false;
            }

            set
            {
                ViewState["Required"] = value;

                if ( _ebPrimary != null )
                {
                    _ebPrimary.Required = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the required error message.  If blank, the LabelName name will be used
        /// </summary>
        /// <value>The required error message.</value>
        public string RequiredErrorMessage
        {
            get
            {
                return RequiredFieldValidator != null ? RequiredFieldValidator.ErrorMessage : string.Empty;
            }

            set
            {
                if ( RequiredFieldValidator != null )
                {
                    RequiredFieldValidator.ErrorMessage = value;

                    if ( _ebPrimary != null )
                    {
                        _ebPrimary.RequiredErrorMessage = value;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                EnsureChildControls();
                bool valid = ( !Required || RequiredFieldValidator == null || RequiredFieldValidator.IsValid ) && _ebPrimary.IsValid;

                if ( this.EnableConfirmation )
                {
                    valid = valid && ( _compareValidator == null || _compareValidator.IsValid ) && _ebConfirm.IsValid;
                }

                return valid;
            }
        }

        /// <summary>
        /// Gets or sets the form group class.
        /// </summary>
        /// <value>
        /// The form group class.
        /// </value>
        public string FormGroupCssClass
        {
            get
            {
                return ViewState["FormGroupCssClass"] as string ?? string.Empty;
            }

            set
            {
                ViewState["FormGroupCssClass"] = value;
            }
        }

        /// <summary>
        /// Gets the help block.
        /// </summary>
        /// <value>
        /// The help block.
        /// </value>
        public HelpBlock HelpBlock { get; set; }

        /// <summary>
        /// Gets the warning block.
        /// </summary>
        /// <value>
        /// The warning block.
        /// </value>
        public WarningBlock WarningBlock { get; set; }

        /// <summary>
        /// Gets the required filed validator
        /// </summary>
        /// <value>
        /// The required field validator
        /// </value>
        public RequiredFieldValidator RequiredFieldValidator { get; set;  }

        private RegularExpressionValidator _regexValidator { get; set; }

        private CustomValidator _compareValidator { get; set; }

        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>
        /// The validation group.
        /// </value>
        public string ValidationGroup
        {
            get
            {
                return ViewState["ValidationGroup"] as string;
            }

            set
            {
                ViewState["ValidationGroup"] = value;

                EnsureChildControls();

                if ( RequiredFieldValidator != null )
                {
                    RequiredFieldValidator.ValidationGroup = value;
                }

                _regexValidator.ValidationGroup = value;
                _ebPrimary.ValidationGroup = value;

                if ( this.EnableConfirmation )
                {
                    _ebConfirm.ValidationGroup = value;
                    _compareValidator.ValidationGroup = value;
                }
            }
        }

        /// <summary>
        /// This is where you implement the simple aspects of rendering your control.  The rest
        /// will be handled by calling RenderControlHelper's RenderControl() method.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void RenderBaseControl(HtmlTextWriter writer)
        {
            writer.AddAttribute( "id", this.ClientID );
            writer.AddAttribute( "class", "js-emailControl " + this.CssClass );

            writer.RenderBeginTag( HtmlTextWriterTag.Div );

            writer.AddAttribute( "class", "form-group" );
            writer.RenderBeginTag( HtmlTextWriterTag.Div );
            _ebPrimary.RenderControl( writer );
            writer.RenderEndTag();  // div.form-group

            if ( this.EnableConfirmation )
            {
                writer.AddAttribute( "class", "form-group" );
                writer.RenderBeginTag( HtmlTextWriterTag.Div );
                _ebConfirm.RenderControl( writer );
                writer.RenderEndTag();  // div.form-group

                _compareValidator.RenderControl( writer );
            }

            _regexValidator.RenderControl( writer );

            writer.RenderEndTag(); // div#ClientId
        }

        #endregion IRockControl Implementation

        #region Constructors

        /// <summary>
        /// Initialize a new instance of the <see cref="EmailBox"/> class.
        /// </summary>
        public EmailBox() : base()
        {
            HelpBlock = new HelpBlock();
            WarningBlock = new WarningBlock();
        }

        #endregion

        /// <summary>
        /// The message to show if validation fails
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// The value of the placeholder 
        /// </summary>
        public string Placeholder
        {
            get
            {
                EnsureChildControls();
                return _ebPrimary.Placeholder ?? string.Empty;
            }

            set
            {
                EnsureChildControls();
                _ebPrimary.Placeholder = value;

                if ( this.EnableConfirmation )
                {
                    _ebConfirm.Placeholder = "Confirm " + value;
                }
            }
        }

        /// <summary>
         /// The value of the text 
         /// </summary>
        public string Text {
            get
            {
                EnsureChildControls();
                return _ebPrimary.Text ?? string.Empty;
            }

            set
            {
                EnsureChildControls();
                _ebPrimary.Text = value;

                if ( this.EnableConfirmation )
                {
                    _ebConfirm.Text = value;
                }
            }
        }

        private RockTextBox _ebPrimary;
        private RockTextBox _ebConfirm;

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad( e );

            EnsureChildControls();
            this.EnableConfirmation = ConfirmationAllowed() ? this.EnableConfirmation : false;
        }

        /// <summary>
        /// Called just before rendering begins on the page.
        /// </summary>
        /// <param name="e">The EventArgs that describe this event.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender( e );
        }

        /// <summary>
        /// Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
        public override void RenderControl(HtmlTextWriter writer)
        {
            if ( this.Visible )
            {
                RockControlHelper.RenderControl( this, writer );
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Clear();
            RockControlHelper.CreateChildControls( this, Controls );

            _ebPrimary = new RockTextBox
            {
                ID = "ebPrimary",
                CssClass = "js-primary",
                Label = string.Empty,
                PrependText = "<i class='fa fa-envelope'></i>",
                Required = this.Required,
                RequiredErrorMessage = this.RequiredErrorMessage.IsNullOrWhiteSpace() ? $"{this.Label} is required." : this.RequiredErrorMessage,
                ValidationGroup = this.ValidationGroup
            };
            Controls.Add( _ebPrimary );

            _regexValidator = new RegularExpressionValidator
            {
                ID = "rev",
                ControlToValidate = this._ebPrimary.ID,
                CssClass = "validation-error help-inline",
                Display = ValidatorDisplay.Dynamic,
                ErrorMessage = "Email address is not valid",
                ValidationExpression = EmailAddressFieldValidator.GetRegularExpression( this.AllowMultiple, this.AllowLava ),
                ValidationGroup = this.ValidationGroup
            };
            Controls.Add( _regexValidator );

            if ( this.EnableConfirmation )
            {
                _ebConfirm = new RockTextBox
                {
                    ID = "ebConfirm",
                    CssClass = "js-confirm",
                    Label = string.Empty,
                    PrependText = "<i class='fa fa-envelope'></i>",
                    Required = false,
                    ValidationGroup = this.ValidationGroup
                };
                Controls.Add( _ebConfirm );

                _ebPrimary.Attributes["Placeholder"] = this.Placeholder.IsNullOrWhiteSpace() ? "Email" : this.Placeholder;
                _ebConfirm.Attributes["placeholder"] = $"Confirm { _ebPrimary.Attributes["Placeholder"] }";

                _compareValidator = new CustomValidator
                {
                    ID = "cv",
                    ClientValidationFunction = "Rock.controls.emailControl.clientValidate",
                    ControlToValidate = this._ebConfirm.ID,
                    CssClass = "validation-error help-inline",
                    Display = ValidatorDisplay.Dynamic,
                    Enabled = true,
                    ErrorMessage = "Email and confirmation do not match.",
                    ValidateEmptyText = true,
                    ValidationGroup = this.ValidationGroup
                };
                _compareValidator.ServerValidate += _CustomValidator_ServerValidate;
                Controls.Add( _compareValidator );
            }
        }

        private void _CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ( ! this._ebPrimary.Text.Equals( this._ebConfirm.Text ) )
            {
                var _customValidator = source as CustomValidator;
                _customValidator.ErrorMessage = "Email and confirmation do not match.";

                args.IsValid = false;
            }
        }

        /// <summary>
        /// Checks the global attribute (killswitch) to verify that email confirmation is allowed
        /// </summary>
        /// <returns>
        ///   <c>false</c> if confirmation is globally disabled; otherwise, <c>true</c>.
        /// </returns>
        protected internal static bool ConfirmationAllowed()
        {
            var killswitchEnabled = SystemSettings.GetValue( SystemKey.SystemSetting.DISABLE_EMAIL_CONFIRMATION );

            if ( killswitchEnabled.AsBoolean() )
            {
                return false;
            }

            return true;
        }
    }
}