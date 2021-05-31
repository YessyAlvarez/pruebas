using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloWorld
{
    public interface IUrlStringReader
    {
        Task<string> GetJsonAsyncIncorrectUsage(Uri uri);
        Task<string> GetJsonAsyncCorrectUsage(Uri uri);
        Task<HttpResponseMessage> Login(Uri uri);
    }
}