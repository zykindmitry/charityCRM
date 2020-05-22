using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace DevFactoryZ.CharityCRM.UI.Web.Filters
{
    public class CategoryFilterAttribute : Attribute, IActionFilter
    {
        string categoryKeyName;

        public CategoryFilterAttribute(string categoryKeyName)
        {
            this.categoryKeyName = categoryKeyName;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid)
            {
                if (context.HttpContext.Request.Query.TryGetValue(
                    categoryKeyName, 
                    out StringValues categoryIds))
                {
                    if (context.HttpContext.Response.Body != null)
                    {
                        context.HttpContext.Response.Body = 
                            new CategoryFilterStream(context.HttpContext.Response.Body, categoryIds);
                    }
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        private class CategoryFilterStream : Stream
        {
            private readonly Stream outputStream;
            StringValues categoryIds;

            public CategoryFilterStream(Stream filterStream, StringValues categoryIds)
            {
                if (filterStream == null)
                {
                    throw new ArgumentNullException(nameof(filterStream));
                }

                this.categoryIds = categoryIds;

                outputStream = filterStream;
            }

            public override async Task WriteAsync(
                byte[] buffer, 
                int offset, 
                int count, 
                CancellationToken cancellationToken)
            {
                var result = await buffer.FromJsonAsync<WardListViewModel[]>();

                result = result.Where(w => 
                    w.WardCategories.Any(c => 
                        categoryIds.Contains(c.Id.ToString()))).ToArray();

                buffer = result.ToJson();

                await outputStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override bool CanRead { get { return false; } }
            
            public override bool CanSeek { get { return false; } }
            
            public override bool CanWrite { get { return true; } }
            
            public override long Length { get { throw new NotSupportedException(); } }
            
            public override long Position
            {
                get { throw new NotSupportedException(); }
                set { throw new NotSupportedException(); }
            }

            public override void Flush()
            {
                outputStream.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }
        }
    }
}
