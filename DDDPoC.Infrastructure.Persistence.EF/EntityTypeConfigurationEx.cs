using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DelegateDecompiler;

namespace DDDPoC.Infrastructure.Persistence.EF
{
  /// <summary>
  /// Extension methods to allow for IEnumerable in HasKey
  /// </summary>
  internal static class EntityTypeConfigurationEx
  {
    private static readonly EntityFrameworkMappingConfiguration Cfg = new EntityFrameworkMappingConfiguration();

    static EntityTypeConfigurationEx()
    {
      Configuration.Configure(Cfg);
    }

    /// <summary>
    /// Enable IEnumerable nito HasKey
    /// </summary>
    /// <typeparam name="TEntityType">Type of the configured entity</typeparam>
    /// <typeparam name="TTargetEntity">TYpe of the target entity</typeparam>
    /// <param name="c">Current configuration</param>
    /// <param name="navigationPropertyExpression">Expression for the navigation property</param>
    /// <returns>A configuration with the mapping expression with IEnumerable replaced by ICollection</returns>
    public static ManyNavigationPropertyConfiguration<TEntityType, TTargetEntity> HasMany<TEntityType, TTargetEntity>(
        this EntityTypeConfiguration<TEntityType> c,
        Expression<Func<TEntityType, IEnumerable<TTargetEntity>>> navigationPropertyExpression)
      where TTargetEntity : class
      where TEntityType : class
    {
      var body = navigationPropertyExpression.Body;
      var member = (PropertyInfo) ((MemberExpression) body).Member;
      Cfg.RegisterForDecompilation(member);
      
      var decompile = DecompileExpressionVisitor.Decompile(body);
      var convert = Expression.Convert(decompile, typeof(ICollection<TTargetEntity>));
      var expression = Expression.Lambda<Func<TEntityType, ICollection<TTargetEntity>>>(convert, navigationPropertyExpression.Parameters);

      return c.HasMany(expression);
    }

    /// <summary>
    /// Allow use of IEnumerable in .Include
    /// </summary>
    /// <typeparam name="T">Source type</typeparam>
    /// <typeparam name="TEnumerable">Type of enumerable to include</typeparam>
    /// <param name="queryable">Queryable to be updated</param>
    /// <param name="path">Expression for the IEnumerable</param>
    /// <returns>A IQueryable with IEnumerable replaced by ICollection</returns>
    public static IQueryable<T> Include<T, TEnumerable>(this IQueryable<T> queryable, Expression<Func<T, IEnumerable<TEnumerable>>> path)
    {
      var newPath = (Expression<Func<T, IEnumerable<TEnumerable>>>) DecompileExpressionVisitor.Decompile(path);
      return QueryableExtensions.Include(queryable, newPath);
    }
  }
}
