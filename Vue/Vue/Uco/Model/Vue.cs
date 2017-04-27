using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vue.Uco.Model
{
    public class Vue
    {
    }

    /// <summary>
    /// 所有儲存結果
    /// </summary>
    public class Result<T>
    {
        /// <summary>
        /// 儲存是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 讀寫資料
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrMsg { get; set; }
    }

    public class StoriesList
    {
        public StoriesList() { Stories = new List<Stories>(); }

        public List<Stories> Stories { get; set; }
    }

    public class Stories
    {
        public int id { get; set; }

        public string plot { get; set; }

        public int upvotes { get; set; }

        public string writer { get; set; }
    }


}