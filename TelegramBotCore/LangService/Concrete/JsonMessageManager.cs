using Business.LangService.Abstract;
using Core.Aspects.Autofac.Caching;
using Entities.Enums;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace TelegramBotCore.LangService.Concrete;

public class JsonMessageManager : IMessageService
{
    ELang _lang;
    IHostEnvironment _environment;
    List<ResourceFile> _resources = new() { new ResourceFile { Lang = ELang.EN, FileName = "EN_USResources.json" }, new ResourceFile { Lang = ELang.FA, FileName = "FA_IRResources.json" }, new ResourceFile { Lang = ELang.RU, FileName = "RU_RUResources.json" }, new ResourceFile { Lang = ELang.ZH, FileName = "ZH_HNResources.json" } };
    string _fileName;

    public JsonMessageManager(ELang lang, IHostEnvironment environment)
    {
        _lang = lang;
        _environment = environment;
        _fileName = _resources.First(x => x.Lang == _lang).FileName;
    }

    [CacheAspect(43200)]
    public string Get(EMessage message)
    {
        int id = (int)message;
        var resources = JsonConvert.DeserializeObject<List<Resource>>(File.ReadAllText(Path.Combine(_environment.ContentRootPath, "LangResources", "Resources", _fileName)))!;
        return resources.Where(x => x.TypeId == id).Select(x => x.Message).FirstOrDefault(defaultValue: "Lang error");
    }

    [CacheAspect(43200)]
    public async Task<string> GetAsync(EMessage message)
    {
        int id = (int)message;
        var resources = JsonConvert.DeserializeObject<List<Resource>>(await File.ReadAllTextAsync(Path.Combine(_environment.ContentRootPath, "LangResources", "Resources", _fileName)))!;
        return resources.Where(x => x.TypeId == id).Select(x => x.Message).FirstOrDefault(defaultValue: "Lang error");
    }

    [CacheAspect(43200)]
    public string GetByName(string name)
    {
        var types = JsonConvert.DeserializeObject<List<LangType>>(File.ReadAllText(Path.Combine(_environment.ContentRootPath, "LangResources", "types.json")))!;
        int id = types.First(x => x.Name == name).Id;
        var resources = JsonConvert.DeserializeObject<List<Resource>>(File.ReadAllText(Path.Combine(_environment.ContentRootPath, "LangResources", "Resources", _fileName)))!;
        return resources.Where(x => x.TypeId == id).Select(x => x.Message).FirstOrDefault(defaultValue: "Lang error");
    }

    [CacheAspect(43200)]
    public async Task<string> GetByNameAsync(string name)
    {
        var types = JsonConvert.DeserializeObject<List<LangType>>(await File.ReadAllTextAsync(Path.Combine(_environment.ContentRootPath, "LangResources", "types.json")))!;
        int id = types.First(x => x.Name == name).Id;
        var resources = JsonConvert.DeserializeObject<List<Resource>>(await File.ReadAllTextAsync(Path.Combine(_environment.ContentRootPath, "LangResources", "Resources", _fileName)))!;
        return resources.Where(x => x.TypeId == id).Select(x => x.Message).FirstOrDefault(defaultValue: "Lang error");
    }

    struct ResourceFile
    {
        public ELang Lang { get; set; }
        public string FileName { get; set; }
    }

    struct LangType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    struct Resource
    {
        public int TypeId { get; set; }
        public string Message { get; set; }
    }
}
