﻿using FacetedSearch.Params;

namespace FacetedSearch.Common
{
    public interface IVisitor
    {
        object Visit<T>(T element) where T : class, ISearchOptionsParam;
    }

    public interface IVisitor<in T> : IVisitor where T : class
    {
        object Visit(T element);
    }
}