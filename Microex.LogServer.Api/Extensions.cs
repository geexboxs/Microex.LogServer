using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Microex.LogServer.Api
{
    public static class Extensions
    {
        //public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        //{
        //    return urlHelper.Action(
        //        action: nameof(AccountController.ConfirmEmail),
        //        controller: "Account",
        //        values: new { userId, code },
        //        protocol: scheme);
        //}
        public static string ReadToString(this Stream stream, bool readFromStart = false)
        {
            try
            {
                var curPosition = stream.Position;
                if (readFromStart)
                {
                    stream.Position = 0;
                }
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var result = sr.ReadToEnd();
                    stream.Position = curPosition;
                    return result;
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            //return urlHelper.Action(
            //    action: nameof(AccountController.ResetPassword),
            //    controller: "Account",
            //    values: new { userId, code },
            //    protocol: scheme);
            throw new NotImplementedException();
        }
    }
}
