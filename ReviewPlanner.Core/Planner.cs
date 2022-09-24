using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ReviewPlanner.Core
{
    internal class Planner
    {
        /// <summary>
        /// 所有练习的列表
        /// </summary>
        public List<Practice> Practices { get; private set; }
        /// <summary>
        /// 所有今天需要复习的练习列表，返回一个Stack
        /// </summary>
        public Stack<Practice> PracticesToReview
        {
            get
            {
                Stack<Practice> stack = new Stack<Practice>();
                foreach (Practice practice in Practices)
                {
                    if(practice.IsReviewable)
                        stack.Push(practice);
                }
                return stack;
            }
        }

        public Planner(List<Practice> practices)
        {
            Practices = practices;
        }
        /// <summary>
        /// 读取XML数据
        /// </summary>
        /// <param name="path">XML路径</param>
        /// <returns>返回的Planner对象</returns>
        public static Planner ReadFromXML(string path)
        {
            Planner planner;
            using (StreamReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Practice>));
                planner = new((List<Practice>)serializer.Deserialize(reader));
            }
            return planner;
        }
        /// <summary>
        /// 将本Planner类中的练习列表写入文件
        /// </summary>
        /// <param name="path">读取的XML路径</param>
        public void WriteToXML(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Practice>));
                xmlSerializer.Serialize(writer, Practices);
            }
        }
        /// <summary>
        /// 添加新题目
        /// </summary>
        /// <param name="practice">要添加的题目</param>
        public void AddNewPractice(Practice practice)
        {
            Practices.Add(practice);
        }
        /// <summary>
        /// 移除题目
        /// </summary>
        /// <param name="practice">要移除的题目的引用</param>
        public void RemovePractice(Practice practice)
        {
            Practices.Remove(practice);
        }
    }
}
