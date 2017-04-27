using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vue.Uco.Dao;
using Vue.Uco.Model;
namespace Vue.Uco
{
    public class Vue
    {
        Dao.Vue dao = new Dao.Vue();
        #region FB

        public Result<int> Create(Stories data)
        {
            Result<int> result = new Result<int>();

            try
            {
                if (string.IsNullOrEmpty(data.writer.Trim()))
                {
                    throw new Exception("必須填寫姓名");
                }

                if (string.IsNullOrEmpty(data.plot))
                {
                    throw new Exception("必須填寫內容");
                }

                result.Data = dao.Create(data);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMsg = ex.Message;
            }

            return result;
        }

        public Result<int> Delete(int id)
        {
            Result<int> result = new Result<int>();

            try
            {
                result.Data = dao.Delete(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMsg = ex.Message;
            }

            return result;
        }

        public Result<int> Update_UpVotes(int id)
        {
            Result<int> result = new Result<int>();

            try
            {
                result.Data = dao.Update_UpVotes(id);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMsg = ex.Message;
            }

            return result;
        }

        public Result<int> Update(Stories data)
        {
            Result<int> result = new Result<int>();

            try
            {
                if (string.IsNullOrEmpty(data.writer.Trim()))
                {
                    throw new Exception("必須填寫姓名");
                }

                if (string.IsNullOrEmpty(data.plot))
                {
                    throw new Exception("必須填寫內容");
                }

                result.Data = dao.Update(data);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMsg = ex.Message;
            }

            return result;
        }

        public Result<StoriesList> GetStoriesList(int PageIndex, int PageSize)
        {
            Result<StoriesList> result = new Result<StoriesList>();
            try
            {
                result.Success = true;
                result.Data = dao.GetStoriesList(PageIndex, PageSize);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMsg = ex.Message;
            }
            return result;
        }

        public Result<StoriesList> GetStoriesList()
        {
            Result<StoriesList> result = new Result<StoriesList>();
            try
            {
                result.Success = true;
                result.Data = dao.GetStoriesList();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrMsg = ex.Message;
            }
            return result;
        }

        #endregion
    }
}