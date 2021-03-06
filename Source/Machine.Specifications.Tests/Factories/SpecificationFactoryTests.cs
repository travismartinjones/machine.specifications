using System;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Machine.Specifications.Model;
using Machine.Specifications.Sdk;
using Machine.Specifications.Utility;
using Machine.Testing;
using NUnit.Framework;

namespace Machine.Specifications.Factories
{
  public class SpecificationFactory_CreateSpecification_FailureTests : TestsFor<SpecificationFactory>
  {

  }

  [TestFixture]
  public class SpecificationFactory_CreateSpecification : WithSingleSpecification
  {
    [Test]
    public void ShouldHaveCorrectItClause()
    {
      specification.Name.Should().Be("is a specification");
    }

    [Test]
    public void ShouldHaveFieldInfo()
    {
      specification.FieldInfo.Name.Should().Be("is_a_specification");
    }
  }

  [TestFixture]
  public class SpecificationFactory_CreateThrowSpecification : WithThrowSpecification
  {
    [Test]
    public void ShouldHaveCorrectItClause()
    {
      specification.Name.Should().Be("should throw an exception");
    }
  }

  [TestFixture]
  public  class SpecificationFactory_CreateUndefinedSpecification : WithUndefinedSpecification
  {
    [Test]
    public void ShouldCreateUnknownSpecification()
    {
      specification.IsDefined.Should().BeFalse();
    }
  }

  [TestFixture]
  public class SpecificationFactory_NestedSpecifications : WithNestedSpecification
  {
    [Test]
    public void ShouldHaveCorrectItClause()
    {        
        specification.Name.Should().Be("is a child specification");
    }

    [Test]
    public void TheContextNameShouldBePrependedWithTheBaseClassName()
    {
        context.Name.Should().Be("when a specification has a child and that child has a test");
    }
  }

  public class WithUndefinedSpecification : TestsFor<SpecificationFactory>
  {
    protected Specification specification;
    public override void BeforeEachTest()
    {
      Type type = typeof(ContextWithEmptySpecification);
      FieldInfo field = type.GetInstanceFieldsOfUsage(new AssertDelegateAttributeFullName()).First();
      ContextFactory factory = new ContextFactory();
      var context = factory.CreateContextFrom(new ContextWithEmptySpecification());

      specification = Target.CreateSpecification(context, field);
    }
  }

  public class WithSingleSpecification : TestsFor<SpecificationFactory>
  {
    protected Specification specification;
    public override void BeforeEachTest()
    {
      Type type = typeof(ContextWithSingleSpecification);
      FieldInfo field = type.GetInstanceFieldsOfUsage(new AssertDelegateAttributeFullName()).First();
      ContextFactory factory = new ContextFactory();
      var context = factory.CreateContextFrom(new ContextWithSingleSpecification());

      specification = Target.CreateSpecification(context, field);
    }
  }

  public class WithThrowSpecification : TestsFor<SpecificationFactory>
  {
    protected Specification specification;
    public override void BeforeEachTest()
    {
      Type type = typeof(ContextWithThrowingSpecification);
      FieldInfo field = type.GetInstanceFieldsOfUsage(new AssertDelegateAttributeFullName()).First();
      ContextFactory factory = new ContextFactory();
      var context = factory.CreateContextFrom(new ContextWithThrowingSpecification());

      specification = Target.CreateSpecification(context, field);
    }
  }

  public class WithNestedSpecification : TestsFor<SpecificationFactory>
  {
    protected Context context;
    protected Specification specification;
    public override void BeforeEachTest()
    {
      Type type = typeof(when_a_specification_has_a_child.and_that_child_has_a_test);
      FieldInfo field = type.GetInstanceFieldsOfUsage(new AssertDelegateAttributeFullName()).First();
      ContextFactory factory = new ContextFactory();
      context = factory.CreateContextFrom(new when_a_specification_has_a_child.and_that_child_has_a_test());

      specification = Target.CreateSpecification(context, field);
    }
  }

}
