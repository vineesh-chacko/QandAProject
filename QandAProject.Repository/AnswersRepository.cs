using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QandADomainModels;

namespace QandAProjectRepository
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionId(int qid);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersRepository : IAnswersRepository
    {
        QandADatabaseDBContext db;
        IQuestionRepository qr;
        IVotesRepository vr;

        public AnswersRepository()
        {
            db = new QandADatabaseDBContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionId, 1);
        }

        public void UpdateAnswer(Answer a)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerId == a.AnswerId).FirstOrDefault();
            if (ans != null)
            {
                ans.AnswerText = a.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerId == aid).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount += value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(ans.QuestionId, value);
                vr.UpdateVote(aid, uid, value);
            }
        }

        public void DeleteAnswer(int aid)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerId == aid).First();
            if (ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionId, -1);
            }
        }
        public List<Answer> GetAnswersByQuestionId(int qid)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.QuestionId == qid).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return ans;
        }
        public List<Answer> GetAnswersByAnswerID(int aid)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.AnswerId == aid).ToList();
            return ans;
        }
    }
}
