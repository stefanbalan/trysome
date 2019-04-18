using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OData.UriParser;

namespace ts.OData.Core.WithModel.Infrastructure
{
    public static class Extensions
    {
        public static TKey GetKeyFromUri<TKey>(HttpRequest request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var urlHelper = request.GetUrlHelper() ?? new UrlHelper(request);
            var serviceRoot = urlHelper.CreateODataLink(request.ODataProperties().RouteName, request.GetPathHandler(), new List<ODataPathSegment>());
            var odataPath = request.GetPathHandler().Parse(serviceRoot, uri.LocalPath, request.GetRequestContainer());

            var keySegment = odataPath.Segments.OfType<KeySegment>().FirstOrDefault();
            if (keySegment?.Keys?.FirstOrDefault() == null)
            {
                throw new InvalidOperationException("The link does not contain a key.");
            }

            return (TKey)keySegment.Keys.First().Value;
        }

        //https://stackoverflow.com/a/54111132/1187199

        public static Microsoft.AspNet.OData.Routing.ODataPath CreateODataPath(this Uri uri, HttpRequest request)
        {
            var pathHandler = request.GetPathHandler();
            var serviceRoot = request.GetUrlHelper().CreateODataLink(
                request.ODataFeature().RouteName,
                pathHandler,
                new List<ODataPathSegment>());

            return pathHandler.Parse(serviceRoot, uri.LocalPath, request.GetRequestContainer());
        }

    }

    //public static class ODataHelpers
    //{
    //    public static bool HasProperty(this object instance, string propertyName)
    //    {
    //        var propertyInfo = instance.GetType().GetProperty(propertyName);
    //        return (propertyInfo != null);
    //    }

    //    public static object GetValue(this object instance, string propertyName)
    //    {
    //        var propertyInfo = instance.GetType().GetProperty(propertyName);
    //        if (propertyInfo == null)
    //        {
    //            throw new HttpRequestException("Can't find property with name " + propertyName);
    //        }
    //        var propertyValue = propertyInfo.GetValue(instance, new object[] { });

    //        return propertyValue;
    //    }

    //    public static IActionResult CreateOKHttpActionResult(this ODataController controller, object propertyValue)
    //    {
    //        var okMethod = default(MethodInfo);

    //        // find the ok method on the current controller
    //        var methods = controller.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
    //        foreach (var method in methods)
    //        {
    //            if (method.Name == "Ok" && method.GetParameters().Length == 1)
    //            {
    //                okMethod = method;
    //                break;
    //            }
    //        }

    //        // invoke the method, passing in the propertyValue
    //        okMethod = okMethod.MakeGenericMethod(propertyValue.GetType());
    //        var returnValue = okMethod.Invoke(controller, new object[] { propertyValue });
    //        return (IActionResult)returnValue;
    //    }

    //    public static TKey GetKeyFromUri<TKey>(HttpRequestMessage request, Uri uri)
    //    {
    //        if (uri == null)
    //        {
    //            throw new ArgumentNullException(nameof(uri));
    //        }

    //        var urlHelper = request.GetUrlHelper() ?? new UrlHelper(request);
    //        // Error    CS1061  'HttpRequestMessage' does not contain a definition for 'GetUrlHelper' and no accessible extension method 'GetUrlHelper' accepting a first argument of type 'HttpRequestMessage' could be found (are you missing a using directive or an assembly reference?)

    //        var serviceRoot = urlHelper.CreateODataLink(request.ODataProperties().RouteName, request.GetPathHandler(), new List<ODataPathSegment>());
    //        var odataPath = request.GetPathHandler().Parse(serviceRoot, uri.LocalPath, request.GetRequestContainer());
    //        // Error    CS1061  'HttpRequestMessage' does not contain a definition for 'GetPathHandler' and no accessible extension method 'GetPathHandler' accepting a first argument of type 'HttpRequestMessage' could be found (are you missing a using directive or an assembly reference?)


    //        var keySegment = odataPath.Segments.OfType<KeySegment>().FirstOrDefault();
    //        if (keySegment?.Keys?.FirstOrDefault() == null)
    //        {
    //            throw new InvalidOperationException("The link does not contain a key.");
    //        }

    //        return (TKey)keySegment.Keys.First().Value;
    //    }
    //}
}
