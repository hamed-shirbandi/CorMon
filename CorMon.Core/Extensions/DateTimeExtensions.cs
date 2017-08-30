using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Extensions
{
   public static class DateTimeExtensions
    {

        #region Fields


        #endregion

        #region Public Methods



        /// <summary>
        ///
        /// </summary>
        public static string ToPersianFullDateTime(this DateTime dt)
        {
            var pd = new PersianDateTime(dt);
            return pd.ToString("dddd d MMMM yyyy ساعت HH:mm:ss");
        }




        /// <summary>
        ///
        /// </summary>
        public static PersianDateTime ToPersianDateTime(this DateTime dt)
        {
            PersianDateTime persiandate = new PersianDateTime(dt);
            return persiandate;
        }





        /// <summary>
        ///
        /// </summary>
        public static string ToPersianFullDate(this DateTime dt)
        {
            var pd = new PersianDateTime(dt);
            return pd.ToString("dddd d MMMM yyyy");
        }






        /// <summary>
        ///
        /// </summary>
        public static string ToPersianFullDate(this PersianDateTime persiandate)
        {
            return persiandate.ToString("dddd d MMMM yyyy");
        }





        /// <summary>
        ///
        /// </summary>
        public static DateTime ToMiladiDate(this PersianDateTime dt)
        {
            return dt.ToDateTime();
        }



        /// <summary>
        ///
        /// </summary>
        public static string ToMiladiFullDateTime(this DateTime dt)
        {
            return dt.ToString("dddd d MMMM yyyy hh:mm:ss tt");
        }




        /// <summary>
        ///
        /// </summary>
        public static string ToMiladiDigitalFullDateTime(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/d H:mm:ss");
        }



        /// <summary>
        ///
        /// </summary>
        public static string ToPersianDigitalFullDateTime(this PersianDateTime persiandate)
        {
            return persiandate.ToString("yyyy/MM/d H:mm:ss");
        }




        /// <summary>
        ///
        /// </summary>
        public static string ToPersianDigitalFullDateTime(this DateTime dt)
        {
            var pd = new PersianDateTime(dt);
            return pd.ToString("yyyy/MM/d H:mm:ss");
        }




        /// <summary>
        ///
        /// </summary>
        public static string ToMiladiFullDate(this PersianDateTime dt)
        {
            return dt.ToDateTime().ToString("dddd d MMMM yyyy");
        }






        #endregion

    }
}
