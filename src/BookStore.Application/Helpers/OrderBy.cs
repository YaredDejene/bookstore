using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BookStore.Application.Helpers
{
    public static class OrderBy<T>
    {
        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy(string columnName, bool reverse)
        {
            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = System.Linq.Expressions.Expression.Parameter(typeQueryable, "p");
            var outerExpression = System.Linq.Expressions.Expression.Lambda(argQueryable, argQueryable);
            
            IQueryable<T> query = new List<T>().AsQueryable<T>();
            var entityType = typeof(T);
            ParameterExpression arg = System.Linq.Expressions.Expression.Parameter(entityType, "x");

            Expression expression = arg;
            string[] properties = columnName.Split('.');
            foreach (string propertyName in properties)
            {
                PropertyInfo propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expression = System.Linq.Expressions.Expression.Property(expression, propertyInfo);
                entityType = propertyInfo.PropertyType;
            }
            LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda(expression, arg);
            string methodName = reverse ? "OrderByDescending" : "OrderBy";

            MethodCallExpression resultExp = System.Linq.Expressions.Expression.Call(typeof(Queryable), 
                                                                                     methodName, 
                                                                                     new Type[] { typeof(T), entityType }, 
                                                                                     outerExpression.Body, 
                                                                                     System.Linq.Expressions.Expression.Quote(lambda));

            var finalLambda = System.Linq.Expressions.Expression.Lambda(resultExp, argQueryable);

            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }
    }
}