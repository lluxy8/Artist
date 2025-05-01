using Application.Services;
using Castle.DynamicProxy;
using Core.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;

namespace Application.Interceptors
{
    public class ErrorhandlingInterceptor : IInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public ErrorhandlingInterceptor(
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataFactory = tempDataFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            var tempData = _tempDataFactory.GetTempData(_httpContextAccessor.HttpContext!)
                ?? throw new InvalidOperationException("TempData is not available.");

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                var originalTask = (Task)invocation.ReturnValue!;
                invocation.ReturnValue = InterceptAsync(originalTask, tempData);
                return;
            }

            if (invocation.Method.ReturnType.IsGenericType &&
                invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var task = (Task)invocation.ReturnValue!;
                invocation.ReturnValue = InterceptAsyncGeneric(task, invocation.Method.ReturnType);
                return;
            }

            try
            {
                tempData["ToastSuccess"] = "işlem başarılı.";
            }
            catch (Exception ex)
            {
                var userMessage = ExceptionHelper.Catch(ex);
                tempData["ToastError"] = userMessage;
            }
        }

        private async Task InterceptAsync(Task task, ITempDataDictionary tempData)
        {
            try
            {
                await task;
                tempData["ToastSuccess"] = "işlem başarılı.";
            }
            catch (Exception ex)
            {
                var userMessage = ExceptionHelper.Catch(ex);
                tempData["ToastError"] = userMessage;
            }
        }

        private static Task InterceptAsyncGeneric(Task task, Type returnType)
        {
            return task;
        }
    }



}
