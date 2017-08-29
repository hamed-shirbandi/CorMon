using CorMon.Core.Enums;
using System;
using System.Collections.Generic;

namespace CorMon.Core.Domain
{
    public class Post: BaseEntity
    {

        /// <summary>
        /// سطح فنی مطلب
        /// </summary>
        public PostLevel PostLevel { get; set; }



        /// <summary>
        /// عنوان مطلب
        /// </summary>
        public string Title { get; set; }




        /// <summary>
        /// نویسنده - نام نمایشی کاربر ارسال کننده
        /// </summary>
        public string Author { get; set; }




        /// <summary>
        /// نمایش عنوان در آدرس بار
        /// </summary>
        public string UrlTitle { get; set; }




        /// <summary>
        /// محتوای مطلب
        /// </summary>
        public string Content { get; set; }





        /// <summary>
        /// تاریخ ایجاد مطلب
        /// </summary>
        public DateTime CreateDateTime { get; set; }




        /// <summary>
        /// تاریخ آخرین ویرایش مطلب
        /// </summary>
        public DateTime ModifiedDateTime { get; set; }



        /// <summary>
        /// توصیف مطلب برای سئو
        /// </summary>
        public string MetaDescription { get; set; }



        /// <summary>
        /// کلید واژه های مطلب برای سئو
        /// </summary>
        public string MetaKeyWords { get; set; }



        /// <summary>
        /// وضعیت مجوز موتورهای جستجو
        /// </summary>
        public RobotsState MetaRobots { get; set; }




        /// <summary>
        ///وضعیت انتشار مطلب ...
        /// </summary>
        public PublishStatus PublishStatus { get; set; }




        /// <summary>
        ///تاریخ انتشار مطلب
        /// </summary>
        public DateTime PublishDateTime { get; set; }




        /// <summary>
        /// تعداد مشاهده شدن های مطلب
        /// </summary>
        public int VisitCount { get; set; }




        /// <summary>
        /// زباله 
        /// </summary> 
        public bool IsDeleted { get; set; }

        


   

        /// <summary>
        /// کلید  کاربر
        /// </summary>
        public string UserId { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public string[] TaxonomyIds { get; set; }

    }
}
