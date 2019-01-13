using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorMon.Core.Extensions
{
    public static class Utility
    {

        #region Fields

        private readonly static string _urlTitleAllowedChar;


        #endregion
        
        #region Ctor

        static Utility()
        {
            _urlTitleAllowedChar = "ضصثقفغعهخحجچشسیبلاآتنمکگظطزژرذدئوپqwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789-_";

        }


        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public static string ToLowerFirst(this string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }




        /// <summary>
        /// 
        /// </summary>
        public static bool IsNullOrEmptyOrWhiteSpace(this string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }





        /// <summary>
        /// 
        /// </summary>
        public static string GenerateUrlTitle(this string title)
        {
            var selectedChar = title.Trim().ToCharArray().Where(ch=> _urlTitleAllowedChar.Contains(ch)).Select(ch=>ch );
            foreach (var ch in selectedChar)
            {
                title = title.Replace(ch, '-');
            }
            return title;
        }






        #endregion


    }

}
