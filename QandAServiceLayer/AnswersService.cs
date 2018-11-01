using System;
using System.Collections.Generic;
using System.Linq;
using QandADomainModels;
using QandAViewModels;
using QandAProjectRepository;
using AutoMapper;
using AutoMapper.Configuration;

namespace QandAServiceLayer
{
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<AnswerViewModel> GetAnswersByQuestionId(int qid);
        AnswerViewModel GetAnswerByAnswerId(int AnswerId);
    }
    public class AnswersService : IAnswersService
    {
        IAnswersRepository ar;

        public AnswersService()
        {
            ar = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);
            ar.InsertAnswer(a);
        }
        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);
            ar.UpdateAnswer(a);
        }
        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            ar.UpdateAnswerVotesCount(aid, uid, value);
        }
        public void DeleteAnswer(int aid)
        {
            ar.DeleteAnswer(aid);
        }

        public List<AnswerViewModel> GetAnswersByQuestionId(int qid)
        {
            List<Answer> a = ar.GetAnswersByQuestionId(qid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            return avm;
        }

        public AnswerViewModel GetAnswerByAnswerId(int AnswerId)
        {
            Answer a = ar.GetAnswersByAnswerId(AnswerId).FirstOrDefault();
            AnswerViewModel avm = null;
            if (a != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(a);
            }
            return avm;
        }
    }
}


