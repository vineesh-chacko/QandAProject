using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QandADomainModels;
using QandAViewModels;
using QandAProjectRepository;
using AutoMapper;
using AutoMapper.Configuration;

namespace QandAServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel qvm);
        void UpdateQuestionDetails(EditQuestionViewModel qvm);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionId(int QuestionID, int UserID);
    }
    public class QuestionsService : IQuestionsService
    {
        IQuestionRepository qr;

        public QuestionsService()
        {
            qr = new QuestionsRepository();
        }

        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(qvm);
            qr.InsertQuestion(q);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(qvm);
            qr.UpdateQuestionDetails(q);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVotesCount(qid, value);
        }
        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            qr.UpdateQuestionAnswersCount(qid, value);
        }
        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCount(qid, value);
        }
        public void DeleteQuestion(int qid)
        {
            qr.DeleteQuestion(qid);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> q = qr.GetQuestions();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            return qvm;
        }

        public QuestionViewModel GetQuestionByQuestionId(int QuestionId, int UserId = 0)
        {
            Question q = qr.GetQuestionByQuestionId(QuestionId).FirstOrDefault();
            QuestionViewModel qvm = null;
            if (q != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();

                qvm = mapper.Map<Question, QuestionViewModel>(q);
                if (qvm.Answers.Count > 0)
                foreach (var item in qvm.Answers)
                {
                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp => temp.UserId == UserId).FirstOrDefault();
                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return qvm;
        }
    }
}
