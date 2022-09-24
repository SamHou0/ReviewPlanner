using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewPlanner.Core
{
    internal class Practice
    {
        int reviewTimes=0;

        public TimeSpan ReviewSpan { get; private set; }
        public DateTime LastReviewed;
        /// <summary>
        /// 题目的内容
        /// </summary>
        public string Content = "";
        public bool IsReviewable
        {
            get
            {
                if (DateTime.Today - LastReviewed >= ReviewSpan)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// 立即进行复习
        /// </summary>
        /// <param name="correct">表示复习的题目是否正确</param>
        /// <returns>返回是否完成全部复习。若返回值为true，应立即释放该对象。</returns>
        public bool Review(bool correct)
        {
            if (IsReviewable == false)
                throw new Exception("尝试复习一个不可复习的练习对象");
            if (correct)
                reviewTimes++;
            else
                reviewTimes = 0;
            switch (reviewTimes)
            {
                case 0: ReviewSpan = TimeSpan.FromDays(1); break;
                case 1: ReviewSpan = TimeSpan.FromDays(3); break;
                case 2: ReviewSpan = TimeSpan.FromDays(5); break;
                default: return true;
            }
            LastReviewed = DateTime.Today;
            return false;
        }
    }
}
