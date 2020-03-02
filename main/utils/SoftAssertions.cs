using System;
using System.Collections.Generic;
using FluentAssertions;

public class SoftAssertions
{
    private readonly List<SingleAssert> _assertions;
    private readonly List<Exception> _exceptions;

    public SoftAssertions()
    {
        _assertions = new List<SingleAssert>();
        _exceptions = new List<Exception>();
    }

    public void Add(SingleAssert singleAssert)
    {
        _assertions.Add(singleAssert);
    }

    private void collectExceptions()
    {
        _assertions.ForEach(assertion =>
        {
            try
            {
                assertion.assert();
            }
            catch (Exception e)
            {
                _exceptions.Add(e);
            }
        });
    }

    public void AssertAll()
    {
        collectExceptions();
        _assertions.Clear();

        _exceptions.Should().BeEmpty();
    }

    public interface SingleAssert
    {
        public void assert();        
    }
}