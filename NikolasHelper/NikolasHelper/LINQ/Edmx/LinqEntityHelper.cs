using System.Data;
using System.Data.Entity;

namespace NikolasHelper.LINQ.Edmx
{
    public class LinqEntityHelper<U> where U : DbContext, new()
    {


        /// <summary>
        /// 新增一个实体
        /// </summary>
        public static bool AddEntity<T>(T entity) where T : class
        {
            using (U db = new U())
            {
                DbSet set = db.Set<T>();
                set.Add(entity);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 更新一个实体
        /// </summary>
        public static bool UpdateEntity<T>(T entity) where T : class
        {
            using (U db = new U())
            {
                DbSet set = db.Set<T>();
                set.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// 删除一个实体
        /// </summary>
        public static bool DelteEntity<T>(T entity) where T : class
        {
            using (U db = new U())
            {
                DbSet set = db.Set<T>();
                set.Attach(entity);
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}
