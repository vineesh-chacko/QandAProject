using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QandADomainModels;

namespace QandAProjectRepository
{
    public interface IQuestionRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qId, int value);
        void UpdateQuestionAnswersCount(int qId, int value);
        void UpdateQuestionViewsCount(int qId, int value);
        void DeleteQuestion(int qId);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionId(int QuestionId);
    }
    public class QuestionsRepository : IQuestionRepository
    {
        QandADatabaseDBContext db;

        public QuestionsRepository()
        {
            db = new QandADatabaseDBContext();
        }

        public void InsertQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionId == q.QuestionId).FirstOrDefault();
            if (qt != null)
            {
                qt.QuestionName = q.QuestionName;
                qt.QuestionDateAndTime = q.QuestionDateAndTime;
                qt.CategoryId = q.CategoryId;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int qId, int value)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionId == qId).FirstOrDefault();
            if (qt != null)
            {
                qt.VotesCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersCount(int qId, int value)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionId == qId).FirstOrDefault();
            if (qt != null)
            {
                qt.AnswersCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int qId, int value)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionId == qId).FirstOrDefault();
            if (qt != null)
            {
                qt.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void DeleteQuestion(int qId)
        {
            Question qt = db.Questions.Where(temp => temp.QuestionId == qId).FirstOrDefault();
            if (qt != null)
            {
                db.Questions.Remove(qt);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> qt = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return qt;
        }

        public List<Question> GetQuestionByQuestionId(int QuestionId)
        {
            List<Question> qt = db.Questions.Where(temp => temp.QuestionId == QuestionId).ToList();
            return qt;
        }
    }
}
