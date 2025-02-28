﻿<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" Inherits="Rock.Web.UI.RockPage" %>

<asp:Content ID="ctMain" ContentPlaceHolderID="main" runat="server">
    <!-- Ajax Error -->
    <div class="alert alert-danger ajax-error no-index" style="display:none">
        <p><strong>Error</strong></p>
        <span class="ajax-error-message"></span>
    </div>

    <div class="hero-split container-fluid">
        <div class="row" style="min-height: 110vh;">
            <div class="col-lg-4 col-md-8 col-sm-12 py-5 m-auto">
                <Rock:Zone Name="Headline" CssClass="zone-headline" runat="server" />
                <Rock:Zone Name="CTA Buttons" CssClass="mt-5" runat="server" />
                <Rock:Zone Name="Section A" runat="server" />
            </div>
            <Rock:Lava ID="HeaderImage" runat="server">
                {%- assign headerImageId = CurrentPage | Attribute:'HeaderImage','Id' -%}
                {%- if headerImageId != '' -%}
                <div class="col-lg-6 col-md-12" style="background: url('{{ headerImageId | ImageUrl:'','rootUrl' }}&maxWidth=2500') center center no-repeat; background-size: cover; min-height: 100vh;"></div>
                {%- else -%}
                <div class="col-lg-6 col-md-12"></div>
                {%- endif -%}
            </Rock:Lava>

        </div>
    </div>


    <div class="section-3col">
        <div class="container">
            <div class="row">
                <Rock:Zone Name="Section B" CssClass="col-md-4 py-sm-5 py-2 pt-5" runat="server" />
                <Rock:Zone Name="Section C" CssClass="col-md-4 py-sm-5 py-2" runat="server" />
                <Rock:Zone Name="Section D" CssClass="col-md-4 py-sm-5 py-2 pb-5" runat="server" />
            </div>
        </div>
    </div>

    <div class="container d-flex flex-column py-5">
        <div class="row my-auto my-5 pb-2">
            <Rock:Zone Name="Main" CssClass="pop-content col-lg-8 col-md-10 col-sm-12 mx-auto" runat="server" />
        </div>

        <div class="row my-auto my-5 pb-2">
            <Rock:Zone Name="Extra" CssClass="col-lg-8 col-md-10 col-sm-12 mx-auto" runat="server" />
        </div>
    </div>

    <Rock:Lava ID="SecondaryImage" runat="server">
        {%- assign secondaryImageId = CurrentPage | Attribute:'SecondaryImage','Id' -%}
        {%- if secondaryImageId != '' -%}
        <div class="secondary-hero py-5" style="background: linear-gradient(90deg, var(--secondary-hero-overlay-color, rgba(0,0,0,0)), var(--secondary-hero-overlay-color, rgba(0,0,0,0))),url('{{ secondaryImageId | ImageUrl: '', 'rootUrl' }}&maxWidth=2500')  center center; background-size: cover;">
        {%- else -%}
        <div class="secondary-hero py-5" style="background: linear-gradient(90deg, var(--secondary-hero-overlay-color, rgba(0,0,0,0)), var(--secondary-hero-overlay-color, rgba(0,0,0,0))),url('https://images.unsplash.com/photo-1520512533001-af75c194690b?ixlib=rb-0.3.5&ixid=eyJhcHBfaWQiOjEyMDd9&s=d23a0082e9aa3caa886db02d419bdd3d&auto=format&fit=crop&w=2500&q=80&auto=enhance') center center; background-size: cover;">
        {%- endif -%}
    </Rock:Lava>
        <div class="container d-flex flex-column" style="height: 95vh; max-height: 563px;">
            <div class="row my-auto">
                <Rock:Zone Name="Secondary Hero" CssClass="col-lg-4 col-md-8 col-sm-12 py-5 mr-auto text-left" runat="server" />
            </div>
        </div>
    </div>

    <footer class="container">
        <div class="row">
            <div class="col-md-12">
                <Rock:Zone Name="Footer" runat="server" />
            </div>
        </div>
    </footer>


    <!-- Modal -->
    <div class="workflow-modal modal fade" id="workflowModal" tabindex="-1" role="dialog" aria-labelledby="workflowModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <Rock:Zone Name="Workflow" CssClass="zone-workflow" runat="server" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

