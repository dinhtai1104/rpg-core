using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.Shared.Pool.Container
{
    /// <summary>
    /// Linked list object pool: storing related objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ListPool<T>
    {
        /// <summary>
        /// Stack object: storing multiple lists
        /// </summary>
        static Stack<List<T>> mListStack = new Stack<List<T>>(8);

        /// <summary>
        /// Pop the stack: Get a List object
        /// </summary>
        /// <returns></returns>
        public static List<T> Get()
        {
            if (mListStack.Count == 0)
            {
                return new List<T>(8);
            }

            return mListStack.Pop();
        }

        /// <summary>
        /// Push: Add the List object to the stack
        /// </summary>
        /// <param name="toRelease"></param>
        public static void Release(List<T> toRelease)
        {
            toRelease.Clear();
            mListStack.Push(toRelease);
        }
    }

    /// <summary>
    /// Linked list object pool extension method class
    /// </summary>
    public static class ListPoolExtensions
    {
        /// <summary>
        /// Extend the method of stacking List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toRelease"></param>
        public static void Release2Pool<T>(this List<T> toRelease)
        {
            ListPool<T>.Release(toRelease);
        }
    }

}
