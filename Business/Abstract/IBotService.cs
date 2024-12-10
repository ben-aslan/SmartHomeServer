using Entities.Concrete;

namespace Business.Abstract;

public interface IBotService
{
    Bot GetSelectedBot();
    List<Bot> GetActiveBots();
    string GetTokenByChatId(long chatId);
    bool IsUploadManager(long botId);
}
