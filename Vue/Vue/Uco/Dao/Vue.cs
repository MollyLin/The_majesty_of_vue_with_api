using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vue.Uco;
using Vue.Uco.Model;
using Vue.lib;
using System.Data.SqlClient;
using System.Data;

namespace Vue.Uco.Dao
{
    public class Vue
    {
        internal int Create(Stories data)
        {
            string sql = @"
--declare
--@plot  nvarchar(500) = 'test',
--@writer nvarchar(80) = 'aaaaaaaaaaaaaaaaaaaaa'

insert into dbo.stories (plot,writer)
values (@plot, @writer)
";
            Sql u = new Sql();
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@plot", data.plot);
                cmd.Parameters.AddWithValue("@writer", data.writer);
                return u.ExecSQL(cmd);
            }
        }

        internal int Delete(int id)
        {
            string sql = @"
--declare @id int =52
delete from dbo.stories where id = @id
";
            Sql u = new Sql();
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return u.ExecSQL(cmd);
            }
        }

        internal int Update_UpVotes(int id)
        {
            string sql = @"
--declare @id int =51
update  dbo.stories set upvotes = isnull(upvotes,0)+ 1  where id = @id
";
            Sql u = new Sql();
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return u.ExecSQL(cmd);
            }
        }

        internal int Update(Stories data)
        {
            string sql = @"
--declare
--@id int =51,
--@plot  nvarchar(500) = 'aaddddddddddddddddddddddddddddd',
--@writer nvarchar(80) = 'aaaaaaaaaaaaaaaaaaaaa'
update  dbo.stories set plot = @plot , writer = @writer where id = @id
";
            Sql u = new Sql();
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", data.id);
                cmd.Parameters.AddWithValue("@writer", data.writer);
                cmd.Parameters.AddWithValue("@plot", data.plot);
                return u.ExecSQL(cmd);
            }
        }

        internal StoriesList GetStoriesList(int PageIndex, int PageSize)
        {
            int start_row = PageIndex == 1 ? 1 : ((PageIndex - 1) * PageSize) + 1;
            int end_row = PageIndex == 1 ? PageSize : start_row + PageSize - 1;
            StoriesList result = new StoriesList();

            string sql = @" 

  --declare
  --@StartRow int = 1
 --,@EndRow int = 10
 --,@PageSize int = 1
 
select * from (
	select row_number() OVER (ORDER BY id) as RowNo,
    id,
	plot,
	upvotes,
	writer
	from dbo.stories a with(nolock)
) X
WHERE X.RowNo BETWEEN @StartRow AND @EndRow 

--總頁數
SELECT ceiling(COUNT(1)*1.0/@PageSize) as PageCount 
FROM dbo.stories with(nolock)
";

            using (SqlCommand cmd = new SqlCommand(sql))
            {
                Sql u = new Sql();
                cmd.Parameters.AddWithValue("@StartRow", start_row);
                cmd.Parameters.AddWithValue("@EndRow", end_row);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                DataSet ds = u.GetDataSet(cmd);
                result.Stories = List.DataTableToList<Stories>(ds.Tables[0]);
            }

            return result;
        }

        internal StoriesList GetStoriesList()
        {
            StoriesList result = new StoriesList();

            string sql = @" 
	                select id,plot,upvotes,writer from dbo.stories a with(nolock) ";

            using (SqlCommand cmd = new SqlCommand(sql))
            {
                Sql u = new Sql();
                DataSet ds = u.GetDataSet(cmd);
                result.Stories = List.DataTableToList<Stories>(ds.Tables[0]);
            }

            return result;
        }
    }
}