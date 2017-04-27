using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vue.Uco.Model;

namespace Vue.Controllers
{
    public class VueController : BaseController
    {
        Uco.Vue uco = new Uco.Vue();
        //
        // GET: /Vue/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult demo_11_1()
        { 
          return View();
        }

        public void UnitTest()
        {
            Stories data = new Stories();
            data.writer = "jui";
            data.plot = "中央氣象局今天上午9時55分發布大雨特報指出，鋒面通過及東北季風影響，今天南投以北山區、東北部山區及基隆北海岸有局部大雨發生的機率，請注意";

            Result<int> result = uco.Create(data);

            if (result.Success)
            {

            }

            int id = 33;
            Result<int> result1 = uco.Delete(id);

            if (result1.Success)
            {

            }

            id = 40;
            Result<int> result2 = uco.Update_UpVotes(id);

            if (result2.Success)
            {

            }

            data.id = 7;
            Result<int> result3 = uco.Update(data);

            if (result3.Success)
            {

            }

            //PageSize - 一頁幾筆
            Result<StoriesList> result4 = uco.GetStoriesList(1, 10);

            if (result4.Success)
            {

            }

            Result<StoriesList> result5 = uco.GetStoriesList();

            if (result5.Success)
            {

            }
        }

        //新增
        [HttpPost]
        public ActionResult Create(string plot, string writer)
        {
            Stories data = new Stories();
            data.plot = plot;
            data.writer = writer;
            Result<int> result = uco.Create(data);
            return _Json(result);
        }

        //刪除
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Result<int> result = uco.Delete(id);
            return _Json(result);
        }

        //更新票數
        [HttpPost]
        public ActionResult Update_UpVotes(int id)
        {
            Result<int> result = uco.Update_UpVotes(id);
            return _Json(result);
        }

        //更新內容,不包含票數
        [HttpPost]
        public ActionResult Update(int id, string plot, string writer)
        {
            Stories data = new Stories();
            data.plot = plot;
            data.writer = writer;
            data.id = id;
            Result<int> result = uco.Update(data);
            return _Json(result);
        }

        //取得所有內容
        [HttpPost]
        public ActionResult GetStoriesList()
        {
            Result<StoriesList> result = new Result<StoriesList>();
            result = uco.GetStoriesList();
            return _Json(result);
        }

        //分頁內容
        [HttpPost]
        public ActionResult GetStoriesList(int PageIndex, int PageSize)
        {
            Result<StoriesList> result = new Result<StoriesList>();
            result = uco.GetStoriesList(PageIndex, PageSize);
            return _Json(result);
        }
    }
}
