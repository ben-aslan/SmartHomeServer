﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class BotManager : IBotService
{
    IBotDal _botDal;

    public BotManager(IBotDal botDal)
    {
        _botDal = botDal;
    }

    public Bot GetSelectedBot()
    {
        return _botDal.First(x => x.Selected && x.Active && x.Status);
    }

    [SecuredOperation("admin")]
    public List<Bot> GetActiveBots()
    {
        return _botDal.GetList(x => x.Active && x.Status).ToList();
    }

    [CacheAspect(86400)]
    public string GetTokenByChatId(long chatId)
    {
        return _botDal.Get(x => x.BotChatId == chatId && x.Status).Token;
    }

    public bool IsUploadManager(long botId)
    {
        return _botDal.Any(x => x.BotChatId == botId && x.Active && x.Selected && x.Status);
    }
}
