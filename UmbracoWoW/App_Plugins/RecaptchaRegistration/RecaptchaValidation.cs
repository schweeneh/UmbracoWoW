namespace UmbracoWoW.App_Plugins.RecaptchaRegistration
{
    using System.Collections.Generic;
    using System.Configuration;

    public class RecaptchaValidation
    {
        public bool Success { get; set; }
        public List<string> ErrorCodes { get; set; }

        public static bool Validate(string encodedResponse, string googleSecret)
        {
            if (string.IsNullOrEmpty(encodedResponse)) return false;

            var client = new System.Net.WebClient();
            var secret = googleSecret;

            if (string.IsNullOrEmpty(secret)) return false;

            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var reCaptcha = serializer.Deserialize<RecaptchaValidation>(googleReply);

            return reCaptcha.Success;
        }
    }
}