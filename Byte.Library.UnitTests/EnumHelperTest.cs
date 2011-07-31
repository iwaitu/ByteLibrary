using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Byte.Library.UnitTests
{
    public class EnumHelperTest
    {
        enum TestEnum
        {
            Alpha,
            Beta,
            Charlie
        };

        [Fact]
        public void GetValues_enumerates_all_possible_enum_values()
        {
            IEnumerable<TestEnum> enumValues = EnumHelper.GetValues<TestEnum>();
            Assert.True(enumValues.All(value => Enum.IsDefined(typeof(TestEnum), value)));
        }
    }
}
