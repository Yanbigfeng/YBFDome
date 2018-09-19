using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Collections;

namespace Model.Cache
{
    public class CacheHelper
    {
        //private static System.Web.Caching.Cache ObjCache = System.Web.HttpRuntime.Cache;
        //使用上下文统一方法实例化
        private static System.Web.Caching.Cache ObjCache = System.Web.HttpContext.Current.Cache;



        #region Exist方法
        /// <summary>  
        /// 判断指定Key的缓存是否存在  
        /// </summary>  
        /// <param name="Key"></param>  
        /// <returns></returns>  
        public static bool Exist(string Key)
        {
            if (ObjCache[Key] == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region  Get方法
        /// <summary>  
        /// 获得指定Key的缓存对象  
        /// </summary>  
        /// <param name="Key"></param>  
        /// <returns></returns>  
        public static Object Get(string Key)
        {
            object objkey = null;
            if (ObjCache[Key] != null)
            {
                objkey = ObjCache.Get(Key);
            }
            return objkey;
        }
        #endregion

        #region Set方法
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="Key">主键</param>
        /// <param name="obj">键值</param>
        /// <param name="dp">依赖项</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="EndTime">过期删除时间</param>
        /// <param name="Priority">优先级</param>
        /// <param name="CallBack">回调函数</param>
        public static void Set(string Key, object obj, CacheDependency dp, DateTime expiry, TimeSpan EndTime, CacheItemPriority Priority, CacheItemRemovedCallback CallBack)
        {
            if (ObjCache[Key] != null)
            {
                ObjCache.Remove(Key);
            }

            ObjCache.Add(Key, obj, dp, expiry, EndTime, Priority, CallBack);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="Key">主键</param>
        /// <param name="obj">键值</param>
        /// <param name="dp">依赖项</param>
        public static void Set(string Key, object obj, CacheDependency dp)
        {
            if (ObjCache[Key] != null)
            {
                ObjCache.Remove(Key);
            }
            ObjCache.Insert(Key, obj, dp);
        }
        public static void Set(string Key, object obj)
        {
            if (ObjCache[Key] != null)
            {
                ObjCache.Remove(Key);
            }
            ObjCache.Insert(Key, obj);
        }
        /// <summary>  
        /// 设置缓存  
        /// </summary>  
        /// <param name="Key">Cache Key</param>  
        /// <param name="expiry">缓存时间</param>  
        /// <param name="obj">缓存对象</param>  
        public static void Set(string Key, object obj, DateTime expiry)
        {
            if (ObjCache[Key] != null)
            {
                ObjCache.Remove(Key);
            }

            ObjCache.Insert(Key, obj, null, expiry, TimeSpan.Zero);
        }

        /// <summary>  
        /// 设置缓存  
        /// </summary>  
        /// <param name="Key">Cache Key</param>  
        /// <param name="min">缓存时间【分钟】</param>  
        /// <param name="obj">缓存对象</param>  
        public static void Set(string Key, int min, object obj)
        {
            double douNum = double.Parse(min.ToString());
            Set(Key, obj, DateTime.Now.AddMinutes(douNum));
        }
        #endregion

        #region Del方法

        /// <summary>  
        /// 删除指定Key的缓存  
        /// </summary>  
        /// <param name="Key"></param>  
        public static void Del(string Key)
        {
            if (ObjCache[Key] != null)
            {
                ObjCache.Remove(Key);
            }
        }
        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public static void Del()
        {
            IDictionaryEnumerator DicCache = ObjCache.GetEnumerator();
            int count = ObjCache.Count;
            for (int i = 0; i < count; i++)
            {
                DicCache.MoveNext();
                ObjCache.Remove(DicCache.Key.ToString());
            }
        }

        #endregion

        #region 其他

        /// <summary>  
        /// 获取缓存中的项数  
        /// </summary>  
        public static int Count
        {
            get
            {
                return ObjCache.Count;
            }
        }

        /// <summary>  
        /// 获取可用于缓存的千字节数  
        /// </summary>  
        public static long PrivateBytes
        {
            get
            {
                return ObjCache.EffectivePrivateBytesLimit;
            }
        }
        #endregion
    }
}
