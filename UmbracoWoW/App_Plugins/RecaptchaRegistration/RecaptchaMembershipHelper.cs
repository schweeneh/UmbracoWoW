using Umbraco.Web.Models;
using Umbraco.Web.Security.Providers;
using MPE = global::Umbraco.Core.Security.MembershipProviderExtensions;
using System.Web.Security;
using Umbraco.Core;
using System.Web;
using System;
using Umbraco.Core.Security;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;

namespace UmbracoWoW.App_Plugins.RecaptchaRegistration
{
    public class RecaptchaMembershipHelper
    {
        private readonly MembershipProvider _membershipProvider;
        private readonly RoleProvider _roleProvider;
        private readonly ApplicationContext _applicationContext;
        private readonly HttpContextBase _httpContext;

        #region Constructors
        public RecaptchaMembershipHelper(ApplicationContext applicationContext, HttpContextBase httpContext)
            : this(applicationContext, httpContext, MPE.GetMembersMembershipProvider(), Roles.Enabled ? Roles.Provider : new MembersRoleProvider(applicationContext.Services.MemberService))
        {
        }

        public RecaptchaMembershipHelper(ApplicationContext applicationContext, HttpContextBase httpContext, MembershipProvider membershipProvider, RoleProvider roleProvider)
        {
            if (applicationContext == null) throw new ArgumentNullException("applicationContext");
            if (httpContext == null) throw new ArgumentNullException("httpContext");
            if (membershipProvider == null) throw new ArgumentNullException("membershipProvider");
            if (roleProvider == null) throw new ArgumentNullException("roleProvider");
            _applicationContext = applicationContext;
            _httpContext = httpContext;
            _membershipProvider = membershipProvider;
            _roleProvider = roleProvider;
        }

        public RecaptchaMembershipHelper(Umbraco.Web.UmbracoContext umbracoContext)
            : this(umbracoContext, MPE.GetMembersMembershipProvider(), Roles.Enabled ? Roles.Provider: new MembersRoleProvider(umbracoContext.Application.Services.MemberService))
        {
        }

        public RecaptchaMembershipHelper(Umbraco.Web.UmbracoContext umbracoContext, MembershipProvider membershipProvider, RoleProvider roleProvider)
        {
            if (umbracoContext == null) throw new ArgumentNullException("umbracoContext");
            if (membershipProvider == null) throw new ArgumentNullException("membershipProvider");
            if (roleProvider == null) throw new ArgumentNullException("roleProvider");
            _httpContext = umbracoContext.HttpContext;
            _applicationContext = umbracoContext.Application;
            _membershipProvider = membershipProvider;
            _roleProvider = roleProvider;
        }
        #endregion

        /// <summary>
        /// Creates a model to use for registering new members with custom member properties
        /// </summary>
        /// <param name="memberTypeAlias"></param>
        /// <returns></returns>
        public virtual RegisterModel CreateRegistrationModel(string memberTypeAlias = null)
        {
            var provider = _membershipProvider;
            if (provider.IsUmbracoMembershipProvider())
            {
                memberTypeAlias = memberTypeAlias ?? Constants.Conventions.MemberTypes.DefaultAlias;
                var memberType = _applicationContext.Services.MemberTypeService.Get(memberTypeAlias);
                if (memberType == null)
                    throw new InvalidOperationException("Could not find a member type with alias " + memberTypeAlias);

                var builtIns = Constants.Conventions.Member.GetStandardPropertyTypeStubs().Select(x => x.Key).ToArray();
                var model = RegisterModel.CreateModel();
                model.MemberTypeAlias = memberTypeAlias;
                model.MemberProperties = GetMemberPropertiesViewModel(memberType, builtIns).ToList();
                return model;
            }
            else
            {
                var model = RegisterModel.CreateModel();
                model.MemberTypeAlias = string.Empty;
                return model;
            }
        }

        private IEnumerable<UmbracoProperty> GetMemberPropertiesViewModel(IMemberType memberType, IEnumerable<string> builtIns, IMember member = null)
        {
            var viewProperties = new List<UmbracoProperty>();

            foreach (var prop in memberType.PropertyTypes
                    .Where(x => builtIns.Contains(x.Alias) == false && memberType.MemberCanEditProperty(x.Alias))
                    .OrderBy(p => p.SortOrder))
            {
                var value = string.Empty;
                if (member != null)
                {
                    var propValue = member.Properties[prop.Alias];
                    if (propValue != null && propValue.Value != null)
                    {
                        value = propValue.Value.ToString();
                    }
                }

                var viewProperty = new UmbracoProperty
                {
                    Alias = prop.Alias,
                    Name = prop.Name,
                    Value = value
                };

                //TODO: Perhaps one day we'll ship with our own EditorTempates but for now developers 
                // can just render their own.

                ////This is a rudimentary check to see what data template we should render
                //// if developers want to change the template they can do so dynamically in their views or controllers 
                //// for a given property.
                ////These are the default built-in MVC template types: “Boolean”, “Decimal”, “EmailAddress”, “HiddenInput”, “Html”, “Object”, “String”, “Text”, and “Url”
                //// by default we'll render a text box since we've defined that metadata on the UmbracoProperty.Value property directly.
                //if (prop.DataTypeId == new Guid(Constants.PropertyEditors.TrueFalse))
                //{
                //    viewProperty.EditorTemplate = "UmbracoBoolean";
                //}
                //else
                //{                    
                //    switch (prop.DataTypeDatabaseType)
                //    {
                //        case DataTypeDatabaseType.Integer:
                //            viewProperty.EditorTemplate = "Decimal";
                //            break;
                //        case DataTypeDatabaseType.Ntext:
                //            viewProperty.EditorTemplate = "Text";
                //            break;
                //        case DataTypeDatabaseType.Date:
                //        case DataTypeDatabaseType.Nvarchar:
                //            break;
                //    }
                //}

                viewProperties.Add(viewProperty);
            }
            return viewProperties;
        }
    }
}