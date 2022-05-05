using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoteApp.Common;

namespace NoteApp.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ValidationException))
            {
                var validationException = (ValidationException) context.Exception;
                context.ExceptionHandled = true;

                var json = new JObject();

                var msgs = new JArray();

                foreach (var error in validationException.ValidationErrors)
                {
                    msgs.Add(error);
                }

                foreach (var encodingInfo in Encoding.GetEncodings())
                {
                    
                }

                json.Add(new JProperty("exceptionTrace", msgs));

                context.HttpContext.Response.Body.WriteAsync(Encoding.GetEncoding("windows-1251")
                    .GetBytes(json.ToString()));

                context.HttpContext.Response.StatusCode = 400;
            }
        }
    }
}