﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OData.UriParser;

namespace ts.OData.Core.WithModel.Infrastructure
{
    public static class Extensions
    {
        //public static TKey GetKeyFromUri<TKey>(HttpRequestMessage request, Uri uri)
        //{
        //    if (uri == null)
        //    {
        //        throw new ArgumentNullException(nameof(uri));
        //    }

        //    var urlHelper = request.GetUrlHelper() ?? new UrlHelper(request);
        //    var serviceRoot = urlHelper.CreateODataLink(request.ODataProperties().RouteName, request.GetPathHandler(), new List<ODataPathSegment>());
        //    var odataPath = request.GetPathHandler().Parse(serviceRoot, uri.LocalPath, request.GetRequestContainer());

        //    var keySegment = odataPath.Segments.OfType<KeySegment>().FirstOrDefault();
        //    if (keySegment?.Keys?.FirstOrDefault() == null)
        //    {
        //        throw new InvalidOperationException("The link does not contain a key.");
        //    }

        //    return (TKey)keySegment.Keys.First().Value;
        //}
    }
}
