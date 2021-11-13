using System;
using System.Collections.Generic;
using FluentAssertions;

namespace csf.main.utils
{

    public class SoftAssert
    {
        private readonly List<Action> _assertions;
        private readonly List<Exception> _exceptions;

        public SoftAssert()
        {
            _assertions = new List<Action>();
            _exceptions = new List<Exception>();
        }

        public static SoftAssert Of(params Action[] assertions)
        {
            var softAssetions = new SoftAssert();
            new List<Action>(assertions).ForEach(assertion => softAssetions.Add(assertion));
            return softAssetions;
        }

        public SoftAssert Add(Action singleAssert)
        {
            _assertions.Add(singleAssert);
            return this;
        }

        public void AssertAll()
        {
            AssertAllAndCollectExceptions();
            _assertions.Clear();
            _exceptions.Count.Should().Equals(0);
         }

        private void AssertAllAndCollectExceptions()
        {
            _assertions.ForEach(assertion =>
            {
                try
                {
                    assertion();
                }
                catch (Exception e)
                {
                    _exceptions.Add(e);
                }
            });
        }

    }
}