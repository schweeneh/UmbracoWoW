using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace UmbracoWoW.MemberShipAccountSync.MangosAccountManagement
{
    public class MangosConfigSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new MangosConfig();
            config.HostName = section.FirstChild.Attributes["hostname"].Value;
            config.HostPort = section.FirstChild.Attributes["hostport"].Value;
            config.UserName = section.FirstChild.Attributes["username"].Value;
            config.Password = section.FirstChild.Attributes["password"].Value;
            return config;
        }
  
    }

    public class MangosConfig
    {
        public string HostName { get; set; }
        public string HostPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}